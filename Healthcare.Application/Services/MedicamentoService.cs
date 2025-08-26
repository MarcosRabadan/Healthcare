using AutoMapper;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
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

        public async Task<IEnumerable<MedicamentoResponseDto>> GetAllAsync()
        {
            var medicamento = await _medicamentoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MedicamentoResponseDto>>(medicamento);
        }

        public async Task<MedicamentoResponseDto?> GetByIdAsync(int id)
        {
            var medicamento = await _medicamentoRepository.GetByIdAsync(id);
            return medicamento == null ? null : _mapper.Map<MedicamentoResponseDto?>(medicamento);
        }

        public async Task<MedicamentoResponseDto> CreateAsync(MedicamentoRequestDto medicamentoDto)
        {
            var medicamento = _mapper.Map<Medicamento>(medicamentoDto);
            await _medicamentoRepository.AddAsync(medicamento);
            await _medicamentoRepository.SaveChangesAsync();

            return _mapper.Map<MedicamentoResponseDto>(medicamento);
        }

        public async Task<bool> UpdateAsync(int id, MedicamentoRequestDto medicamento)
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