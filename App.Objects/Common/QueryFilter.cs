namespace App.Objects.Common;

public abstract class QueryFilter<T>
{
    public abstract IQueryable<T> ApplyFilter(IQueryable<T> query);
}