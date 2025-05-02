using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
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

        //For create User table 
        public DbSet<UserEntity> Users { get; set; }

        //Create Table Notes
        public DbSet<NotesEntity> Notes { get; set; }

        //Create Collaborators table
        public DbSet<CollaboratorsEntity> Collaborators { get; set; }

        //Create Labels table
        public DbSet<LabelEntity> Labels { get; set; }

        //Create NoteLabels table
        public DbSet<NoteLabelEntity> NoteLabels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many-to-Many relationship between NoteEntity and LabelEntity
            modelBuilder.Entity<NoteLabelEntity>()
                .HasKey(nl => new { nl.NoteId, nl.LabelId });

            modelBuilder.Entity<NoteLabelEntity>()
                .HasOne(nl => nl.Note)
                .WithMany(n => n.NoteLabels)
                .HasForeignKey(nl => nl.NoteId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<NoteLabelEntity>()
                .HasOne(nl => nl.Label)
                .WithMany(l => l.NoteLabels)
                .HasForeignKey(nl => nl.LabelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CollaboratorsEntity>()
                .HasOne(c => c.CollaborateNote)
                .WithMany(n => n.Collaborators)
                .HasForeignKey(c => c.NoteId)
                .OnDelete(DeleteBehavior.Cascade);


        }

        /*protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Labels_Users_NoteUserUserId",
                table: "Labels");

            migrationBuilder.DropIndex(
                name: "IX_Labels_NoteUserUserId",
                table: "Labels");

            migrationBuilder.DropColumn(
                name: "NoteUserUserId",
                table: "Labels");
        }*/

    }


}

