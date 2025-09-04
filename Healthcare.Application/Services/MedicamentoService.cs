using AutoMapper;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;

namespace Healthcare.Application.Services
{
    public class MedicamentoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicamentoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IQueryable<MedicamentoResponseDto> GetAll()
        {
            return _unitOfWork.Medicamentos.GetAll()
                .Select(m => _mapper.Map<MedicamentoResponseDto>(m));
        }

        public async Task<MedicamentoResponseDto?> GetByIdAsync(int id)
        {
            var medicamento = await _unitOfWork.Medicamentos.GetByIdAsync(id);
            return medicamento == null ? null : _mapper.Map<MedicamentoResponseDto?>(medicamento);
        }

        public async Task<MedicamentoResponseDto> CreateAsync(MedicamentoRequestDto medicamentoDto)
        {
            var medicamento = _mapper.Map<Medicamento>(medicamentoDto);
            await _unitOfWork.Medicamentos.AddAsync(medicamento);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<MedicamentoResponseDto>(medicamento);
        }

        public async Task<bool> UpdateAsync(int id, MedicamentoRequestDto medicamento)
        {
            var existing = await _unitOfWork.Medicamentos.GetByIdAsync(id);
            if (existing == null)
                return false;

            _mapper.Map(medicamento, existing);
            _unitOfWork.Medicamentos.Update(existing);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var medicamento = await _unitOfWork.Medicamentos.GetByIdAsync(id);
            if (medicamento == null)
                return false;

            medicamento.IsDeleted = true;
            _unitOfWork.Medicamentos.Update(medicamento);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}