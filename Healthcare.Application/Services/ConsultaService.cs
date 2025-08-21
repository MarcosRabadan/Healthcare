using AutoMapper;
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

        public async Task<IEnumerable<Consulta>> GetAllAsync()
        {
            return await _consultaRepository.GetAllAsync();
        }

        public async Task<Consulta?> GetByIdAsync(int id)
        {
            return await _consultaRepository.GetByIdAsync(id);
        }

        public async Task<Consulta> CreateAsync(Consulta consulta)
        {
            await _consultaRepository.AddAsync(consulta);
            await _consultaRepository.SaveChangesAsync();
            return consulta;
        }

        public async Task<bool> UpdateAsync(int id, Consulta consulta)
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