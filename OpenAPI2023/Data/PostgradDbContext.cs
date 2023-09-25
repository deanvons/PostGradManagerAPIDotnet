using Microsoft.EntityFrameworkCore;
using OpenAPI2023.Data.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace OpenAPI2023.Data
{
    public class PostgradDbContext : DbContext
    {
        // We need this constructor so DI can create an instance and pass the options in.
        public PostgradDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Professors

            modelBuilder.Entity<Professor>().HasData(
                new Professor() { Id = 1, Name = "William Fences", Field = "Monopoly" },
                new Professor() { Id = 2, Name = "Arvid Strøm", Field = "High voltage electricity" },
                new Professor() { Id = 3, Name = "Anne Hansen", Field = "Soldering while skydiving" }
                );

            // Students

            modelBuilder.Entity<Student>().HasData(
                new Student() { Id = 1, Name = "Ola Nordmann", ProfessorId = 1 },
                new Student() { Id = 2, Name = "Emma Hansen", ProfessorId = 1 },
                new Student() { Id = 3, Name = "Olivia Nordmann", ProfessorId = 2 },
                new Student() { Id = 4, Name = "Lucas Olsen", ProfessorId = 2 },
                new Student() { Id = 5, Name = "Askel Nilsen", ProfessorId = 3 },
                new Student() { Id = 6, Name = "Frida Kristiansen", ProfessorId = 3 },
                new Student() { Id = 7, Name = "Ingrid Johansen", ProfessorId = 3 },
                new Student() { Id = 8, Name = "Jakob Andersen", ProfessorId = 3 }
                );

            // Projects

            modelBuilder.Entity<Project>().HasData(
                new Project() { Id = 1, Title = "Automating your life in 3 easy steps", StudentId = 1 },
                new Project() { Id = 2, Title = "How to build a technology empire", StudentId = 2 },
                new Project() { Id = 3, Title = "Powering the new age", StudentId = 3 },
                new Project() { Id = 4, Title = "Predicting the weather on Mars", StudentId = 5 },
                new Project() { Id = 5, Title = "Classifying Deep Learning classifiers", StudentId = 7 },
                new Project() { Id = 6, Title = "Can we make safe bets? An analysis of card counting", StudentId = 8 }
                );

            // Subjects

            modelBuilder.Entity<Subject>().HasData(
                new Subject() { Id = 1, Code = "ONT4001", Title = "Advanced Programming", ProfessorId = 1 },
                new Subject() { Id = 2, Code = "ELEC1001", Title = "Electroboom", ProfessorId = 2 },
                new Subject() { Id = 3, Code = "SKY2003", Title = "Tig Welding for astronauts", ProfessorId = 3 },
                new Subject() { Id = 4, Code = "AGED4320", Title = "Research Methodologies for Life", ProfessorId = 2 }
                );

            // Student subjects

            modelBuilder.Entity<Student>()
                .HasMany(std => std.Subjects)
                .WithMany(sub => sub.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "StudentSubject",
                    l => l.HasOne<Subject>().WithMany().HasForeignKey("SubjectId"),
                    r => r.HasOne<Student>().WithMany().HasForeignKey("StudentId"),
                    je =>
                    {
                        je.HasKey("StudentId", "SubjectId");
                        je.HasData(
                            new { StudentId = 1, SubjectId = 1 },
                            new { StudentId = 2, SubjectId = 1 },
                            new { StudentId = 2, SubjectId = 2 },
                            new { StudentId = 3, SubjectId = 2 },
                            new { StudentId = 3, SubjectId = 3 },
                            new { StudentId = 4, SubjectId = 3 },
                            new { StudentId = 4, SubjectId = 4 },
                            new { StudentId = 5, SubjectId = 1 },
                            new { StudentId = 5, SubjectId = 4 },
                            new { StudentId = 6, SubjectId = 2 },
                            new { StudentId = 7, SubjectId = 3 },
                            new { StudentId = 8, SubjectId = 4 }
                            );
                    }
                );
        }
    }
}
