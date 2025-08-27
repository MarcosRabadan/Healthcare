using AutoMapper;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;

namespace Healthcare.Application.Services
{
    public class ConsultaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConsultaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ConsultaResponseDto>> GetAllAsync()
        {
            var consulta = await _unitOfWork.Consultas.GetAllAsync();
            return _mapper.Map<IEnumerable<ConsultaResponseDto>>(consulta);
        }

        public async Task<ConsultaResponseDto?> GetByIdAsync(int id)
        {
            var consulta = await _unitOfWork.Consultas.GetByIdAsync(id);
            return consulta == null ? null : _mapper.Map<ConsultaResponseDto?>(consulta);
        }

        public async Task<ConsultaResponseDto> CreateAsync(ConsultaRequestDto consultaDto)
        {
            var consulta = _mapper.Map<Consulta>(consultaDto);
            await _unitOfWork.Consultas.AddAsync(consulta);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ConsultaResponseDto>(consulta);          
        }

        public async Task<bool> UpdateAsync(int id, ConsultaRequestDto consulta)
        {
            var existing = await _unitOfWork.Consultas.GetByIdAsync(id);
            if (existing == null)
                return false;

            _mapper.Map(consulta, existing);
            _unitOfWork.Consultas.Update(existing);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var consulta = await _unitOfWork.Consultas.GetByIdAsync(id);
            if (consulta == null)
                return false;

            consulta.IsDeleted = true;
            _unitOfWork.Consultas.Update(consulta);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}