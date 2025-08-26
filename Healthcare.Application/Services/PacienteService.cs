using AutoMapper;
using Healthcare.Application.Constants;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Application.Services
{
    public class PacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMapper _mapper;
        private readonly Random _random = new();

        public PacienteService(IPacienteRepository pacienteRepository, IMapper mapper)
        {
            _pacienteRepository = pacienteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PacienteResponseDto>> GetAllAsync()
        {
            var pacientes = await _pacienteRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PacienteResponseDto>>(pacientes);
        }


        public async Task<PacienteResponseDto?> GetByIdAsync(int id)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(id);
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

            await _pacienteRepository.AddAsync(paciente);
            await _pacienteRepository.SaveChangesAsync();

            var createdDto = _mapper.Map<PacienteResponseDto>(paciente);
            return (createdDto, null);
        }


        public async Task<bool> UpdateAsync(int id, PacienteRequestDto paciente)
        {
            var existing = await _pacienteRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            _mapper.Map(paciente, existing);

            _pacienteRepository.Update(existing);
            await _pacienteRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(id);

            if (paciente == null)
                return false;

            paciente.IsDeleted = true;
            _pacienteRepository.Update(paciente);

            await _pacienteRepository.SaveChangesAsync();
            return true;
        }

        public async Task<string> GenerarNumeroHistoriaClinicaUnicoAsync(int size = 11)
        {
            string code;
            bool exists;
            do
            {
                code = "MRC-" + GenerarCodigoAleatorio(size);
                exists = await _pacienteRepository.ExistNumeroHistoriaClinicaAsync(code);

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
            var existe = await _pacienteRepository.ExistEmailAsync(email);
            if (existe)
                return ErrorMessages.EmailYaExiste;
            return null;
        }
    }
}