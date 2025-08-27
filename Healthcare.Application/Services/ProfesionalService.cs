using AutoMapper;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;

namespace Healthcare.Application.Services
{
    public class ProfesionalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProfesionalService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProfesionalResponseDto>> GetAllAsync()
        {
            var profesionales = await _unitOfWork.Profesionales.GetAllAsync();
            return _mapper.Map<IEnumerable<ProfesionalResponseDto>>(profesionales);
        }

        public async Task<ProfesionalResponseDto?> GetByIdAsync(int id)
        {
            var profesional = await _unitOfWork.Profesionales.GetByIdAsync(id);
            return profesional == null ? null : _mapper.Map<ProfesionalResponseDto?>(profesional);
        }

        public async Task<ProfesionalResponseDto> CreateAsync(ProfesionalRequestDto profesionalDto)
        {
            var profesional = _mapper.Map<Profesional>(profesionalDto);
            await _unitOfWork.Profesionales.AddAsync(profesional);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProfesionalResponseDto>(profesional);
        }

        public async Task<bool> UpdateAsync(int id, ProfesionalRequestDto profesional)
        {
            var existing = await _unitOfWork.Profesionales.GetByIdAsync(id);
            if (existing == null)
                return false;

            _mapper.Map(profesional, existing);
            _unitOfWork.Profesionales.Update(existing);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var profesional = await _unitOfWork.Profesionales.GetByIdAsync(id);
            if (profesional == null)
                return false;

            profesional.IsDeleted = true;
            _unitOfWork.Profesionales.Update(profesional);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}