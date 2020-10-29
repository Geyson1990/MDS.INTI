using Minedu.Comun.Helper;
using Models = MuniSayan.BusinessLogic.Models;
using System.Threading.Tasks;

namespace MuniSayan.Application.Contracts.Services
{
    public interface IConstanciaService
    {
        Task<StatusResponse> ValidarDJ(Models.Constancia.DeclaracionRequest encryptedRequest);
        
        Task<StatusResponse> ValidarApoderado(Models.Constancia.ApoderadoPersonaRequest encryptedRequest);
        
        Task<StatusResponse> ValidarEstudiante(Models.Constancia.EstudiantePersonaRequest encryptedRequest);
        
        Task<StatusResponse> ObtenerNivelesEstudiante(Models.Constancia.EstudianteModalidadRequest encryptedRequest);
        //Task<StatusResponse> ObtenerUltimoColegioEstudiante(Models.Constancia.EstudianteModalidadNivelRequest encryptedRequest);
        Task<StatusResponse> ObtenerUltimoColegioEstudianteV2(Models.Constancia.EstudianteModalidadNivelRequest encryptedRequest);
        Task<StatusResponse> ObtenerUltimaSolicitudEstudiante(Models.Constancia.EstudianteModalidadNivelRequest requencryptedRequestest);
        
        Task<byte[]> VistaPreviaPDFConstancia(Models.Constancia.EstudianteModalidadNivelPersonaRequest encryptedRequest);
        Task<StatusResponse> GenerarPDFCostancia(Models.Constancia.SolicitudRequest encryptedRequest);
        Task<StatusResponse> DescargarPDFConstancia(Models.Constancia.DescargaRequest encryptedRequest);
        Task<StatusResponse> ValidarPDFConstancia(Models.Constancia.VerificacionRequest encryptedRequest);
    }
}