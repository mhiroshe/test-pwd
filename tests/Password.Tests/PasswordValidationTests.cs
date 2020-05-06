using System;
using Xunit;
using Password.Infra.Services;

namespace Password.Tests
{
    public class PasswordValidationTests
    {
        [Fact]
        public void ShouldBeAValidPassword()
        {
            PasswordValidationService service = new PasswordValidationService();
            Assert.True(service.IsValid("D@tabase1"));
        }       

        [Theory]
        [InlineData("Database1")]
        [InlineData("database1")]
        [InlineData("databasea")]
        [InlineData("d@tabase1")]
        [InlineData("d@tabasea")]
        [InlineData("D@tabas1")]
        public void ShouldBeAnInvalidPassword(string value)
        {
            PasswordValidationService service = new PasswordValidationService();
            Assert.False(service.IsValid(value));
        } 
    }
}
