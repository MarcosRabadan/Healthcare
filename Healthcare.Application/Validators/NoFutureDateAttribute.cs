using System;
using System.ComponentModel.DataAnnotations;

namespace Healthcare.Application.Validators
{
    public class NoFutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime date)
            {
                return date <= DateTime.UtcNow;
            }
            return true;
        }
    }
}