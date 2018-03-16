
namespace Lands.Backend.Models
{
    using Lands2.Domain;
    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<Lands2.Domain.User> Users { get; set; }
    }
}