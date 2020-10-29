using iText.Kernel.Pdf;
using System.Collections.Generic;
using System.IO;

namespace MuniSayan.Application.Utils.Pdf
{
    public class Pdf
    {
        MemoryStream memoryStream;
        PdfDocument pdf;
        PdfFormato formato;

        public Pdf(PdfProperties props)
        {
            formato = new PdfFormato(props);
        }

        public MemoryStream formatoConstancia()
        {
            using (memoryStream = new MemoryStream())
            using (pdf = new PdfDocument(new PdfWriter(memoryStream).SetSmartMode(true)))
            {
                byte[] bytesPdf1 = formato.crearFormatoConstanciaV1();
                // mostrar subHeader si no existe datos en el formato v1
                bool printSubHeader = bytesPdf1 == null;
                byte[] bytesPdf2 = formato.crearFormatoConstanciaV2(printSubHeader);

                List<byte[]> files = new List<byte[]>();
                if (bytesPdf1 != null) files.Add(bytesPdf1);
                if (bytesPdf2 != null) files.Add(bytesPdf2);

                pdf = mergePdf(files, pdf);

                pdf.Close();

                return memoryStream;
            }
        }

        private PdfDocument mergePdf(List<byte[]> files, PdfDocument pdf)
        {
            foreach (byte[] file in files)
            {
                // Create reader from bytes
                using (MemoryStream memoryStream = new MemoryStream(file))
                using (PdfReader reader = new PdfReader(memoryStream))
                {
                    PdfDocument srcDoc = new PdfDocument(reader);
                    srcDoc.CopyPagesTo(1, srcDoc.GetNumberOfPages(), pdf);
                }
            }

            return pdf;
        }
    }
}
