using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.Application.Validators
{
    internal class NoPastDate : ValidationAttribute
    {
        public override bool IsValid(object? value)
        { 
            if (value is DateTime date)
            {
                return date >= DateTime.UtcNow;
            }
            return true;
        }
    }
}
