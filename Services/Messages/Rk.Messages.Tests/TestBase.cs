using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Rk.Messages.Infrastructure.EFCore;
using Rk.Messages.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Tests
{
    /// <summary>
    /// Базовый класс теста с инкапсулированным DbContext
    /// </summary>
    public class TestBase
    {
        protected readonly AppDbContext _db;

        protected readonly Mock<IUserService> _mockUserService;

        public TestBase()
        {

            _mockUserService = new Mock<IUserService>();

            _mockUserService.Setup(x=>x.IsAuthenticated).Returns(true);
            _mockUserService.Setup(x => x.UserName).Returns("Иванов");
            


            var uniqueId = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<AppDbContext>()
                          .UseInMemoryDatabase(databaseName: uniqueId)
                          .Options;

            _db = new AppDbContext(options, _mockUserService.Object);

            _db.Database.EnsureCreated();

        }



        protected virtual void PrepareData()
        {
        }



        public void Dispose()
        {

            _db.Database.EnsureDeleted();

            _db.Dispose();

        }
    }
}

