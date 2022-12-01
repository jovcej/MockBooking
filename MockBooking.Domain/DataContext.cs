using Microsoft.EntityFrameworkCore;
using MockBooking.Domain.Entities.Book;
using MockBooking.Domain.Entities.CheckStatus;
using MockBooking.Domain.Entities.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBooking.Domain.Entities
{
    public class DataContext : DbContext
    {

       
        public DataContext(DbContextOptions<DataContext>
                options) : base(options)
        {

        }

        protected override void OnConfiguring
        (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "MockBooking");
        }

        //Book
        //public DbSet<BookReq> BookReq { get; set; }
        public DbSet<BookedReservation> BookRes { get; set; }

        //Search
        public DbSet<SearchReq> SearchReq { get; set; }
        //public DbSet<SearchRes> SearchRes { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<User> Users {get; set; }

        //CheckStatus
        public DbSet<CheckStatusReq> CheckStatusReq { get; set; }
        public DbSet<CheckStatusRes> CheckStatusRes { get; set; }

    }
}
