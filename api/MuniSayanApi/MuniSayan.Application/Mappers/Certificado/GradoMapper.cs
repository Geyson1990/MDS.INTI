﻿using MuniSayan.BusinessLogic.Models.Certificado;
using MuniSayan.DataAccess.Contracts.Entities.Certificado;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuniSayan.Application.Mappers.Certificado
{
    public class GradoMapper
    {
        public static GradoCertificadoEntity Map(GradoCertificadoModel dto)
        {
            return new GradoCertificadoEntity()
            {
                ID_CONSTANCIA_GRADO = dto.idConstanciaGrado,
                ID_SOLICITUD = dto.idSolicitud,
                ID_GRADO = dto.idGrado,
                DSC_GRADO = dto.dscGrado,
                CORR_ESTADISTICA = dto.corrEstadistica,
                ID_ANIO = dto.idAnio,
                COD_MOD = dto.codMod,
                ANEXO = dto.anexo,
                SITUACION_FINAL = dto.situacionFinal
            };
        }

        public static GradoCertificadoModel Map(GradoCertificadoEntity entity)
        {
            return new GradoCertificadoModel()
            {
                idConstanciaGrado = entity.ID_CONSTANCIA_GRADO,
                idSolicitud = entity.ID_SOLICITUD,
                idGrado = entity.ID_GRADO,
                dscGrado = entity.DSC_GRADO,
                corrEstadistica = entity.CORR_ESTADISTICA,
                idAnio = entity.ID_ANIO,
                codMod = entity.COD_MOD,
                anexo = entity.ANEXO,
                situacionFinal = entity.SITUACION_FINAL
            };
        }
    }
}
