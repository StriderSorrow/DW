using DW.Data.Database.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DW.Data.Database
{
    public class DwDbContext : IdentityDbContext<DwUser>
    {
        public DwDbContext(DbContextOptions<DwDbContext> options):base(options) {}

        public DbSet<DwMedia> Medias { get; set; }
        public DbSet<DwTeam> Teams { get; set; }
        public DbSet<DwProject> Projects { get; set; }
        public DbSet<DwTask> Tasks { get; set; }
        public DbSet<DwTranslateHistory> TranslateHistory { get; set; }
        public DbSet<DwScriptLine> ScriptLines { get; set; }
        public DbSet<DwCharacter> Characters { get; set; }
        public DbSet<DwUserConfirmation> Confirmations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DwUser>().HasMany(x => x.Candidate).WithMany(x => x.Candidates);
            builder.Entity<DwUser>().HasMany(x => x.Characters).WithOne(x => x.Actor);
            builder.Entity<DwUser>().HasMany(x => x.Teams).WithMany(x => x.Users);
            builder.Entity<DwUser>().HasMany(x => x.CreatedTeams).WithOne(x => x.Creator);
            builder.Entity<DwUser>().HasMany(x => x.LeadingTeams).WithOne(x => x.Leader);
            builder.Entity<DwUser>().HasMany(x => x.Projects).WithMany(x => x.Participants);
            builder.Entity<DwUser>().HasMany(x => x.CreatedProjects).WithOne(x => x.Creator);
            builder.Entity<DwUser>().HasMany(x => x.LeadingProjects).WithOne(x => x.Leader);
            builder.Entity<DwUser>().HasMany(x => x.ActorsMedias).WithMany();
            builder.Entity<DwUser>().HasMany(x => x.UploadedMedias).WithOne(x => x.Uploader);
            builder.Entity<DwCharacter>().HasMany(x => x.Medias).WithMany();
            builder.Entity<DwProject>().HasMany(x => x.Medias).WithMany();
            builder.Entity<DwTeam>().HasMany(x => x.Medias).WithMany();



            base.OnModelCreating(builder);
        }
    }
}
