namespace Levolut.Api.V3.Domain.DomainServices.Command
{
    public interface ICommandHandler<in TCommand, out TResponse>
    {
        TResponse Handle(TCommand getRuleQuery);
    }
}