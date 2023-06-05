using KeyboardKata.Domain.Tests.Helpers;
using System.Linq;
using Xunit;

namespace KeyboardKata.Domain.Tests
{
    public class InputStreamTests
    {
        [Fact]
        public void GivenSingleInput_WhenFirst_ThenReturnThatInput()
        {
            Input input = Stubs.Down("B");

            InputStream stream = new(input);

            Assert.Equal(input, stream.First());
        }

        [Fact]
        public void GivenTwoInputs_WhenNextTwice_ThenReturnBothInputs()
        {
            Input down = Stubs.Down("C");
            Input up = Stubs.Up("C");

            InputStream stream = new(down, up);

            Input first = stream.Next();
            Input second = stream.Next();

            Assert.Equal(down, first);
            Assert.Equal(up, second);
        }
    }
}
