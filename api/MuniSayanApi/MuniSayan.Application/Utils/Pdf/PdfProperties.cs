using MuniSayan.BusinessLogic.Models.Constancia;
using System.Globalization;
using System.Text;

namespace MuniSayan.Application.Utils
{
    public class PdfProperties
    {
        public string correlativo { get; set; }
        public bool vistaPrevia { get; set; }
        public float fontSize { get; set; }
        public float detailFontSize { get; set; } = 6.5f;
        public int sizeMargin { get; set; }
        public string titulo { get; set; }
        public string hashQRCode { get; set; }
        public string urlQRCode { get; set; }
        public EstudianteConstancia estudiante { get; set; }
        public string waterMarkText { get; set; }
        public string waterMarkFontSize { get; set; }

        public string gradosConcatenados { get; set; }
        public string fechaSolicitud { get; set; } = "";
        public string horaSolicitud { get; set; } = "";
        public string[] gradosCursados { get; set; }
        public int anioLimiteV1 { get; set; }

        public PdfProperties(
            string _correlativo,
            bool _vistaPrevia,
            float _fontSize,
            int _sizeMargin,
            string _titulo,
            string _urlQRCode,
            string _hashQRCode,
            EstudianteConstancia _estudiante,
            string _waterMarkText,
            string _waterMarkFontSize,
            int _anioLimiteV1
        )
        {
            correlativo = _correlativo;
            vistaPrevia = _vistaPrevia;
            fontSize = _fontSize;
            sizeMargin = _sizeMargin;
            titulo = _titulo;
            urlQRCode = _urlQRCode;
            hashQRCode = _hashQRCode;
            estudiante = _estudiante;
            waterMarkText = _waterMarkText;
            waterMarkFontSize = _waterMarkFontSize;
            anioLimiteV1 = _anioLimiteV1;

            detailFontSize = fontSize - 1.5f;

            fechaSolicitud = estudiante.solicitud.fechaCreacion.ToString("d 'de' MMMM 'del' yyyy", CultureInfo.CreateSpecificCulture("es-PE"));
            horaSolicitud = estudiante.solicitud.fechaCreacion.ToString("HH:mm:ss", CultureInfo.CreateSpecificCulture("es-PE"));
        }

        public string ConcatenarGrados(string[] grados)
        {
            StringBuilder text = new StringBuilder();

            for (int i = 0; i < grados.Length; i++)
            {
                switch (grados[i])
                {
                    case "PRIMERO":
                        text.Append("PRIMER");
                        break;
                    case "TERCERO":
                        text.Append("TERCER");
                        break;
                    default:
                        text.Append(grados[i]);
                        break;
                }

                if (grados.Length != 1)
                {
                    if (i == grados.Length - 2)
                    {
                        text.Append(" y ");
                    }
                    else if (i != grados.Length - 1)
                    {
                        text.Append(", ");
                    }
                }

            }

            return text.ToString();
        }

        public string switchTextoGrado(string grado)
        {
            switch (grado)
            {
                case "PRIMERO":
                    return "1.°";
                case "SEGUNDO":
                    return "2.°";
                case "TERCERO":
                    return "3.°";
                case "CUARTO":
                    return "4.°";
                case "QUINTO":
                    return "5.°";
                case "SEXTO":
                    return "6.°";
                default:
                    return grado;
            }
        }
    }
}
