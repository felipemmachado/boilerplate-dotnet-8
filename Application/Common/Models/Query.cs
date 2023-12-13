namespace Application.Common.Models;
public abstract class Query
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
