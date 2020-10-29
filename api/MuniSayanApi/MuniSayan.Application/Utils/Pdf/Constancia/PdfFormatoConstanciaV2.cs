using iText.IO.Font.Constants;
using iText.Kernel.Font;
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
    public class PdfFormatoConstanciaV2
    {
        public string version = "v2";
        public bool existeData = false;
        public PdfProperties props;

        List<GradoModel> grados;
        List<PDFNotaCompetencia> areas;
        List<PDFNotaCompetencia> competencias;
        List<PDFNotaCompetencia> talleres;
        string observaciones = "";

        public PdfFormatoConstanciaV2(PdfProperties _props)
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
                    idAnio = (grado.idAnio <= props.anioLimiteV1) ? 0 : grado.idAnio,
                    idConstanciaGrado = grado.idConstanciaGrado,
                    idGrado = grado.idGrado,
                    idSolicitud = grado.idSolicitud,
                    situacionFinal = grado.situacionFinal
                });
            });

            props.gradosCursados = grados.Where(w => w.idAnio > 0).Select(x => x.dscGrado).ToArray();
            
            //props.gradosConcatenados = props.ConcatenarGrados(props.gradosCursados);
            var gradosCursados = props.estudiante.grados.Where(w => w.idAnio > 0).Select(x => x.dscGrado).ToArray();
            props.gradosConcatenados = props.ConcatenarGrados(gradosCursados);

            // notas a partir del 2020
            var notas = props.estudiante.notas.Where(i => i.idAnio > props.anioLimiteV1).ToList();
            areas = OrdenarNotasEstudiante(notas, grados, "001");
            competencias = OrdenarNotasEstudiante(notas, grados, "003");
            talleres = OrdenarNotasEstudiante(notas, grados, "002");
            observaciones = ConcatenarObservaciones(props.estudiante.observaciones);

            int totalRows = 0;
            areas.ForEach(area => totalRows += area.Competencias.Count);
            competencias.ForEach(competencia => totalRows += competencia.Competencias.Count);
            talleres.ForEach(taller => totalRows += taller.Competencias.Count);

            existeData = props.gradosCursados.Length > 0 && totalRows > 0;
        }

        public Table createContent()
        {
            Table table;

            int extraRows = 4;
            int initRow = extraRows - 1;
            int totalRows = grados.Count + extraRows;
            float[] withColumns = new float[totalRows];
            int withGrados = 7; // 50 / totalGrados;

            for (int i = initRow; i < totalRows; i++)
            {
                withColumns[i] = withGrados;
            }

            withColumns[totalRows - 1] = 15;

            withColumns[0] = 7;
            withColumns[1] = 10;
            withColumns[2] = 100 - ((grados.Count * withGrados) + 32);

            table = tableHeader(withColumns);
            table = tableBody(table);

            return table;
        }

        private Table tableHeader(float[] withColumns)
        {
            int colSpan = 3;
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
            PdfFont fontRegular = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            float fontSize = props.fontSize - 1;
            int rows = areas.Count;

            int totalRows = 0;
            int totalAreasRows = 0;
            int totalCompetenciasRows = 0;
            int totalTalleresRows = 0;

            areas.ForEach(area => totalAreasRows += area.Competencias.Count);
            competencias.ForEach(competencia => totalCompetenciasRows += competencia.Competencias.Count);
            talleres.ForEach(taller => totalTalleresRows += taller.Competencias.Count);
            totalRows = totalAreasRows + totalCompetenciasRows + totalTalleresRows;

            Paragraph rotateText = new Paragraph("Áreas Curriculares y Competencias").SetFont(fontRegular).SetFontSize(fontSize);
            table.AddCell(new Cell(totalAreasRows, 1).Add(rotateText).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
            
            areas.ForEach(area =>
            {
                table.AddCell(PDFMinedu.getCell(area.DscArea, props.detailFontSize, false, area.Competencias.Count, 1, TextAlignment.CENTER));

                area.Competencias.ForEach(competencia =>
                {
                    table.AddCell(PDFMinedu.getCell(competencia.DscArea, props.detailFontSize, false, 1, 1, TextAlignment.LEFT));
                    table = printNotas(table, competencia);

                    //Adición de observaciones
                    if (areas.IndexOf(area) == 0 && area.Competencias.IndexOf(competencia) == 0)
                        table.AddCell(PDFMinedu.getCell(observaciones, props.detailFontSize, false, totalRows, 1, TextAlignment.JUSTIFIED));
                });
            });

            if (competencias.Count > 0)
            {
                rotateText = new Paragraph("Competencias Transversales").SetFont(fontRegular).SetFontSize(fontSize);

                table.AddCell(new Cell(totalCompetenciasRows, 1).Add(rotateText).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE));

                competencias.ForEach(competencia =>
                {
                    //table.AddCell(PDFMinedu.getCell(competencia.DscArea, props.detailFontSize, false, competencia.Competencias.Count, 1, TextAlignment.CENTER));

                    competencia.Competencias.ForEach(detalle =>
                    {
                        //table.AddCell(PDFMinedu.getCell(detalle.DscArea, props.detailFontSize, false, 1, 1, TextAlignment.LEFT));
                        table.AddCell(PDFMinedu.getCell(competencia.DscArea, props.detailFontSize, false, 1, 2, TextAlignment.LEFT));
                        table = printNotas(table, detalle);
                    });
                });
            }

            if (talleres.Count > 0)
            {
                rotateText = new Paragraph("Taller(es) y Competencias").SetFont(fontRegular).SetFontSize(fontSize);

                table.AddCell(new Cell(totalTalleresRows, 1).Add(rotateText).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE));

                talleres.ForEach(taller =>
                {
                    table.AddCell(PDFMinedu.getCell(taller.DscArea, props.detailFontSize, false, taller.Competencias.Count, 1, TextAlignment.CENTER));

                    taller.Competencias.ForEach(competencia =>
                    {
                        table.AddCell(PDFMinedu.getCell(competencia.DscArea, props.detailFontSize, false, 1, 1, TextAlignment.LEFT));
                        table = printNotas(table, competencia);
                    });
                });
            }

            table.AddCell(PDFMinedu.getCell("Situación final", props.fontSize, true, 1, 3, TextAlignment.RIGHT));

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

        private List<PDFNotaCompetencia> OrdenarNotasEstudiante(List<NotaModel> notasList, List<GradoModel> gradosList, string idTipoArea)
        {
            List<PDFNotaCompetencia> list = new List<PDFNotaCompetencia>();

            list = notasList.Where(z => z.idTipoArea == idTipoArea).GroupBy(c => new { c.idArea, c.dscArea })
                .Select(g => new PDFNotaCompetencia
                {
                    DscArea = g.Key.dscArea,
                    Competencias = notasList.Where(c => c.idArea == g.Key.idArea && c.idAsignatura != null).GroupBy(a => new { a.idAsignatura, a.dscAsignatura })
                        .Select(z => new PDFNota
                        {
                            DscArea = z.Key.dscAsignatura,
                            GradoNotas = gradosList.Where(x => x.idAnio > 0).GroupBy(a => new { a.idGrado, a.dscGrado })
                                .Select(b => new PDFGradoNota
                                {
                                    IdGrado = b.Key.idGrado,
                                    DscGrado = b.Key.dscGrado,
                                    NotaFinalArea = notasList.Where(w => w.idGrado == b.Key.idGrado && w.dscArea == g.Key.dscArea)
                                        .Select(h => h.notaFinalArea).FirstOrDefault()
                                }).ToList()
                        })
                        .OrderBy(i => i.DscArea)
                        .ToList()
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
                    result = observaciones.Where(o => o.idAnio > props.anioLimiteV1).Select(x => String.Format("{0} - {1} - {2}", x.idAnio.ToString(), x.resolucion.Trim().ToUpper(), x.motivo.Trim().ToUpper()))
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
