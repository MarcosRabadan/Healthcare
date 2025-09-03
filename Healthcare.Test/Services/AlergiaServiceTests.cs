using AutoMapper;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Application.Services;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Enums;
using Healthcare.Domain.Repositories;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Healthcare.Application.DTOs;
using Healthcare.Application.DTOs.Enums;

namespace Healthcare.Test.Services
{
    public class AlergiaServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AlergiaService _service;

        public AlergiaServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _service = new AlergiaService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            _unitOfWorkMock.Setup(u => u.Alergias.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Alergia?)null);

            var result = await _service.GetByIdAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMappedAlergia_WhenFound()
        {
            var alergia = new Alergia { Id = 1 };
            var alergiaDto = new AlergiaResponseDto { Id = 1 };

            _unitOfWorkMock.Setup(u => u.Alergias.GetByIdAsync(1)).ReturnsAsync(alergia);
            _mapperMock.Setup(m => m.Map<AlergiaResponseDto>(alergia)).Returns(alergiaDto);

            var result = await _service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedAlergias()
        {
            var alergias = new List<Alergia> { new Alergia { Id = 1 } };
            var alergiasDto = new List<AlergiaResponseDto> { new AlergiaResponseDto { Id = 1 } };

            _unitOfWorkMock.Setup(u => u.Alergias.GetAllAsync()).ReturnsAsync(alergias);
            _mapperMock.Setup(m => m.Map<IEnumerable<AlergiaResponseDto>>(It.IsAny<IEnumerable<Alergia>>())).Returns(alergiasDto);

            var result = await _service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task CreateAsync_ReturnsError_WhenTipoAlergiaInvalido()
        {
            var request = new AlergiaRequestDto
            { 
                Tipo = new EnumValueDto { Value = 999 },
                Descripcion = "Test",
                FechaDiagnostico = DateTime.Today,
                Severidad = "Alta"
            };

            var result = await _service.CreateAsync(request);

            Assert.Null(result.Created);
            Assert.NotNull(result.Error);
        }

        [Fact]
        public async Task CreateAsync_ReturnsCreatedAlergia_WhenValid()
        {
            var request = new AlergiaRequestDto
            {
                Tipo = new EnumValueDto { Value = (int)TipoAlergia.Medicamentos },
                Descripcion = "Penicilina",
                FechaDiagnostico = DateTime.Today,
                Severidad = "Alta"
            };

            var alergia = new Alergia
            {
                Id = 1,
                Tipo = TipoAlergia.Medicamentos,
                Descripcion = "Penicilina",
                FechaDiagnostico = DateTime.Today,
                Severidad = "Alta"
            };

            var alergiaDto = new AlergiaResponseDto
            {
                Id = 1,
                Tipo = new EnumValueDto { Value = (int)TipoAlergia.Medicamentos },
                Descripcion = "Penicilina",
                FechaDiagnostico = DateTime.Today,
                Severidad = "Alta"
            };

            _mapperMock.Setup(m => m.Map<Alergia>(request)).Returns(alergia);
            _unitOfWorkMock.Setup(u => u.Alergias.AddAsync(alergia)).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);
            _mapperMock.Setup(m => m.Map<AlergiaResponseDto>(alergia)).Returns(alergiaDto);

            var result = await _service.CreateAsync(request);

            Assert.NotNull(result.Created);
            Assert.Null(result.Error);
            Assert.Equal("Penicilina", result.Created.Descripcion);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsFalse_WhenAlergiaNotFound()
        {
            _unitOfWorkMock.Setup(u => u.Alergias.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Alergia?)null);

            var result = await _service.UpdateAsync(1, new AlergiaRequestDto());

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsTrue_WhenAlergiaUpdatedSuccessfully()
        {
            var id = 1;
            var existingAlergia = new Alergia
            {
                Id = id,
                Tipo = TipoAlergia.Medicamentos,
                Descripcion = "Penicilina",
                FechaDiagnostico = DateTime.Today,
                Severidad = "Alta"
            };

            var request = new AlergiaRequestDto
            {
                Tipo = new EnumValueDto { Value = (int)TipoAlergia.Medicamentos },
                Descripcion = "Penicilina",
                FechaDiagnostico = DateTime.Today,
                Severidad = "Alta"
            };

            _unitOfWorkMock.Setup(u => u.Alergias.GetByIdAsync(id)).ReturnsAsync(existingAlergia);
            _mapperMock.Setup(m => m.Map(request, existingAlergia)).Verifiable();
            _unitOfWorkMock.Setup(u => u.Alergias.Update(existingAlergia)).Verifiable();
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _service.UpdateAsync(id, request);

            Assert.True(result);
            _mapperMock.Verify(m => m.Map(request, existingAlergia), Times.Once);
            _unitOfWorkMock.Verify(u => u.Alergias.Update(existingAlergia), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalse_WhenAlergiaNotFound()
        {
            _unitOfWorkMock.Setup(u => u.Alergias.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Alergia?)null);

            var result = await _service.DeleteAsync(1);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsTrue_WhenAlergiaDeletedSuccessfully()
        {
            var id = 1;
            var existingAlergia = new Alergia
            {
                Id = id,
                Tipo = TipoAlergia.Medicamentos,
                Descripcion = "Penicilina",
                FechaDiagnostico = DateTime.Today,
                Severidad = "Alta",
                IsDeleted = false
            };

            _unitOfWorkMock.Setup(u => u.Alergias.GetByIdAsync(id)).ReturnsAsync(existingAlergia);
            _unitOfWorkMock.Setup(u => u.Alergias.Update(existingAlergia)).Verifiable();
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _service.DeleteAsync(id);

            Assert.True(result);
            Assert.True(existingAlergia.IsDeleted);
            _unitOfWorkMock.Verify(u => u.Alergias.Update(existingAlergia), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}