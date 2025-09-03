using Healthcare.Application.DTOs.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Healthcare.Application.Validators
{
    public class EnumValueAttribute : ValidationAttribute
    {
        private readonly Type _enumType;

        public EnumValueAttribute(Type enumType)
        {
            _enumType = enumType;
        }

        public override bool IsValid(object? value)
        {
            if (value is EnumValueDto dto)
            {
                return Enum.IsDefined(_enumType, dto.Value);
            }
            if (value != null && value.GetType().IsValueType)
            {
                return Enum.IsDefined(_enumType, value);
            }
            return false;
        }
    }
}