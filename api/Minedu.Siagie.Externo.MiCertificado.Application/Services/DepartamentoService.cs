using AutoMapper;
using Minedu.Siagie.Externo.MiCertificado.Application.Contract;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using Minedu.Siagie.Externo.MiCertificado.Domain.Queries;
using Minedu.Siagie.Externo.MiCertificado.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Minedu.Siagie.Externo.MiCertificado.Application.Services
{
    public class DepartamentoService : IDepartamentoService
    {
        private readonly IDepartamentoQuery _uow;
        private readonly IMapper _iMapper;

        public DepartamentoService(IMapper iMapper, IDepartamentoQuery unitOfWork)
        {
            _iMapper = iMapper;
            _uow = unitOfWork;
        }

        public async Task<IEnumerable<DepartamentoResponseDto>> Listar()
        {            
            var item = await _uow.Listar();
            return _iMapper.Map<IEnumerable<USP_SEL_DEPARTAMENTO_Result>,IEnumerable<DepartamentoResponseDto>>(item);
        }

        public async Task<IEnumerable<DepartamentoResponseDto>> ListarDepartamentosSIAGIE()
        {
            var item = await _uow.ListarDepartamentosSIAGIE();
            return _iMapper.Map<IEnumerable<USP_SEL_DEPARTAMENTO_SIAGIE_Result>, IEnumerable<DepartamentoResponseDto>>(item);
        }

    }
}
