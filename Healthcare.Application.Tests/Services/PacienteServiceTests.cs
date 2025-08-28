using Xunit;
using Moq;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Healthcare.Application.Services;
using Healthcare.Domain.Repositories;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Domain.Entities;
using Healthcare.Application.DTOs.Requests;

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
    public async Task GetAllAsync_ReturnsMappedPacientes()
    {
        // Arrange
        var pacientes = new List<Paciente> { new Paciente() };
        var pacientesDto = new List<PacienteResponseDto> { new PacienteResponseDto() };

        _unitOfWorkMock.Setup(u => u.Pacientes.GetAllAsync()).ReturnsAsync(pacientes);
        _mapperMock.Setup(m => m.Map<IEnumerable<PacienteResponseDto>>(pacientes)).Returns(pacientesDto);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
    {
        _unitOfWorkMock.Setup(u => u.Pacientes.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Paciente?)null);

        var result = await _service.GetByIdAsync(1);

        Assert.Null(result);
    }
}