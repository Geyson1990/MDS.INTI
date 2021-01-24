using System.Collections.Generic;
using System.Threading.Tasks;
using Minedu.Core.General.Communication;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Application.Contract
{
    public interface IInstitucionEducativaService
    {
        Task<IEnumerable<InstitucionEducativaResponseDto>> Listar(InstitucionEducativaRequestDto filtro);
        Task<IEnumerable<InstitucionEducativaPorCodigoResponseDto>> ListarDatosInstitucionesEducativas(InstitucionEducativaPorCodigoRequestDto filtro);
        Task<IEnumerable<InstitucionEducativaPorDreUgelResponseDto>> ListarInstitucionEducativaPorCodigoModular(InstitucionEducativaPorDreUgelRequestDto filtro);
        Task<IEnumerable<EstudianteColegioNivelResponseDto>> ListarInstitucionEducativaNiveles(EstudianteModalidadNivelRequestDto filtro);
        Task<IEnumerable<ColegioPadronResponseDto>> ListarInstitucionEducativaPorPadrones(ColegioRequestDto filtro);
        Task<StatusResponse<IEnumerable<object>>> ConsultarDatosIECunaJardin(string codigoModular, string anexo);
     }
}