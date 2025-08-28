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

namespace Healthcare.Test.Services
{
    public class PrescripcionServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly PrescripcionService _service;

        public PrescripcionServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _service = new PrescripcionService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            _unitOfWorkMock.Setup(u => u.Prescripciones.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Prescripcion?)null);

            var result = await _service.GetByIdAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMappedPrescripcion_WhenFound()
        {
            var prescripcion = new Prescripcion { Id = 1 };
            var prescripcionDto = new PrescripcionResponseDto { Id = 1 };

            _unitOfWorkMock.Setup(u => u.Prescripciones.GetByIdAsync(1)).ReturnsAsync(prescripcion);
            _mapperMock.Setup(m => m.Map<PrescripcionResponseDto>(prescripcion)).Returns(prescripcionDto);

            var result = await _service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedPrescripciones()
        {
            var prescripciones = new List<Prescripcion> { new Prescripcion { Id = 1 } };
            var prescripcionesDto = new List<PrescripcionResponseDto> { new PrescripcionResponseDto { Id = 1 } };

            _unitOfWorkMock.Setup(u => u.Prescripciones.GetAllAsync()).ReturnsAsync(prescripciones);
            _mapperMock.Setup(m => m.Map<IEnumerable<PrescripcionResponseDto>>(It.IsAny<IEnumerable<Prescripcion>>())).Returns(prescripcionesDto);

            var result = await _service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task CreateAsync_ReturnsCreatedPrescripcion()
        {
            var request = new PrescripcionRequestDto
            {
            };

            var prescripcion = new Prescripcion
            {
                Id = 1
            };

            var prescripcionDto = new PrescripcionResponseDto
            {
                Id = 1
            };

            _mapperMock.Setup(m => m.Map<Prescripcion>(request)).Returns(prescripcion);
            _unitOfWorkMock.Setup(u => u.Prescripciones.AddAsync(prescripcion)).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);
            _mapperMock.Setup(m => m.Map<PrescripcionResponseDto>(prescripcion)).Returns(prescripcionDto);

            var result = await _service.CreateAsync(request);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsFalse_WhenPrescripcionNotFound()
        {
            _unitOfWorkMock.Setup(u => u.Prescripciones.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Prescripcion?)null);

            var result = await _service.UpdateAsync(1, new PrescripcionRequestDto());

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsTrue_WhenPrescripcionUpdatedSuccessfully()
        {
            var id = 1;
            var existingPrescripcion = new Prescripcion { Id = id };

            var request = new PrescripcionRequestDto
            {
            };

            _unitOfWorkMock.Setup(u => u.Prescripciones.GetByIdAsync(id)).ReturnsAsync(existingPrescripcion);
            _mapperMock.Setup(m => m.Map(request, existingPrescripcion)).Verifiable();
            _unitOfWorkMock.Setup(u => u.Prescripciones.Update(existingPrescripcion)).Verifiable();
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _service.UpdateAsync(id, request);

            Assert.True(result);
            _mapperMock.Verify(m => m.Map(request, existingPrescripcion), Times.Once);
            _unitOfWorkMock.Verify(u => u.Prescripciones.Update(existingPrescripcion), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalse_WhenPrescripcionNotFound()
        {
            _unitOfWorkMock.Setup(u => u.Prescripciones.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Prescripcion?)null);

            var result = await _service.DeleteAsync(1);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsTrue_WhenPrescripcionDeletedSuccessfully()
        {
            var id = 1;
            var existingPrescripcion = new Prescripcion
            {
                Id = id,
                IsDeleted = false
            };

            _unitOfWorkMock.Setup(u => u.Prescripciones.GetByIdAsync(id)).ReturnsAsync(existingPrescripcion);
            _unitOfWorkMock.Setup(u => u.Prescripciones.Update(existingPrescripcion)).Verifiable();
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _service.DeleteAsync(id);

            Assert.True(result);
            Assert.True(existingPrescripcion.IsDeleted);
            _unitOfWorkMock.Verify(u => u.Prescripciones.Update(existingPrescripcion), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}