namespace EasyEnglish.Web.Tests
{
    using System;

    using EasyEnglish.Web.Infrastructure.ValidationAttributes;
    using Xunit;

    public class CurrentYearMaxValueAttributeTests
    {
        [Fact]
        public void IsValidReturnsFalseForDateTimeAfterCurrentYear()
        {
            var attribute = new CurrentYearMaxValueAttribute(1990);

            var isValid = attribute.IsValid(DateTime.UtcNow.AddYears(1));

            Assert.False(isValid);
        }

        [Fact]
        public void IsValidReturnsFalseForYearAfterCurrentYear()
        {
            var attribute = new CurrentYearMaxValueAttribute(1990);

            var isValid = attribute.IsValid(DateTime.UtcNow.AddYears(1).Year);

            Assert.False(isValid);
        }

        [Fact]
        public void IsValidReturnsTrueForYearBeforeCurrentYear()
        {
            var attribute = new CurrentYearMaxValueAttribute(1990);

            var isValid = attribute.IsValid(DateTime.UtcNow.AddYears(-1).Year);

            Assert.True(isValid);
        }

        [Fact]
        public void IsValidReturnsTrueForDateTimeBeforeCurrentYear()
        {
            var attribute = new CurrentYearMaxValueAttribute(1990);

            var isValid = attribute.IsValid(DateTime.UtcNow.AddYears(-1));

            Assert.True(isValid);
        }
    }
}
