using MediatR;
using System.Threading.Tasks;

namespace KeyboardKata.App.Commands
{
    public class MediatrCommandProcessor : ICommandProcessor
    {
        private readonly IMediator _mediator;

        public MediatrCommandProcessor(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task ProcessAsync(Command command)
        {
            return _mediator.Send(command);
        }
    }
}
