using KeyboardKata.Domain.Actions;
using KeyboardKata.Domain.Actions.Pools;
using KeyboardKata.Domain.Tests.Helpers;
using Xunit;

namespace KeyboardKata.Domain.Tests.Actions
{
    public class KeyboardActionSourceTests
    {
        private readonly KeyboardActionSourceFactory _factory;

        public KeyboardActionSourceTests()
        {
            _factory = new KeyboardActionSourceFactory();
        }

        [Fact]
        public void GivenLinear_WhenActionsRequested_ReturnOneAfterTheOther()
        {
            KeyboardAction action1 = Stubs.Action("Do the thing!", Stubs.Down("A"));
            KeyboardAction action2 = Stubs.Action("Do the other thing!", Stubs.Down("U"));

            LinearActionPool actions = Stubs.Linear(
                Stubs.Single(action1),
                Stubs.Single(action2)
            );

            IKeyboardActionSource actionSource = _factory.Create(actions);

            KeyboardAction? first = actionSource.GetKeyboardAction();
            KeyboardAction? second = actionSource.GetKeyboardAction();

            Assert.Equal(action1, first);
            Assert.Equal(action2, second);
        }

        [Fact]
        public void GivenLinear_WhenMoreActionsRequested_ReturnNull()
        {
            KeyboardAction action1 = Stubs.Action("Do the thing!", Stubs.Down("C"));
            KeyboardAction action2 = Stubs.Action("Do the other thing!", Stubs.Down("K"));

            LinearActionPool actions = Stubs.Linear(
                Stubs.Single(action1),
                Stubs.Single(action2)
            );

            IKeyboardActionSource actionSource = _factory.Create(actions);

            _ = actionSource.GetKeyboardAction();
            _ = actionSource.GetKeyboardAction();
            KeyboardAction? third = actionSource.GetKeyboardAction();

            Assert.Null(third);
        }
    }
}
