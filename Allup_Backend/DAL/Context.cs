using System;
using Microsoft.EntityFrameworkCore;

namespace Allup_Backend.DAL
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options):base(options)
        {

        }
        
    }
}
