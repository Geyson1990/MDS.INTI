using iText.Barcodes;
using iText.Barcodes.Qrcode;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Extgstate;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using MuniSayan.Application.Constants;
using MuniSayan.Application.Utils.Constancia;
using System;
using System.Collections.Generic;
using System.IO;

namespace MuniSayan.Application.Utils
{
    public class PdfFormato
    {
        PdfProperties properties;

        public PdfFormato(PdfProperties _props)
        {
            properties = _props;
        }

        public byte[] crearFormatoConstanciaV1()
        {
            PdfFormatoConstanciaV1 formato = new PdfFormatoConstanciaV1(properties);
            PdfProperties props = formato.props;

            if (!formato.existeData)
            {
                return null;
            }

            using (MemoryStream memoryStream = new MemoryStream())
            using (PdfWriter writer = new PdfWriter(memoryStream))
            using (PdfDocument pdf = new PdfDocument(writer))
            using (Document document = new Document(pdf, PageSize.A4.Rotate(), false))
            {
                int sizeMarginBottom = (props.vistaPrevia ? 43 : 87);
                document.SetMargins(80, props.sizeMargin, sizeMarginBottom, props.sizeMargin);

                createHeader(pdf, document, props);
                createSubHeader(document, props);

                Table table = formato.createContent();
                document.Add(table);

                createFooter(pdf, document, props);

                if (!props.estudiante.solicitud.estadoSolicitud.Equals("2"))
                {
                    waterMarkPdf(pdf, props);
                }

                document.Close();
                pdf.Close();

                return memoryStream.GetBuffer();
            }
        }

        public byte[] crearFormatoConstanciaV2(bool printSubHeader = false)
        {
            PdfFormatoConstanciaV2 formato = new PdfFormatoConstanciaV2(properties);
            PdfProperties props = formato.props;

            if (!formato.existeData)
            {
                return null;
            }

            using (MemoryStream memoryStream = new MemoryStream())
            using (PdfWriter writer = new PdfWriter(memoryStream))
            using (PdfDocument pdf = new PdfDocument(writer))
            using (Document document = new Document(pdf, PageSize.A4.Rotate(), false))
            {
                int sizeMarginBottom = (props.vistaPrevia ? 43 : 87);
                document.SetMargins(80, props.sizeMargin, sizeMarginBottom, props.sizeMargin);

                createHeader(pdf, document, props);
                if (printSubHeader) createSubHeader(document, props);

                Table table = formato.createContent();
                document.Add(table);

                createFooter(pdf, document, props);

                if (!props.estudiante.solicitud.estadoSolicitud.Equals("2"))
                {
                    waterMarkPdf(pdf, props);
                }

                document.Close();
                pdf.Close();

                return memoryStream.GetBuffer();
            }
        }

        private Image getQRImage(PdfDocument pdf, string hashQRCode)
        {
            IDictionary<EncodeHintType, Object> hints = new Dictionary<EncodeHintType, object>();
            hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.L);

            BarcodeQRCode barcodeQRCode = new BarcodeQRCode(hashQRCode, hints);
            PdfFormXObject xObject = barcodeQRCode.CreateFormXObject(PDFMinedu.getColor("BLACK"), pdf);
            Image qrImage = new Image(xObject);
            qrImage.ScaleToFit(60, 60);
            qrImage.SetHorizontalAlignment(HorizontalAlignment.CENTER);

            return qrImage;
        }

        private void createHeader(PdfDocument pdf, Document document, PdfProperties props)
        {
            #region Logo
            var pathLogoMinedu = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Public", "Images", "logo_minedu.png");
            byte[] imgdata = File.ReadAllBytes(pathLogoMinedu);

            ImageData imageData = ImageDataFactory.Create(imgdata);
            Image logoMinedu = new Image(imageData)
                .ScaleToFit(130, 400)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);
            #endregion Logo

