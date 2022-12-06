using ToDo.Domain.Entities;

namespace ToDo.Domain.Interface
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllAsync();
        Task<Item> GetAsync(Guid id);
        Task AddAsync(Item item);
        Task EditAsync(Item item);
        Task DeleteAsync(Guid id);
    }
}
