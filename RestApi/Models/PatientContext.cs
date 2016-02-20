using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using RestApi.Interfaces;

namespace RestApi.Models
{
    public class PatientContext : DbContext, IDatabaseContext
    {

        public PatientContext()
            : base("PatientContext")
        {
            Database.SetInitializer<PatientContext>(null);
        }

        public IDbSet<Patient> Patients { get; set; }
        public IDbSet<Episode> Episodes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}