            float fontSize = props.fontSize + 1;
            Table table = new Table(UnitValue.CreatePercentArray(new float[] { 16, 68, 16 }));
            table.SetWidth(UnitValue.CreatePercentValue(100));
            table.SetHorizontalAlignment(HorizontalAlignment.CENTER);

            table.AddCell(new Cell(4, 1).SetBorder(Border.NO_BORDER).SetPadding(-1).SetPaddingTop(10).Add(logoMinedu).SetVerticalAlignment(VerticalAlignment.TOP));
            table.AddCell(PDFMinedu.getCell("MINISTERIO DE EDUCACIÓN", fontSize, false, 1, 1, TextAlignment.CENTER, null, false));
            table.AddCell(PDFMinedu.getCell(" ", fontSize, false, 1, 1, TextAlignment.CENTER, null, false));
            table.AddCell(PDFMinedu.getCell(props.titulo, (props.fontSize + 2), true, 1, 1, TextAlignment.CENTER, null, false));

            if (!props.vistaPrevia)
            {
                Table tblCodigoVirtual = new Table(UnitValue.CreatePercentArray(new float[] { 100 }));
                tblCodigoVirtual.SetWidth(UnitValue.CreatePercentValue(100));
                tblCodigoVirtual.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                tblCodigoVirtual.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                tblCodigoVirtual.AddCell(PDFMinedu.getCell("CÓDIGO VIRTUAL", fontSize, false, 1, 1, TextAlignment.CENTER, null, false).SetPadding(-1));
                tblCodigoVirtual.AddCell(PDFMinedu.getCell(props.estudiante.solicitud.codigoVirtual, fontSize, true, 1, 1, TextAlignment.CENTER, null, false));
                table.AddCell(new Cell(2, 1).Add(tblCodigoVirtual).SetBorder(Border.NO_BORDER));
            }
            else
            {
                table.AddCell(PDFMinedu.getCell(" ", fontSize, false, 2, 1, TextAlignment.CENTER, null, false));
            }

