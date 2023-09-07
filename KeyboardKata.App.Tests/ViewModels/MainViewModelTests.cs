using KeyboardKata.App.Commands;
using System.Threading.Tasks;
using Xunit;

namespace KeyboardKata.App.Tests.ViewModels
{
    public class MainViewModelTests
    {
        [Fact]
        public void Test()
        {
            int lol = 0;

            Command testCommand = new("TestCommand", () =>
            {
                lol = 10;

                return Task.CompletedTask;
            });

            testCommand.Execute();

            Assert.Equal(10, lol);
        }
    }
}
