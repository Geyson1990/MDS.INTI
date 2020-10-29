using iText.Layout.Element;
using iText.Layout.Properties;
using MuniSayan.Application.Constants;
using MuniSayan.BusinessLogic.Models;
using MuniSayan.BusinessLogic.Models.Constancia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MuniSayan.Application.Utils.Constancia
{
    public class PdfFormatoConstanciaV1
    {
        public string version = "v1";
        public bool existeData = false;
        public PdfProperties props;

        List<GradoModel> grados;
        List<PDFNota> areas;
        List<PDFNota> competencias;
        List<PDFNota> talleres;
        string observaciones = "";

        public PdfFormatoConstanciaV1(PdfProperties _props)
        {
            props = _props;

            grados = new List<GradoModel>();
            props.estudiante.grados.ForEach(grado =>
            {
                grados.Add(new GradoModel()
                {
                    anexo = grado.anexo,
                    codMod = grado.codMod,
                    corrEstadistica = grado.corrEstadistica,
                    dscGrado = grado.dscGrado,
                    idAnio = (grado.idAnio > props.anioLimiteV1) ? 0 : grado.idAnio,
                    idConstanciaGrado = grado.idConstanciaGrado,
                    idGrado = grado.idGrado,
                    idSolicitud = grado.idSolicitud,
                    situacionFinal = grado.situacionFinal
                });
            });

            props.gradosCursados = grados.Where(w => w.idAnio > 0).Select(x => x.dscGrado).ToArray();
            existeData = props.gradosCursados.Length > 0;
            //props.gradosConcatenados = props.ConcatenarGrados(props.gradosCursados);
            var gradosCursados = props.estudiante.grados.Where(w => w.idAnio > 0).Select(x => x.dscGrado).ToArray();
            props.gradosConcatenados = props.ConcatenarGrados(gradosCursados);

            // notas antes del 2020
            var notas = props.estudiante.notas.Where(i => i.idAnio > 0 && i.idAnio <= props.anioLimiteV1).ToList();
            areas = OrdenarNotasEstudiante(notas, grados, "001");
            competencias = OrdenarNotasEstudiante(notas, grados, "003");
            talleres = OrdenarNotasEstudiante(notas, grados, "002");
            observaciones = ConcatenarObservaciones(props.estudiante.observaciones);
        }

        public Table createContent()
        {
            Table table;

            int extraRows = 3;
            int initRow = extraRows - 1;
            int totalRows = grados.Count + extraRows;
            float[] withColumns = new float[totalRows];
            int withGrados = 7;

            for (int i = initRow; i < totalRows; i++)
            {
                withColumns[i] = withGrados;
            }

            withColumns[totalRows - 1] = 15;

            withColumns[0] = 7;
            withColumns[1] = 100 - ((grados.Count * withGrados) + 22);

            table = tableHeader(withColumns);
            table = tableBody(table);

            return table;
        }

        private Table tableHeader(float[] withColumns)
        {
            int colSpan = 2;
            Table table = new Table(UnitValue.CreatePercentArray(withColumns));
            table.SetWidth(UnitValue.CreatePercentValue(100));
            table.SetHorizontalAlignment(HorizontalAlignment.CENTER);

            table.AddHeaderCell(PDFMinedu.getCell("Año lectivo:", props.fontSize, false, 1, colSpan, TextAlignment.LEFT));

            grados.ForEach(grado =>
            {
                string anio = grado.idAnio.ToString();
                table.AddHeaderCell(PDFMinedu.getCell(anio.Equals("0") ? "-" : anio, props.fontSize, true));
            });

            table.AddHeaderCell(PDFMinedu.getCell("Observación", props.fontSize, false, 3));

            table.AddHeaderCell(PDFMinedu.getCell((props.estudiante.solicitud.idNivel.Equals("B0")
                                                || props.estudiante.solicitud.idNivel.Equals("F0")) ? "Grado:" : "Edades:", props.fontSize, false, 1, colSpan, TextAlignment.LEFT));

            grados.ForEach(grado =>
            {
                table.AddHeaderCell(PDFMinedu.getCell(props.switchTextoGrado(grado.dscGrado), props.fontSize));
            });

            table.AddHeaderCell(PDFMinedu.getCell("Código modular de la IE:", props.fontSize, false, 1, colSpan, TextAlignment.LEFT));

            grados.ForEach(grado =>
            {
                string codModAnexo = (grado.idAnio == 0) ? "-" : grado.codMod + "-" + grado.anexo;
                table.AddHeaderCell(PDFMinedu.getCell(codModAnexo, props.detailFontSize));
            });

            return table;
        }

        private Table tableBody(Table table)
        {
            float fontSize = props.fontSize - 1;

            table.AddCell(PDFMinedu.getCell("Áreas Curriculares", fontSize, false, areas.Count));

            areas.ForEach(area =>
            {
                table.AddCell(PDFMinedu.getCell(area.DscArea, props.detailFontSize, false, 1, 1, TextAlignment.LEFT));
                table = printNotas(table, area);

                //Adición de observaciones
                int total = areas.Count + competencias.Count + talleres.Count;
                if (areas.IndexOf(area) == 0) table.AddCell(PDFMinedu.getCell(observaciones, props.detailFontSize, false, total, 1, TextAlignment.JUSTIFIED));
            });

            if (competencias.Count > 0)
            {
                table.AddCell(PDFMinedu.getCell("Competencias Transversales", fontSize, false, competencias.Count));

                competencias.ForEach(competencia =>
                {
                    table.AddCell(PDFMinedu.getCell(competencia.DscArea, props.detailFontSize, false, 1, 1, TextAlignment.LEFT));
                    table = printNotas(table, competencia);
                });
            }

            if (talleres.Count > 0)
            {
                table.AddCell(PDFMinedu.getCell("Taller(es)", fontSize, false, talleres.Count));

                talleres.ForEach(taller =>
                {
                    table.AddCell(PDFMinedu.getCell(taller.DscArea, props.detailFontSize, false, 1, 1, TextAlignment.LEFT));
                    table = printNotas(table, taller);
                });
            }

            table.AddCell(PDFMinedu.getCell("Situación final", props.fontSize, true, 1, 2, TextAlignment.RIGHT));

            grados.ForEach(grado =>
            {
                table.AddCell(PDFMinedu.getCell(grado.idAnio == 0 ? "-" : grado.situacionFinal, props.detailFontSize, true));
            });

            return table;
        }

        private Table printNotas(Table table, PDFNota notas)
        {
            grados.ForEach(grado =>
            {
                var nota = notas.GradoNotas.Where(x => x.IdGrado == grado.idGrado).FirstOrDefault();
                var notaFinalArea = (nota == null || nota.NotaFinalArea == null || nota.NotaFinalArea == "") ? "-" : nota.NotaFinalArea;

                table.AddCell(PDFMinedu.getCell(notaFinalArea, props.detailFontSize));
            });

            return table;
        }

        private List<PDFNota> OrdenarNotasEstudiante(List<NotaModel> notasList, List<GradoModel> gradosList, string idTipoArea)
        {
            List<PDFNota> list = new List<PDFNota>();
            list = notasList.Where(z => z.idTipoArea == idTipoArea).GroupBy(c => new { c.dscArea })
                .Select(g => new PDFNota
                {
                    DscArea = g.Key.dscArea,
                    GradoNotas = gradosList.Where(x => x.idAnio > 0).GroupBy(a => new { a.idGrado, a.dscGrado })
                        .Select(b => new PDFGradoNota
                        {
                            IdGrado = b.Key.idGrado,
                            DscGrado = b.Key.dscGrado,
                            NotaFinalArea = notasList.Where(w => w.idGrado == b.Key.idGrado && w.dscArea == g.Key.dscArea)
                                .Select(h => h.notaFinalArea).FirstOrDefault()
                        }).ToList()
                })
                .OrderBy(y => y.DscArea)
                .ToList();

            return list;
        }

        private string ConcatenarObservaciones(List<ObservacionModel> observaciones)
        {
            try
            {
                string result = "";

                if (observaciones != null)
                {
                    result = observaciones.Where(o => o.idAnio <= props.anioLimiteV1).Select(x => String.Format("{0} - {1} - {2}", x.idAnio.ToString(), x.resolucion.Trim().ToUpper(), x.motivo.Trim().ToUpper()))
                                          .Aggregate(new StringBuilder(), (current, next) => current.Append(next).Append(Environment.NewLine).Append(Environment.NewLine)).ToString();
                }

                return result;
            }
            catch
            {
                return "";
            }
        }
    }
}
