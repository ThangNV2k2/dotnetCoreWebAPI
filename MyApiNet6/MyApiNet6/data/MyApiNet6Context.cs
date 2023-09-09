using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyApiNet6.data;

namespace MyApiNet6.Data
{
    public class MyApiNet6Context : DbContext
    {
        public MyApiNet6Context (DbContextOptions<MyApiNet6Context> options)
            : base(options)
        {
        }

        public DbSet<MyApiNet6.data.Book> Book { get; set; } = default!;
    }
}
