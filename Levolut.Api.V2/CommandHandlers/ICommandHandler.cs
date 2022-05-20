namespace Levolut.Api.V2.CommandHandlers
{
    public interface ICommandHandler<TCommand, TResponse>
        where TCommand : class
        where TResponse : class
    {
        TResponse Handle(TCommand getRuleQuery);
    }
}