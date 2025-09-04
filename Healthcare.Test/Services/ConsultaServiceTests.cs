using AutoMapper;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Application.Services;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Healthcare.Test.Services
{
    public class ConsultaServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ConsultaService _service;

        public ConsultaServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _service = new ConsultaService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            _unitOfWorkMock.Setup(u => u.Consultas.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Consulta?)null);

            var result = await _service.GetByIdAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMappedConsulta_WhenFound()
        {
            var consulta = new Consulta { Id = 1 };
            var consultaDto = new ConsultaResponseDto { Id = 1 };

            _unitOfWorkMock.Setup(u => u.Consultas.GetByIdAsync(1)).ReturnsAsync(consulta);
            _mapperMock.Setup(m => m.Map<ConsultaResponseDto>(consulta)).Returns(consultaDto);

            var result = await _service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void GetAll_ReturnsMappedConsultas()
        {
            var consultas = new List<Consulta>
            {
                new Consulta
                {
                    Id = 1,
                    PacienteId = 1,
                    ProfesionalId = 1,
                    Fecha = new DateTime(2025, 9, 1),
                    IsDeleted = false
                }
            }.AsQueryable();

            var consultaDto = new ConsultaResponseDto
            {
                Id = 1,
                PacienteId = 1,
                ProfesionalId = 1
            };

            _unitOfWorkMock.Setup(u => u.Consultas.GetAll()).Returns(consultas);
            _mapperMock.Setup(m => m.Map<ConsultaResponseDto>(It.IsAny<Consulta>())).Returns(consultaDto);

            var result = _service.GetAll().ToList();

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(1, result[0].PacienteId);
            Assert.Equal(1, result[0].ProfesionalId);
        }

        [Fact]
        public async Task CreateAsync_ReturnsCreatedConsulta()
        {
            var request = new ConsultaRequestDto
            {
            };

            var consulta = new Consulta
            {
                Id = 1
            };

            var consultaDto = new ConsultaResponseDto
            {
                Id = 1
            };

            _mapperMock.Setup(m => m.Map<Consulta>(request)).Returns(consulta);
            _unitOfWorkMock.Setup(u => u.Consultas.AddAsync(consulta)).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);
            _mapperMock.Setup(m => m.Map<ConsultaResponseDto>(consulta)).Returns(consultaDto);

            var result = await _service.CreateAsync(request);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsFalse_WhenConsultaNotFound()
        {
            _unitOfWorkMock.Setup(u => u.Consultas.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Consulta?)null);

            var result = await _service.UpdateAsync(1, new ConsultaRequestDto());

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsTrue_WhenConsultaUpdatedSuccessfully()
        {
            var id = 1;
            var existingConsulta = new Consulta { Id = id };

            var request = new ConsultaRequestDto
            {
            };

            _unitOfWorkMock.Setup(u => u.Consultas.GetByIdAsync(id)).ReturnsAsync(existingConsulta);
            _mapperMock.Setup(m => m.Map(request, existingConsulta)).Verifiable();
            _unitOfWorkMock.Setup(u => u.Consultas.Update(existingConsulta)).Verifiable();
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _service.UpdateAsync(id, request);

            Assert.True(result);
            _mapperMock.Verify(m => m.Map(request, existingConsulta), Times.Once);
            _unitOfWorkMock.Verify(u => u.Consultas.Update(existingConsulta), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalse_WhenConsultaNotFound()
        {
            _unitOfWorkMock.Setup(u => u.Consultas.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Consulta?)null);

            var result = await _service.DeleteAsync(1);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsTrue_WhenConsultaDeletedSuccessfully()
        {
            var id = 1;
            var existingConsulta = new Consulta
            {
                Id = id,
                IsDeleted = false
            };

            _unitOfWorkMock.Setup(u => u.Consultas.GetByIdAsync(id)).ReturnsAsync(existingConsulta);
            _unitOfWorkMock.Setup(u => u.Consultas.Update(existingConsulta)).Verifiable();
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _service.DeleteAsync(id);

            Assert.True(result);
            Assert.True(existingConsulta.IsDeleted);
            _unitOfWorkMock.Verify(u => u.Consultas.Update(existingConsulta), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}