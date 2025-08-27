using AutoMapper;
using Healthcare.Application.Constants;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;

namespace Healthcare.Application.Services
{
    public class PacienteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Random _random = new();

        public PacienteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PacienteResponseDto>> GetAllAsync()
        {
            var pacientes = await _unitOfWork.Pacientes.GetAllAsync();
            return _mapper.Map<IEnumerable<PacienteResponseDto>>(pacientes);
        }


        public async Task<PacienteResponseDto?> GetByIdAsync(int id)
        {
            var paciente = await _unitOfWork.Pacientes.GetByIdAsync(id);
            return paciente == null ? null : _mapper.Map<PacienteResponseDto?>(paciente);
        }


        public async Task<(PacienteResponseDto? Created, ErrorResponseDto? Error)> CreateAsync(PacienteRequestDto pacienteDto)
        {
            var emailError = await ValidarEmailUnicoAsync(pacienteDto.Email);
            if (emailError != null)
                return (null, emailError);

            var numeroHistoriaClinica = await GenerarNumeroHistoriaClinicaUnicoAsync();

            var paciente = _mapper.Map<Paciente>(pacienteDto);
            paciente.NumeroHistoriaClinica = numeroHistoriaClinica;

            await _unitOfWork.Pacientes.AddAsync(paciente);
            await _unitOfWork.SaveChangesAsync();

            var createdDto = _mapper.Map<PacienteResponseDto>(paciente);
            return (createdDto, null);
        }


        public async Task<(bool Success, ErrorResponseDto? Error)> UpdateAsync(int id, PacienteRequestDto paciente)
        {
            var existing = await _unitOfWork.Pacientes.GetByIdAsync(id);
            if (existing == null)
                return (false, null);

            if (!string.Equals(existing.Email, paciente.Email, StringComparison.OrdinalIgnoreCase))
            {
                var emailError = await ValidarEmailUnicoAsync(paciente.Email);
                if (emailError != null)
                    return (false, emailError);
            }

            _mapper.Map(paciente, existing);

            _unitOfWork.Pacientes.Update(existing);
            await _unitOfWork.SaveChangesAsync();
            return (true, null);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var paciente = await _unitOfWork.Pacientes.GetByIdAsync(id);

            if (paciente == null)
                return false;

            paciente.IsDeleted = true;
            _unitOfWork.Pacientes.Update(paciente);

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<string> GenerarNumeroHistoriaClinicaUnicoAsync(int size = 11)
        {
            string code;
            bool exists;
            do
            {
                code = "MRC-" + GenerarCodigoAleatorio(size);
                exists = await _unitOfWork.Pacientes.ExistNumeroHistoriaClinicaAsync(code);

            } while (exists);

            return code;
        }

        private string GenerarCodigoAleatorio(int longitud)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Range(0, longitud)
                .Select(i => chars[_random.Next(chars.Length)]).ToArray());
        }

        public async Task<ErrorResponseDto?> ValidarEmailUnicoAsync(string email)
        {
            var existe = await _unitOfWork.Pacientes.ExistEmailAsync(email);
            if (existe)
                return ErrorMessages.EmailYaExiste;
            return null;
        }
    }
}