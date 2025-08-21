using AutoMapper;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Application.Services
{
    public class MedicamentoService
    {
        private readonly IMedicamentoRepository _medicamentoRepository;
        private readonly IMapper _mapper;

        public MedicamentoService(IMedicamentoRepository medicamentoRepository, IMapper mapper)
        {
            _medicamentoRepository = medicamentoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Medicamento>> GetAllAsync()
        {
            return await _medicamentoRepository.GetAllAsync();
        }

        public async Task<Medicamento?> GetByIdAsync(int id)
        {
            return await _medicamentoRepository.GetByIdAsync(id);
        }

        public async Task<Medicamento> CreateAsync(Medicamento medicamento)
        {
            await _medicamentoRepository.AddAsync(medicamento);
            await _medicamentoRepository.SaveChangesAsync();
            return medicamento;
        }

        public async Task<bool> UpdateAsync(int id, Medicamento medicamento)
        {
            var existing = await _medicamentoRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            _mapper.Map(medicamento, existing);
            _medicamentoRepository.Update(existing);
            await _medicamentoRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var medicamento = await _medicamentoRepository.GetByIdAsync(id);
            if (medicamento == null)
                return false;

            medicamento.IsDeleted = true;
            _medicamentoRepository.Update(medicamento);
            await _medicamentoRepository.SaveChangesAsync();
            return true;
        }
    }
}