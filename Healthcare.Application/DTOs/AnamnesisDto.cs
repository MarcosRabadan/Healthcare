namespace Healthcare.Application.DTOs
{
    public class AnamnesisDto
    {
        public int Id { get; set; }
        public int ConsultaId { get; set; }
        public string AntecedentesPersonales { get; set; } = string.Empty;
        public string AntecedentesFamiliares { get; set; } = string.Empty;
        public string Habitos { get; set; } = string.Empty;
        public string MotivoConsulta { get; set; } = string.Empty;
    }
}