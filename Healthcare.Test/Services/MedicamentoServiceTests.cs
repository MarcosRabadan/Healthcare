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
    public class MedicamentoServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly MedicamentoService _service;

        public MedicamentoServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _service = new MedicamentoService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            _unitOfWorkMock.Setup(u => u.Medicamentos.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Medicamento?)null);

            var result = await _service.GetByIdAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMappedMedicamento_WhenFound()
        {
            var medicamento = new Medicamento { Id = 1 };
            var medicamentoDto = new MedicamentoResponseDto { Id = 1 };

            _unitOfWorkMock.Setup(u => u.Medicamentos.GetByIdAsync(1)).ReturnsAsync(medicamento);
            _mapperMock.Setup(m => m.Map<MedicamentoResponseDto>(medicamento)).Returns(medicamentoDto);

            var result = await _service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedMedicamentos()
        {
            var medicamentos = new List<Medicamento> { new Medicamento { Id = 1 } };
            var medicamentosDto = new List<MedicamentoResponseDto> { new MedicamentoResponseDto { Id = 1 } };

            _unitOfWorkMock.Setup(u => u.Medicamentos.GetAllAsync()).ReturnsAsync(medicamentos);
            _mapperMock.Setup(m => m.Map<IEnumerable<MedicamentoResponseDto>>(It.IsAny<IEnumerable<Medicamento>>())).Returns(medicamentosDto);

            var result = await _service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task CreateAsync_ReturnsCreatedMedicamento()
        {
            var request = new MedicamentoRequestDto
            {
            };

            var medicamento = new Medicamento
            {
                Id = 1
            };

            var medicamentoDto = new MedicamentoResponseDto
            {
                Id = 1
            };

            _mapperMock.Setup(m => m.Map<Medicamento>(request)).Returns(medicamento);
            _unitOfWorkMock.Setup(u => u.Medicamentos.AddAsync(medicamento)).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);
            _mapperMock.Setup(m => m.Map<MedicamentoResponseDto>(medicamento)).Returns(medicamentoDto);

            var result = await _service.CreateAsync(request);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsFalse_WhenMedicamentoNotFound()
        {
            _unitOfWorkMock.Setup(u => u.Medicamentos.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Medicamento?)null);

            var result = await _service.UpdateAsync(1, new MedicamentoRequestDto());

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsTrue_WhenMedicamentoUpdatedSuccessfully()
        {
            var id = 1;
            var existingMedicamento = new Medicamento { Id = id };

            var request = new MedicamentoRequestDto
            {
            };

            _unitOfWorkMock.Setup(u => u.Medicamentos.GetByIdAsync(id)).ReturnsAsync(existingMedicamento);
            _mapperMock.Setup(m => m.Map(request, existingMedicamento)).Verifiable();
            _unitOfWorkMock.Setup(u => u.Medicamentos.Update(existingMedicamento)).Verifiable();
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _service.UpdateAsync(id, request);

            Assert.True(result);
            _mapperMock.Verify(m => m.Map(request, existingMedicamento), Times.Once);
            _unitOfWorkMock.Verify(u => u.Medicamentos.Update(existingMedicamento), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalse_WhenMedicamentoNotFound()
        {
            _unitOfWorkMock.Setup(u => u.Medicamentos.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Medicamento?)null);

            var result = await _service.DeleteAsync(1);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsTrue_WhenMedicamentoDeletedSuccessfully()
        {
            var id = 1;
            var existingMedicamento = new Medicamento
            {
                Id = id,
                IsDeleted = false
            };

            _unitOfWorkMock.Setup(u => u.Medicamentos.GetByIdAsync(id)).ReturnsAsync(existingMedicamento);
            _unitOfWorkMock.Setup(u => u.Medicamentos.Update(existingMedicamento)).Verifiable();
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _service.DeleteAsync(id);

            Assert.True(result);
            Assert.True(existingMedicamento.IsDeleted);
            _unitOfWorkMock.Verify(u => u.Medicamentos.Update(existingMedicamento), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}