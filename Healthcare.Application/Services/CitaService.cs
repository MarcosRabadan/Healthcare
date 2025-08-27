using AutoMapper;
using Healthcare.Application.Constants;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Enums;
using Healthcare.Domain.Repositories;

namespace Healthcare.Application.Services
{
    public class CitaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CitaService(IUnitOfWork unitOfwork, IMapper mapper)
        {
            _unitOfWork = unitOfwork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CitaResponseDto>> GetAllAsync()
        {
            var citas = await _unitOfWork.Citas.GetAllAsync();
            return _mapper.Map<IEnumerable<CitaResponseDto>>(citas);
        }

        public async Task<CitaResponseDto?> GetByIdAsync(int id)
        {
            var cita = await _unitOfWork.Citas.GetByIdAsync(id);
            return cita == null ? null : _mapper.Map<CitaResponseDto>(cita);
        }

        public async Task<(CitaResponseDto? Created, ErrorResponseDto? Error)> CreateAsync(CitaRequestDto citaDto)
        {
            if (!Enum.IsDefined(typeof(EstadoCita), citaDto.Estado.Value))
            {
                return (null, ErrorMessages.EstadoCitaInvalido);
            }

            var cita = _mapper.Map<Cita>(citaDto);
            await _unitOfWork.Citas.AddAsync(cita);
            await _unitOfWork.SaveChangesAsync();
            var createdDto = _mapper.Map<CitaResponseDto>(cita);
            return (createdDto, null);
        }

        public async Task<bool> UpdateAsync(int id, CitaRequestDto cita)
        {
            var existing = await _unitOfWork.Citas.GetByIdAsync(id);
            if (existing == null)
                return false;

            _mapper.Map(cita, existing);
            _unitOfWork.Citas.Update(existing);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cita = await _unitOfWork.Citas.GetByIdAsync(id);
            if (cita == null)
                return false;

            cita.IsDeleted = true;
            _unitOfWork.Citas.Update(cita);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}