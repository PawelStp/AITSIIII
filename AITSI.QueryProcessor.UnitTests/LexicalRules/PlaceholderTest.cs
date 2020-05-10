using AITSI.QueryProcessor.LexicalRules;
using Xunit;

namespace AITSI.QueryProcessor.UnitTests.LexicalRules
{
    public class PlaceholderTest
    {
        [Theory]
        [InlineData("_")]
        public void ValidatePlaceholder_ShouldReturnTrue(string input)
        {
            ILexicalRules placeholderValidator = new Placeholder();
            bool result = placeholderValidator.validate(input);
            Assert.True(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("1abc")]
        [InlineData("__")]
        [InlineData("a__")]
        [InlineData("__a")]
        public void ValidatePlaceholder_ShouldReturnFalse(string input)
        {
            ILexicalRules placeholderValidator = new Placeholder();
            bool result = placeholderValidator.validate(input);
            Assert.False(result);
        }
    }
}