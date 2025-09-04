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
    public class ProfesionalServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ProfesionalService _service;

        public ProfesionalServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _service = new ProfesionalService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            _unitOfWorkMock.Setup(u => u.Profesionales.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Profesional?)null);

            var result = await _service.GetByIdAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMappedProfesional_WhenFound()
        {
            var profesional = new Profesional { Id = 1 };
            var profesionalDto = new ProfesionalResponseDto { Id = 1 };

            _unitOfWorkMock.Setup(u => u.Profesionales.GetByIdAsync(1)).ReturnsAsync(profesional);
            _mapperMock.Setup(m => m.Map<ProfesionalResponseDto>(profesional)).Returns(profesionalDto);

            var result = await _service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void GetAll_ReturnsMappedProfesionales()
        {
            var profesionales = new List<Profesional>
            {
                new Profesional
                {
                    Id = 1,
                    NombreCompleto = "Marcos Rabadan",
                    Especialidad = "Cardiología",
                    Email = "marcos.rabadan@hospital.com",
                    IsDeleted = false
                }
            }.AsQueryable();

            var profesionalDto = new ProfesionalResponseDto
            {
                Id = 1,
                NombreCompleto = "Marcos Rabadan",
                Especialidad = "Cardiología",
                Email = "marcos.rabadan@hospital.com"
            };

            _unitOfWorkMock.Setup(u => u.Profesionales.GetAll()).Returns(profesionales);
            _mapperMock.Setup(m => m.Map<ProfesionalResponseDto>(It.IsAny<Profesional>())).Returns(profesionalDto);

            var result = _service.GetAll().ToList();

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Marcos Rabadan", result[0].NombreCompleto);
            Assert.Equal("Cardiología", result[0].Especialidad);
            Assert.Equal("marcos.rabadan@hospital.com", result[0].Email);
        }

        [Fact]
        public async Task CreateAsync_ReturnsCreatedProfesional()
        {
            var request = new ProfesionalRequestDto
            {
            };

            var profesional = new Profesional
            {
                Id = 1
            };

            var profesionalDto = new ProfesionalResponseDto
            {
                Id = 1
            };

            _mapperMock.Setup(m => m.Map<Profesional>(request)).Returns(profesional);
            _unitOfWorkMock.Setup(u => u.Profesionales.AddAsync(profesional)).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);
            _mapperMock.Setup(m => m.Map<ProfesionalResponseDto>(profesional)).Returns(profesionalDto);

            var result = await _service.CreateAsync(request);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsFalse_WhenProfesionalNotFound()
        {
            _unitOfWorkMock.Setup(u => u.Profesionales.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Profesional?)null);

            var result = await _service.UpdateAsync(1, new ProfesionalRequestDto());

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsTrue_WhenProfesionalUpdatedSuccessfully()
        {
            var id = 1;
            var existingProfesional = new Profesional { Id = id };

            var request = new ProfesionalRequestDto
            {
            };

            _unitOfWorkMock.Setup(u => u.Profesionales.GetByIdAsync(id)).ReturnsAsync(existingProfesional);
            _mapperMock.Setup(m => m.Map(request, existingProfesional)).Verifiable();
            _unitOfWorkMock.Setup(u => u.Profesionales.Update(existingProfesional)).Verifiable();
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _service.UpdateAsync(id, request);

            Assert.True(result);
            _mapperMock.Verify(m => m.Map(request, existingProfesional), Times.Once);
            _unitOfWorkMock.Verify(u => u.Profesionales.Update(existingProfesional), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalse_WhenProfesionalNotFound()
        {
            _unitOfWorkMock.Setup(u => u.Profesionales.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Profesional?)null);

            var result = await _service.DeleteAsync(1);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsTrue_WhenProfesionalDeletedSuccessfully()
        {
            var id = 1;
            var existingProfesional = new Profesional
            {
                Id = id,
                IsDeleted = false
            };

            _unitOfWorkMock.Setup(u => u.Profesionales.GetByIdAsync(id)).ReturnsAsync(existingProfesional);
            _unitOfWorkMock.Setup(u => u.Profesionales.Update(existingProfesional)).Verifiable();
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _service.DeleteAsync(id);

            Assert.True(result);
            Assert.True(existingProfesional.IsDeleted);
            _unitOfWorkMock.Verify(u => u.Profesionales.Update(existingProfesional), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}