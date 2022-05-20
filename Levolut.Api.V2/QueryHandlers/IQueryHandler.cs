namespace Levolut.Api.V2.QueryHandlers
{
    // LSP and ISP compliant.
    // Why is it better than repo?
    // API versioning when?

    // Question about having generic vs non-generic handlers
    // The benefit of this vs repository pattern.
    public interface IQueryHandler<TQuery, TResponse>
        where TQuery : class
        where TResponse : class
    {
        TResponse Handle(TQuery getRuleQuery);
    }
}