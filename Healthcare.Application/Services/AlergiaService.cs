using AutoMapper;
using Healthcare.Application.Constants;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Enums;
using Healthcare.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Healthcare.Application.Services
{
    public class AlergiaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AlergiaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AlergiaResponseDto?> GetByIdAsync(int id)
        {
            var alergia = await _unitOfWork.Alergias.GetByIdAsync(id);
            return alergia == null ? null : _mapper.Map<AlergiaResponseDto>(alergia);
        }

        public IQueryable<AlergiaResponseDto> GetAll()
        {
            return _unitOfWork.Alergias.GetAll()
                .Select(a => _mapper.Map<AlergiaResponseDto>(a));
        }

        public async Task<(AlergiaResponseDto? Created, ErrorResponseDto? Error)> CreateAsync(AlergiaRequestDto alergiaDto)
        {
            if (!Enum.IsDefined(typeof(TipoAlergia), alergiaDto.Tipo.Value))            
                return (null, ErrorMessages.TipoAlergiaInvalido);
            

            var alergia = _mapper.Map<Alergia>(alergiaDto);

            await _unitOfWork.Alergias.AddAsync(alergia);
            await _unitOfWork.SaveChangesAsync();

            var createdDto = _mapper.Map<AlergiaResponseDto>(alergia);
            return (createdDto, null);
        }

        public async Task<bool> UpdateAsync(int id, AlergiaRequestDto alergia)
        {
            var existing = await _unitOfWork.Alergias.GetByIdAsync(id);
            if (existing == null)
                return false;

            _mapper.Map(alergia, existing);

            _unitOfWork.Alergias.Update(existing);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var alergia = await _unitOfWork.Alergias.GetByIdAsync(id);

            if (alergia == null)
                return false;

            alergia.IsDeleted = true;
            _unitOfWork.Alergias.Update(alergia);

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}