            table.AddCell(PDFMinedu.getCell(props.estudiante.solicitud.dscModalidad, fontSize, false, 1, 1, TextAlignment.CENTER, null, false));
            table.AddCell(PDFMinedu.getCell("NIVEL DE EDUCACIÓN " + props.estudiante.solicitud.dscNivel, fontSize, false, 1, 1, TextAlignment.CENTER, null, false));
            table.AddCell(PDFMinedu.getCell(" ", fontSize, false, 1, 1, TextAlignment.CENTER, null, false));

            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new StartPageEventHandler(table, document));
        }

        private void createSubHeader(Document document, PdfProperties props)
        {
            PdfFont fontRegular = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontBold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

            Table table = new Table(UnitValue.CreatePercentArray(new float[] { 100 }));
            table.SetWidth(UnitValue.CreatePercentValue(100));
            table.SetHorizontalAlignment(HorizontalAlignment.CENTER);

            table.AddCell(PDFMinedu.getCell("EL MINISTERIO DE EDUCACIÓN DEL PERÚ", props.fontSize, true, 1, 1, TextAlignment.CENTER, null, false).SetPaddingBottom(3f));

            Text text1 = new Text("Hace constar que, de acuerdo con la información registrada en el Sistema de Información de Apoyo a la Gestión de la " +
                                  "Institución Educativa (Siagie), el/la estudiante " + props.estudiante.estudiante.nombres + " " + props.estudiante.estudiante.apellidoPaterno + " " +
                                  props.estudiante.estudiante.apellidoMaterno + ", con DNI N.° " + props.estudiante.estudiante.numeroDocumento + ", registra " +
                                  "calificativos correspondiente(s) a ").SetFont(fontRegular);
            Text text2 = new Text(props.gradosConcatenados + " ").SetFont(fontBold); // BOLD
            Text text3 = new Text((props.estudiante.solicitud.idNivel.Equals("B0")
                                || props.estudiante.solicitud.idNivel.Equals("F0")) ? "grado" : "").SetFont(fontRegular);
            Text text4 = new Text(" de " + props.estudiante.solicitud.abrModalidad + ", nivel ").SetFont(fontRegular);
            Text text5 = new Text((props.estudiante.solicitud.idNivel.Equals("B0") || props.estudiante.solicitud.idNivel.Equals("F0")) ? "de " : "").SetFont(fontRegular);
            Text text6 = new Text(" EDUCACIÓN " + props.estudiante.solicitud.dscNivel).SetFont(fontBold); // BOLD
            Text text7 = new Text(", según consta en las actas de evaluación respectivas cuyo detalle figura a continuación:").SetFont(fontRegular);

            Paragraph paragraph = new Paragraph().Add(text1).Add(text2).Add(text3).Add(text4).Add(text5).Add(text6).Add(text7);
            Cell cell = new Cell().Add(paragraph)
                .SetFontSize(props.fontSize)
                .SetPaddingTop(-1)
                .SetPaddingBottom(-1)
                .SetTextAlignment(TextAlignment.JUSTIFIED)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetBorder(Border.NO_BORDER);

            table.AddCell(cell);
            table.AddCell(new Cell(1, 1).Add(new Paragraph("")).SetHeight(2.5f).SetBorder(Border.NO_BORDER));

            document.Add(table);
        }

        private void createFooter(PdfDocument pdf, Document document, PdfProperties props)
        {
            float fontSize = props.fontSize - 0.5f;
            float[] withColumns;
            Table tableFooter;

            if (!props.vistaPrevia)
            {
                withColumns = new float[] { 10, 75, 15 };
                tableFooter = new Table(UnitValue.CreatePercentArray(withColumns));
                tableFooter.SetWidth(UnitValue.CreatePercentValue(100));
                tableFooter.SetHorizontalAlignment(HorizontalAlignment.CENTER);

                #region QRImage
                Image qrImage = getQRImage(pdf, props.hashQRCode);
                Cell qrCell = new Cell(7, 1);
                qrCell.Add(qrImage);
                qrCell.SetVerticalAlignment(VerticalAlignment.MIDDLE).SetPaddingTop(-5).SetPaddingBottom(-3).SetBorder(Border.NO_BORDER);
                tableFooter.AddCell(qrCell);
                #endregion QRImage

                tableFooter.AddCell(PDFMinedu.getCell("Fecha de emisión:", fontSize, false, 1, 1, TextAlignment.RIGHT, null, false));
                tableFooter.AddCell(PDFMinedu.getCell(props.fechaSolicitud, fontSize, false, 1, 1, TextAlignment.LEFT, null, false).SetPaddingLeft(5));

                tableFooter.AddCell(PDFMinedu.getCell("Hora de emisión:", fontSize, false, 1, 1, TextAlignment.RIGHT, null, false));
                tableFooter.AddCell(PDFMinedu.getCell(props.horaSolicitud, fontSize, false, 1, 1, TextAlignment.LEFT, null, false).SetPaddingLeft(5));

                tableFooter.AddCell(PDFMinedu.getCell("", props.detailFontSize, false, 1, 2, TextAlignment.CENTER, null, false).SetHeight(6f));
                tableFooter.AddCell(PDFMinedu.getCell(
                    string.Format(
                        "* Esta constancia puede ser verificada en el sitio web del Ministerio de Educación ({0}), utilizando lectora " +
                        "de códigos QR o teléfono celular enfocando al código QR: el celular debe poseer un software gratuito descargado de internet.",
                        props.urlQRCode
                    ),
                    props.detailFontSize - 1, false, 1, 2, TextAlignment.CENTER, null, false
                ));
                tableFooter.AddCell(PDFMinedu.getCell(
                    "* EXO: exoneración del área de educación religiosa a solicitud del padre o madre de familia, tutor legal o apoderado.",
                    props.detailFontSize - 1, false, 2, 2, TextAlignment.CENTER, null, false
                ));
                tableFooter.AddCell(PDFMinedu.getCell("", props.detailFontSize, false, 1, 2, TextAlignment.CENTER, null, false).SetHeight(0f));
                tableFooter.AddCell(PDFMinedu.getCell("N.° " + props.correlativo, fontSize, true, 1, 1, TextAlignment.CENTER, null, false));

                tableFooter.AddCell(PDFMinedu.getCell("Calle Del Comercio 193, San Borja, Lima, Perú / (511) 615-5800", fontSize, false, 1, 2, TextAlignment.CENTER, null, false));
            }
            else
            {
                withColumns = new float[] { 100 };
                tableFooter = new Table(UnitValue.CreatePercentArray(withColumns));
                tableFooter.SetWidth(UnitValue.CreatePercentValue(100));
                tableFooter.SetHorizontalAlignment(HorizontalAlignment.CENTER);

                tableFooter.AddCell(PDFMinedu.getCell(
                    "* EXO: exoneración del área de educación religiosa a solicitud del padre o madre de familia, tutor legal o apoderado.",
                    props.detailFontSize - 1, false, 1, 1, TextAlignment.CENTER, null, false
                ));
                tableFooter.AddCell(PDFMinedu.getCell("Calle Del Comercio 193, San Borja, Lima, Perú / (511) 615-5800", fontSize, false, 1, 1, TextAlignment.CENTER, null, false));
            }

            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new EndPageEventHandler(tableFooter, document));
        }

        private void waterMarkPdf(PdfDocument pdfDoc, PdfProperties props)
        {
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            float fontSize = Convert.ToInt32(props.waterMarkFontSize);

            Rectangle pageSize;
            PdfCanvas canvas;
            int n = pdfDoc.GetNumberOfPages();
            for (int i = 1; i <= n; i++)
            {
                PdfPage page = pdfDoc.GetPage(i);
                pageSize = page.GetPageSize();
                canvas = new PdfCanvas(page);

                Paragraph p = new Paragraph(props.waterMarkText).SetFont(font).SetFontSize(fontSize);
                canvas.SaveState();
                PdfExtGState gs1 = new PdfExtGState();
                gs1.SetFillOpacity(0.25f);
                canvas.SetExtGState(gs1);
                canvas.Fill();
                new Canvas(canvas, pdfDoc, pdfDoc.GetDefaultPageSize()).SetFontColor(WebColors.GetRGBColor("GRAY"))
                    .ShowTextAligned(p, pageSize.GetWidth() / 2, pageSize.GetHeight() / 2, 1, TextAlignment.CENTER, VerticalAlignment.MIDDLE, 120);
                canvas.RestoreState();
                canvas.Release();
            }
        }

        private class StartPageEventHandler : IEventHandler
        {
            private Document doc;
            private Table table;

            public StartPageEventHandler(Table table, Document doc)
            {
                this.table = table;
                this.doc = doc;
            }

            public void HandleEvent(Event @event)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
                PdfDocument pdfDoc = docEvent.GetDocument();
                PdfPage page = docEvent.GetPage();
                PdfCanvas canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
                Rectangle rect = new Rectangle(doc.GetLeftMargin(), 480, page.GetPageSize().GetWidth() - doc.GetLeftMargin() - doc.GetRightMargin(), 100f);
                new Canvas(canvas, pdfDoc, rect).Add(table).Close();
            }
        }

        private class EndPageEventHandler : IEventHandler
        {
            private Document doc;
            private Table table;

            public EndPageEventHandler(Table table, Document doc)
            {
                this.table = table;
                this.doc = doc;
            }

            public void HandleEvent(Event @event)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
                PdfDocument pdfDoc = docEvent.GetDocument();
                PdfPage page = docEvent.GetPage();
                PdfCanvas canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);

                float height = doc.GetBottomMargin() - doc.GetLeftMargin();
                Rectangle rect = new Rectangle(doc.GetLeftMargin(), doc.GetLeftMargin() - 8, page.GetPageSize().GetWidth() - doc.GetLeftMargin() - doc.GetRightMargin(), height);
                new Canvas(canvas, pdfDoc, rect).Add(table).Close();
            }
        }
    }
}
