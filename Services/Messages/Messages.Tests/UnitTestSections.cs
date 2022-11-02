using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using Rk.Messages.Logic.SectionsNS.Commands.CreateSectionCommand;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Messages.Tests
{
    /// <summary>
    ///Тесты для Sections
    /// </summary>
    public class UnitTestSections : TestBase
    {
        const string rootName = "Рутовая секция";

        [Fact]
        public async Task CreateValidSection_ShouldReturnId()
        {
            // Arrange
            var validator = new Mock<IValidator<CreateSectionCommand>>();            

            validator.Setup(x => x.Validate(It.IsAny<CreateSectionCommand>()))              
                .Returns(new FluentValidation.Results.ValidationResult());

            var handler = new CreateSectionCommandHandler(this._db, validator.Object);

            var command = new CreateSectionCommand { Name = rootName };
            // Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.NotEqual(default(long), result);

            Assert.Equal(1, _db.CatalogSections.Count());

            Assert.Equal(rootName, _db.CatalogSections.First().Name);
        }
    }
}