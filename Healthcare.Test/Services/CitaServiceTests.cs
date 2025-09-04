using AutoMapper;
using Healthcare.Application.DTOs;
using Healthcare.Application.DTOs.Enums;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Application.Services;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Enums;
using Healthcare.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Healthcare.Test.Services
{
    public class CitaServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CitaService _service;

        public CitaServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _service = new CitaService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            _unitOfWorkMock.Setup(u => u.Citas.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Cita?)null);

            var result = await _service.GetByIdAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMappedCita_WhenFound()
        {
            var cita = new Cita { Id = 1 };
            var citaDto = new CitaResponseDto { Id = 1 };

            _unitOfWorkMock.Setup(u => u.Citas.GetByIdAsync(1)).ReturnsAsync(cita);
            _mapperMock.Setup(m => m.Map<CitaResponseDto>(cita)).Returns(citaDto);

            var result = await _service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void GetAll_ReturnsMappedCitas()
        {
            var citas = new List<Cita>
            {
                new Cita
                {
                    Id = 1,
                    PacienteId = 1,
                    ProfesionalId = 1,
                    Especialidad = "Cardiología",
                    Estado = EstadoCita.Pendiente,
                    FechaHora = new DateTime(2025, 9, 1, 9, 0, 0),
                    IsDeleted = false
                }
            }.AsQueryable();

            var citaDto = new CitaResponseDto
            {
                Id = 1,
                PacienteId = 1,
                ProfesionalId = 1,
                Especialidad = "Cardiología",
                Estado = new EnumValueDto { Value = (int)EstadoCita.Pendiente, Name = "Pendiente" }
            };

            _unitOfWorkMock.Setup(u => u.Citas.GetAll()).Returns(citas);
            _mapperMock.Setup(m => m.Map<CitaResponseDto>(It.IsAny<Cita>())).Returns(citaDto);

            var result = _service.GetAll().ToList();

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Cardiología", result[0].Especialidad);
            Assert.Equal((int)EstadoCita.Pendiente, result[0].Estado.Value);
            Assert.Equal("Pendiente", result[0].Estado.Name);
        }

        [Fact]
        public async Task CreateAsync_ReturnsCreatedCita()
        {
            var request = new CitaRequestDto
            {
                Estado = new EnumValueDto { Value = (int)EstadoCita.Pendiente }
            };

            var cita = new Cita
            {
                Id = 1,
                Estado = EstadoCita.Pendiente

            };

            var citaDto = new CitaResponseDto
            {
                Id = 1,
                Estado = new EnumValueDto { Value = (int)EstadoCita.Pendiente }

            };

            _mapperMock.Setup(m => m.Map<Cita>(request)).Returns(cita);
            _unitOfWorkMock.Setup(u => u.Citas.AddAsync(cita)).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);
            _mapperMock.Setup(m => m.Map<CitaResponseDto>(cita)).Returns(citaDto);

            var result = await _service.CreateAsync(request);
            Assert.Equal(1, result.Created.Id);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsFalse_WhenCitaNotFound()
        {
            _unitOfWorkMock.Setup(u => u.Citas.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Cita?)null);

            var result = await _service.UpdateAsync(1, new CitaRequestDto());

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsTrue_WhenCitaUpdatedSuccessfully()
        {
            var id = 1;
            var existingCita = new Cita { Id = id };

            var request = new CitaRequestDto
            {
            };

            _unitOfWorkMock.Setup(u => u.Citas.GetByIdAsync(id)).ReturnsAsync(existingCita);
            _mapperMock.Setup(m => m.Map(request, existingCita)).Verifiable();
            _unitOfWorkMock.Setup(u => u.Citas.Update(existingCita)).Verifiable();
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _service.UpdateAsync(id, request);

            Assert.True(result);
            _mapperMock.Verify(m => m.Map(request, existingCita), Times.Once);
            _unitOfWorkMock.Verify(u => u.Citas.Update(existingCita), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalse_WhenCitaNotFound()
        {
            _unitOfWorkMock.Setup(u => u.Citas.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Cita?)null);

            var result = await _service.DeleteAsync(1);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsTrue_WhenCitaDeletedSuccessfully()
        {
            var id = 1;
            var existingCita = new Cita
            {
                Id = id,
                IsDeleted = false
            };

            _unitOfWorkMock.Setup(u => u.Citas.GetByIdAsync(id)).ReturnsAsync(existingCita);
            _unitOfWorkMock.Setup(u => u.Citas.Update(existingCita)).Verifiable();
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _service.DeleteAsync(id);

            Assert.True(result);
            Assert.True(existingCita.IsDeleted);
            _unitOfWorkMock.Verify(u => u.Citas.Update(existingCita), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}