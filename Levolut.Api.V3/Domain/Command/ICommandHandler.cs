namespace Levolut.Api.V3.Domain.Command
{
    public interface ICommandHandler<in TCommand, out TResponse>
    {
        TResponse Handle(TCommand getRuleQuery);
    }
}