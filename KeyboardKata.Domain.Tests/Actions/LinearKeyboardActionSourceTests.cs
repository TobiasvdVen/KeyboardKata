using KeyboardKata.Domain.Actions;
using KeyboardKata.Domain.Tests.Helpers;
using System.Collections.Generic;
using Xunit;

namespace KeyboardKata.Domain.Tests.Actions
{
    public class LinearKeyboardActionSourceTests
    {
        [Fact]
        public void GivenMultipleActions_WhenRequested_ReturnOneAfterTheOther()
        {
            List<KeyboardAction> actions = new()
            {
                Stubs.Action("Do the thing!", Stubs.Down("A")),
                Stubs.Action("Do the other thing!", Stubs.Down("U"))
            };

            LinearKeyboardActionSource actionSource = new(actions);

            KeyboardAction first = actionSource.GetKeyboardAction();
            KeyboardAction second = actionSource.GetKeyboardAction();

            Assert.Equal(actions[0], first);
            Assert.Equal(actions[1], second);
        }

        [Fact]
        public void GivenMultipleActions_WhenMoreRequested_ReturnBackToTheFirst()
        {
            List<KeyboardAction> actions = new()
            {
                Stubs.Action("Do the thing!", Stubs.Down("A")),
                Stubs.Action("Do the other thing!", Stubs.Down("U"))
            };

            LinearKeyboardActionSource actionSource = new(actions);

            KeyboardAction first = actionSource.GetKeyboardAction();
            KeyboardAction second = actionSource.GetKeyboardAction();
            KeyboardAction third = actionSource.GetKeyboardAction();

            Assert.Equal(actions[0], first);
            Assert.Equal(actions[1], second);
            Assert.Equal(actions[0], third);
        }
    }
}
