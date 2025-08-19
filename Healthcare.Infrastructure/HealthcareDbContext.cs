using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.Infrastructure
{
    public class HealthcareDbContext : DbContext
    {
        public HealthcareDbContext(DbContextOptions options) : base (options)
        {
        }
        
    }
}
