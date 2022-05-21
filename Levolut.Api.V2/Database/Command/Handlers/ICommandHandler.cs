namespace Levolut.Api.V2.Database.Command.Handlers
{
    public interface ICommandHandler<TCommand, TResponse>
        where TCommand : class
        where TResponse : class
    {
        TResponse Handle(TCommand getRuleQuery);
    }
}