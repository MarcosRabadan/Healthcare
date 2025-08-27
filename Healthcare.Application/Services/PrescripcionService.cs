using AutoMapper;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;

namespace Healthcare.Application.Services
{
    public class PrescripcionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PrescripcionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PrescripcionResponseDto>> GetAllAsync()
        {
            var prescripciones = await _unitOfWork.Prescripciones.GetAllAsync();   
            return _mapper.Map<IEnumerable<PrescripcionResponseDto>>(prescripciones);
        }

        public async Task<PrescripcionResponseDto?> GetByIdAsync(int id)
        {
            var prescripcion = await _unitOfWork.Prescripciones.GetByIdAsync(id);
            return _mapper.Map<PrescripcionResponseDto?>(prescripcion);
        }

        public async Task<PrescripcionResponseDto> CreateAsync(PrescripcionRequestDto prescripcionDto)
        {
            var prescripcion = _mapper.Map<Prescripcion>(prescripcionDto);  
            await _unitOfWork.Prescripciones.AddAsync(prescripcion);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<PrescripcionResponseDto>(prescripcion);  
        }

        public async Task<bool> UpdateAsync(int id, PrescripcionRequestDto prescripcion)
        {
            var existing = await _unitOfWork.Prescripciones.GetByIdAsync(id);
            if (existing == null)
                return false;

            _mapper.Map(prescripcion, existing);
            _unitOfWork.Prescripciones.Update(existing);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var prescripcion = await _unitOfWork.Prescripciones.GetByIdAsync(id);
            if (prescripcion == null)
                return false;

            prescripcion.IsDeleted = true;
            _unitOfWork.Prescripciones.Update(prescripcion);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}