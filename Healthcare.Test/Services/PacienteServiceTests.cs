using AutoMapper;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Application.Services;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using Moq;
using Xunit;


namespace Healthcare.Test.Services
{

    public class PacienteServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly PacienteService _service;

        public PacienteServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _service = new PacienteService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            _unitOfWorkMock.Setup(u => u.Pacientes.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Paciente?)null);

            var result = await _service.GetByIdAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedPacientes()
        {
            var pacientes = new List<Paciente> { new Paciente() };
            var pacientesDto = new List<PacienteResponseDto> 
            { 
                new PacienteResponseDto() 
                {
                    Id = 1,
                    Nombre = "Emilio",
                    Apellidos = "Delgado",
                    FechaNacimiento = new DateTime(1990, 1, 1),
                    Sexo = "M",
                    Direccion = "Desengano 21",
                    Telefono = "600123456",
                    Email = "emilio.delgado@gmail.com",
                    NumeroHistoriaClinica = "MRC-000000001"
                } 
            };

            _unitOfWorkMock.Setup(u => u.Pacientes.GetAllAsync()).ReturnsAsync(pacientes);
            _mapperMock.Setup(m => m.Map<IEnumerable<PacienteResponseDto>>(pacientes)).Returns(pacientesDto);

            var result = await _service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task CreateAsync_ReturnsError_WhenEmailExists()
        {
            var request = new PacienteRequestDto { Email = "test@mail.com" };
            _unitOfWorkMock.Setup(u => u.Pacientes.ExistEmailAsync(request.Email)).ReturnsAsync(true);

            var result = await _service.CreateAsync(request);

            Assert.Null(result.Created);
            Assert.NotNull(result.Error);
        }
        [Fact]
        public async Task CreateAsync_ReturnsCreatedPaciente_WhenEmailDoesNotExist()
        {
            var request = new PacienteRequestDto
            {
                Nombre = "Emilio",
                Apellidos = "Delgado",
                FechaNacimiento = new DateTime(1990, 1, 1),
                Sexo = "M",
                Direccion = "Desengano 21",
                Telefono = "600123456",
                Email = "emilio.delgado@gmail.com"
            };

            var paciente = new Paciente
            {
                Nombre = request.Nombre,
                Apellidos = request.Apellidos,
                FechaNacimiento = request.FechaNacimiento,
                Sexo = request.Sexo,
                Direccion = request.Direccion,
                Telefono = request.Telefono,
                Email = request.Email,
                NumeroHistoriaClinica = "MRC-000000001"
            };

            var pacienteDto = new PacienteResponseDto
            {
                Id = 1,
                Nombre = request.Nombre,
                Apellidos = request.Apellidos,
                FechaNacimiento = request.FechaNacimiento,
                Sexo = request.Sexo,
                Direccion = request.Direccion,
                Telefono = request.Telefono,
                Email = request.Email,
                NumeroHistoriaClinica = "MRC-000000001"
            };

            _unitOfWorkMock.Setup(u => u.Pacientes.ExistEmailAsync(request.Email)).ReturnsAsync(false);
            _service.GetType().GetMethod("GenerarNumeroHistoriaClinicaUnicoAsync", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.Invoke(_service, new object[] { 11 });

            _mapperMock.Setup(m => m.Map<Paciente>(request)).Returns(paciente);
            _unitOfWorkMock.Setup(u => u.Pacientes.AddAsync(It.IsAny<Paciente>())).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);
            _mapperMock.Setup(m => m.Map<PacienteResponseDto>(paciente)).Returns(pacienteDto);

            var result = await _service.CreateAsync(request);

            Assert.NotNull(result.Created);
            Assert.Null(result.Error);
            Assert.Equal(request.Nombre, result.Created.Nombre);
            Assert.Equal(request.Email, result.Created.Email);
            Assert.Equal("MRC-000000001", result.Created.NumeroHistoriaClinica);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsFalse_WhenPacienteNotFound()
        {
            _unitOfWorkMock.Setup(u => u.Pacientes.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Paciente?)null);

            var result = await _service.UpdateAsync(1, new PacienteRequestDto());

            Assert.False(result.Success);
            Assert.Null(result.Error);
        }
        [Fact]
        public async Task UpdateAsync_ReturnsTrue_WhenPacienteUpdatedSuccessfully()
        {
            var id = 1;
            var existingPaciente = new Paciente
            {
                Id = id,
                Nombre = "Emilio",
                Apellidos = "Delgado",
                FechaNacimiento = new DateTime(1990, 1, 1),
                Sexo = "M",
                Direccion = "Desengano 21",
                Telefono = "600123456",
                Email = "emilio.delgado@gmail.com",
                NumeroHistoriaClinica = "MRC-000000001"
            };

            var request = new PacienteRequestDto
            {
                Nombre = "Emilio",
                Apellidos = "Delgado",
                FechaNacimiento = new DateTime(1990, 1, 1),
                Sexo = "M",
                Direccion = "Desengano 21",
                Telefono = "600123456",
                Email = "emilio.delgado@gmail.com"
            };

            _unitOfWorkMock.Setup(u => u.Pacientes.GetByIdAsync(id)).ReturnsAsync(existingPaciente);
            _mapperMock.Setup(m => m.Map(request, existingPaciente)).Verifiable();
            _unitOfWorkMock.Setup(u => u.Pacientes.Update(existingPaciente)).Verifiable();
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _service.UpdateAsync(id, request);

            Assert.True(result.Success);
            Assert.Null(result.Error);
            _mapperMock.Verify(m => m.Map(request, existingPaciente), Times.Once);
            _unitOfWorkMock.Verify(u => u.Pacientes.Update(existingPaciente), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
        [Fact]
        public async Task DeleteAsync_ReturnsFalse_WhenPacienteNotFound()
        {
            _unitOfWorkMock.Setup(u => u.Pacientes.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Paciente?)null);

            var result = await _service.DeleteAsync(1);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsTrue_WhenPacienteDeletedSuccessfully()
        {
            var id = 1;
            var existingPaciente = new Paciente
            {
                Id = id,
                Nombre = "Emilio",
                Apellidos = "Delgado",
                FechaNacimiento = new DateTime(1990, 1, 1),
                Sexo = "M",
                Direccion = "Desengano 21",
                Telefono = "600123456",
                Email = "emilio.delgado@gmail.com",
                NumeroHistoriaClinica = "MRC-000000001",
                IsDeleted = false
            };

            _unitOfWorkMock.Setup(u => u.Pacientes.GetByIdAsync(id)).ReturnsAsync(existingPaciente);
            _unitOfWorkMock.Setup(u => u.Pacientes.Update(existingPaciente)).Verifiable();
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _service.DeleteAsync(id);

            Assert.True(result);
            Assert.True(existingPaciente.IsDeleted);
            _unitOfWorkMock.Verify(u => u.Pacientes.Update(existingPaciente), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }

}
