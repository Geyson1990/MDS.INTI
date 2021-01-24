using Minedu.Core.General.Communication;
using Minedu.Siagie.Externo.MiCertificado.Dto;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Minedu.Siagie.Externo.MiCertificado.Api.Utils
{
    public class ValidateRequest
    {
        public static void ValidarInputIE(StatusResponse response, InstitucionEducativaPorDreUgelRequestDto request)
        {
            if (String.IsNullOrEmpty(request.CodigoDre) && String.IsNullOrEmpty(request.CodigoUgel) && String.IsNullOrEmpty(request.CodigoModular) && String.IsNullOrEmpty(request.Anexo))
            {
                response.Validations.Add(new MessageStatusResponse("Ingrese el campo CodigoModular o ingrese los campos CodigoDre y CodigoUgel.", "CE"));
            }
            if (!String.IsNullOrEmpty(request.CodigoDre) && String.IsNullOrEmpty(request.CodigoUgel))
            {
                response.Validations.Add(new MessageStatusResponse("Ingrese el campo CodigoUgel.", "CE"));
            }
            if (String.IsNullOrEmpty(request.CodigoDre) && !String.IsNullOrEmpty(request.CodigoUgel))
            {
                response.Validations.Add(new MessageStatusResponse("Ingrese el campo CodigoDre.", "CE"));
            }
        }

        public static void ValidarDocumento(StatusResponse response, string tipoDocumento, string numeroDocumento)
        {
            switch (tipoDocumento.Trim())
            {
                case "1":// CE
                    {
                        if (numeroDocumento.Trim().Length < 8 || numeroDocumento.Trim().Length > 14)
                        {
                            response.Validations.Add(new MessageStatusResponse("El código de estudiante debe tener entre 8 y 14 caracteres", "CE"));
                        }
                        break;
                    }
                case "2": // DNI
                    {
                        if (numeroDocumento.Trim().Length != 8)
                        {
                            response.Validations.Add(new MessageStatusResponse("El número de documento debe tener 8 caracteres", "DNI"));
                        }
                        break;
                    }
                default:
                    response.Validations.Add(new MessageStatusResponse("Campo tipo de documento no válido.", "Tipo de documento"));
                    break;
            }
        }

        public static void ValidarIngresoIE(StatusResponse response, ColegioRequestDto request)
        {
            if (String.IsNullOrEmpty(request.CodigoUgel) && String.IsNullOrEmpty(request.Departamento) &&
                String.IsNullOrEmpty(request.Provincia) && String.IsNullOrEmpty(request.Ubigeo) &&
                String.IsNullOrEmpty(request.CodigoModular))
            {
                response.Validations.Add(new MessageStatusResponse(
                    "Ingrese el campo Departamento, Provincia y Ubigeo o ingrese el campo CodigoUgel o ingrese el campo CodigoModular.", "CE"));
            }

            if (String.IsNullOrEmpty(request.CodigoUgel) && !String.IsNullOrEmpty(request.Departamento) &&
                String.IsNullOrEmpty(request.Provincia) && String.IsNullOrEmpty(request.Ubigeo) &&
                String.IsNullOrEmpty(request.CodigoModular))
            {
                response.Validations.Add(new MessageStatusResponse("Ingrese el campo Provincia y Ubigeo.", "CE"));
            }

            if (String.IsNullOrEmpty(request.CodigoUgel) && !String.IsNullOrEmpty(request.Departamento) &&
                !String.IsNullOrEmpty(request.Provincia) && String.IsNullOrEmpty(request.Ubigeo) &&
                String.IsNullOrEmpty(request.CodigoModular))
            {
                response.Validations.Add(new MessageStatusResponse("Ingrese el campo Ubigeo.", "CE"));
            }

            if (String.IsNullOrEmpty(request.CodigoUgel) && String.IsNullOrEmpty(request.Departamento) &&
                String.IsNullOrEmpty(request.Provincia) && !String.IsNullOrEmpty(request.Ubigeo) &&
                String.IsNullOrEmpty(request.CodigoModular))
            {
                response.Validations.Add(new MessageStatusResponse("Ingrese el campo Departamento y Provincia.", "CE"));
            }

            if (String.IsNullOrEmpty(request.CodigoUgel) && String.IsNullOrEmpty(request.Departamento) &&
                !String.IsNullOrEmpty(request.Provincia) && !String.IsNullOrEmpty(request.Ubigeo) &&
                String.IsNullOrEmpty(request.CodigoModular))
            {
                response.Validations.Add(new MessageStatusResponse("Ingrese el campo Departamento.", "CE"));
            }
        }
    }
}
