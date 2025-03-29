using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{
    public class FundooDBContext : DbContext
    {
        public FundooDBContext(DbContextOptions options) : base(options)
        {
        }

        //For table creation
        public DbSet<UserEntity> Users { get; set; }

        //Create Table Notes
        public DbSet<NotesEntity> Notes { get; set; }
    }
}
