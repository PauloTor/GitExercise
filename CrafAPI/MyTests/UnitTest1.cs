
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using craf.Data;
using craf.Models;

namespace MyTests
{
    class ParquePrivateAPIContextMocker
    {
        private static crafContext dbContext;
        public static crafContext GetCraftAPIContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<crafContext>()
                .UseInMemoryDatabase(databaseName: dbName).Options;
            dbContext = new crafContext(options);
            Seed();
            return dbContext;
        }

        public static void Seed()
        {
            dbContext.Search.Add(new Search { SearchID = 1, PostCode = "SN13JY", Longitude = 10, Latitude = 1, Region = "South West", City = "Swindon", DistanceToLondon = 1 });
            dbContext.Search.Add(new Search { SearchID = 2, PostCode = "N76RS", Longitude = 20, Latitude = 2, Region = "South West", City = "Swindon", DistanceToLondon = 2 });
            dbContext.Search.Add(new Search { SearchID = 3, PostCode = "SW1A", Longitude = 30, Latitude = 3, Region = "South West", City = "Swindon", DistanceToLondon = 3 });
            dbContext.Search.Add(new Search { SearchID = 4, PostCode = "W1B3AG", Longitude = 40, Latitude = 4, Region = "South West", City = "Swindon", DistanceToLondon = 4 });
            dbContext.Search.Add(new Search { SearchID = 5, PostCode = "PO63TD", Longitude = 50, Latitude = 5, Region = "South West", City = "Swindon", DistanceToLondon = 5 });

            dbContext.SaveChanges();
        }
    }
}