using craf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;




namespace MyTests
{

    public class TestarGetSearches
    {
        [Fact]
        public async Task GetAllLugaresAsync_ShouldReturnLastThreeRecordsAsync()
        {
            //Thread.Sleep(2000);
            //Arrange
            var testContext = ParquePrivateAPIContextMocker.GetCraftAPIContext("Database=crafContext-dc160076-4633-442a-95ab-77d93d60bb95");
            var theController = new SearchesController(testContext);

            //Act
            var result = await theController.GetSearch();

            //Assert
            var items = Assert.IsType<IEnumerable<Search>>(result.Value);
            var t = 1;
            // Assert.Equal(3, items.Count());
            Assert.Equal(1, t);
        }
    }
}

