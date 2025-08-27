using AutoMapper;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;

namespace Healthcare.Application.Services
{
    public class AnamnesisService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AnamnesisService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AnamnesisResponseDto>> GetAllAsync()
        {
            var anamesis = await _unitOfWork.Anamnesis.GetAllAsync();
            return _mapper.Map<IEnumerable<AnamnesisResponseDto>>(anamesis);
        }

        public async Task<AnamnesisResponseDto?> GetByIdAsync(int id)
        {
            var anamesis = await _unitOfWork.Anamnesis.GetByIdAsync(id);
            return anamesis == null ? null : _mapper.Map<AnamnesisResponseDto>(anamesis);
        }

        public async Task<AnamnesisResponseDto> CreateAsync(AnamnesisRequestDto anamnesisDto)
        {
            var anamnesis = _mapper.Map<Anamnesis>(anamnesisDto);
            await _unitOfWork.Anamnesis.AddAsync(anamnesis);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<AnamnesisResponseDto>(anamnesis);
        }

        public async Task<bool> UpdateAsync(int id, AnamnesisRequestDto anamnesis)
        {
            var existing = await _unitOfWork.Anamnesis.GetByIdAsync(id);
            if (existing == null)
                return false;

            _mapper.Map(anamnesis, existing);
            _unitOfWork.Anamnesis.Update(existing);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var anamnesis = await _unitOfWork.Anamnesis.GetByIdAsync(id);
            if (anamnesis == null)
                return false;

            anamnesis.IsDeleted = true;
            _unitOfWork.Anamnesis.Update(anamnesis);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}