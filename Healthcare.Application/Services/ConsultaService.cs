using AutoMapper;
using Healthcare.Application.DTOs;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Application.Services
{
    public class ConsultaService
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IMapper _mapper;

        public ConsultaService(IConsultaRepository consultaRepository, IMapper mapper)
        {
            _consultaRepository = consultaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ConsultaDto>> GetAllAsync()
        {
            var consulta = await _consultaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ConsultaDto>>(consulta);
        }

        public async Task<ConsultaDto?> GetByIdAsync(int id)
        {
            var consulta = await _consultaRepository.GetByIdAsync(id);
            return consulta == null ? null : _mapper.Map<ConsultaDto?>(consulta);
        }

        public async Task<ConsultaDto> CreateAsync(ConsultaDto consultaDto)
        {
            var consulta = _mapper.Map<Consulta>(consultaDto);
            await _consultaRepository.AddAsync(consulta);
            await _consultaRepository.SaveChangesAsync();
            return _mapper.Map<ConsultaDto>(consulta);          
        }

        public async Task<bool> UpdateAsync(int id, ConsultaDto consulta)
        {
            var existing = await _consultaRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            _mapper.Map(consulta, existing);
            _consultaRepository.Update(existing);
            await _consultaRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var consulta = await _consultaRepository.GetByIdAsync(id);
            if (consulta == null)
                return false;

            consulta.IsDeleted = true;
            _consultaRepository.Update(consulta);
            await _consultaRepository.SaveChangesAsync();
            return true;
        }
    }
}