using Healthcare.Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.Application.Constants
{
    public static class ErrorMessages
    {
        public static readonly ErrorResponseDto TipoAlergiaInvalido = new()
        {
            Code = "ERR_TIPO_ALERGIA_INVALIDO",
            Message = "El tipo de alergia no es válido."
        };

        public static readonly ErrorResponseDto EstadoCitaInvalido = new()
        {
            Code = "ERR_ESTADO_CITA_INVALIDO",
            Message = "El estado de la cita no es válido."
        };

        public static readonly ErrorResponseDto EmailYaExiste = new () 
        { 
            Code = "ERR_EMAIL_YA_EXISTE", 
            Message = "El email ya está en uso." };
        }
}
