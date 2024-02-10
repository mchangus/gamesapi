using Games.Domain.Models;
using Games.Domain.Validators;

namespace GamesApi.Tests.Validators
{
    public class SearchValidatorTests
    {
        [Fact]
        public void SearchValidator_validSearchOptions_ShouldPassValidation() 
        {
            // Arrange
            var searchOptions = new List<string> { "name", "-name", "rating", "-rating" };

            var validator = new SearchValidator(searchOptions);

            var search = new Search
            {
                Query = "Mario",
                Sort = "name"
            };

            // Act
            var result = validator.Validate(search);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void SearchValidator_queryEmpty_ShouldFailValidation()
        {
            // Arrange
            var searchOptions = new List<string> { "name", "-name", "rating", "-rating" };

            var validator = new SearchValidator(searchOptions);

            var search = new Search
            {
                Sort = "name"
            };

            // Act
            var result = validator.Validate(search);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void SearchValidator_InvalidOrderingParameter_ShouldFailValidation()
        {
            // Arrange
            var searchOptions = new List<string> { "name", "-name", "rating", "-rating" };

            var validator = new SearchValidator(searchOptions);

            var search = new Search
            {
                Query = "Mario",
                Sort = "lastName"
            };

            // Act
            var result = validator.Validate(search);

            // Assert
            Assert.False(result.IsValid);
        }
    }
}
