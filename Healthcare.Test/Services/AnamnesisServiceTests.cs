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
    public class AnamnesisServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AnamnesisService _service;

        public AnamnesisServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _service = new AnamnesisService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            _unitOfWorkMock.Setup(u => u.Anamnesis.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Anamnesis?)null);

            var result = await _service.GetByIdAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMappedAnamnesis_WhenFound()
        {
            var anamnesis = new Anamnesis { Id = 1 };
            var anamnesisDto = new AnamnesisResponseDto { Id = 1 };

            _unitOfWorkMock.Setup(u => u.Anamnesis.GetByIdAsync(1)).ReturnsAsync(anamnesis);
            _mapperMock.Setup(m => m.Map<AnamnesisResponseDto>(anamnesis)).Returns(anamnesisDto);

            var result = await _service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedAnamnesis()
        {
            var anamnesisList = new List<Anamnesis> { new Anamnesis { Id = 1 } };
            var anamnesisDtoList = new List<AnamnesisResponseDto> { new AnamnesisResponseDto { Id = 1 } };

            _unitOfWorkMock.Setup(u => u.Anamnesis.GetAllAsync()).ReturnsAsync(anamnesisList);
            _mapperMock.Setup(m => m.Map<IEnumerable<AnamnesisResponseDto>>(It.IsAny<IEnumerable<Anamnesis>>())).Returns(anamnesisDtoList);

            var result = await _service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task CreateAsync_ReturnsCreatedAnamnesis()
        {
            var request = new AnamnesisRequestDto
            {
            };

            var anamnesis = new Anamnesis
            {
                Id = 1
            };

            var anamnesisDto = new AnamnesisResponseDto
            {
                Id = 1
            };

            _mapperMock.Setup(m => m.Map<Anamnesis>(request)).Returns(anamnesis);
            _unitOfWorkMock.Setup(u => u.Anamnesis.AddAsync(anamnesis)).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);
            _mapperMock.Setup(m => m.Map<AnamnesisResponseDto>(anamnesis)).Returns(anamnesisDto);

            var result = await _service.CreateAsync(request);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsFalse_WhenAnamnesisNotFound()
        {
            _unitOfWorkMock.Setup(u => u.Anamnesis.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Anamnesis?)null);

            var result = await _service.UpdateAsync(1, new AnamnesisRequestDto());

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsTrue_WhenAnamnesisUpdatedSuccessfully()
        {
            var id = 1;
            var existingAnamnesis = new Anamnesis { Id = id };

            var request = new AnamnesisRequestDto
            {
            };

            _unitOfWorkMock.Setup(u => u.Anamnesis.GetByIdAsync(id)).ReturnsAsync(existingAnamnesis);
            _mapperMock.Setup(m => m.Map(request, existingAnamnesis)).Verifiable();
            _unitOfWorkMock.Setup(u => u.Anamnesis.Update(existingAnamnesis)).Verifiable();
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _service.UpdateAsync(id, request);

            Assert.True(result);
            _mapperMock.Verify(m => m.Map(request, existingAnamnesis), Times.Once);
            _unitOfWorkMock.Verify(u => u.Anamnesis.Update(existingAnamnesis), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalse_WhenAnamnesisNotFound()
        {
            _unitOfWorkMock.Setup(u => u.Anamnesis.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Anamnesis?)null);

            var result = await _service.DeleteAsync(1);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsTrue_WhenAnamnesisDeletedSuccessfully()
        {
            var id = 1;
            var existingAnamnesis = new Anamnesis
            {
                Id = id,
                IsDeleted = false
            };

            _unitOfWorkMock.Setup(u => u.Anamnesis.GetByIdAsync(id)).ReturnsAsync(existingAnamnesis);
            _unitOfWorkMock.Setup(u => u.Anamnesis.Update(existingAnamnesis)).Verifiable();
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _service.DeleteAsync(id);

            Assert.True(result);
            Assert.True(existingAnamnesis.IsDeleted);
            _unitOfWorkMock.Verify(u => u.Anamnesis.Update(existingAnamnesis), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}