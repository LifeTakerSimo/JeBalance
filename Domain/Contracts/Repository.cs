namespace Domain.Contracts;

public interface Repository<T> where T : Entity
{
    public Task<(IEnumerable<T> Results, int Total)> Find(int limit, int offset, Specification<T> specification);
    public Task<int> Count(Specification<T> specification);
    public Task<bool> HasAny(Specification<T> specification);
    public Task<T> GetOne(int id);
    public Task<int> Create(T T);
    public Task<int> Update(int id, T T);
    public Task<bool> Delete(int id);
}