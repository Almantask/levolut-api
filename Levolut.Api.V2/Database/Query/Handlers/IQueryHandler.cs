namespace Levolut.Api.V2.Database.Query.Handlers
{
    // ISP compliant.
    // Why is it better than repo?

    // Question about having generic vs non-generic handlers
    // The benefit of this vs repository pattern.
    public interface IQueryHandler<in TQuery, out TResponse>
    {
        TResponse Handle(TQuery getRuleQuery);
    }
}