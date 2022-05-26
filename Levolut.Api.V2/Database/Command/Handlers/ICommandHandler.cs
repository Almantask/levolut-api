namespace Levolut.Api.V2.Database.Command.Handlers
{
    public interface ICommandHandler<in TCommand, out TResponse>
    {
        TResponse Handle(TCommand getRuleQuery);
    }
}