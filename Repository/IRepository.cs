using matelso.contactmanager.Dto;
using matelso.contactmanager.Entity;

namespace matelso.contactmanager.Repository
{
    public interface IRepository
    {
        Task<bool> CreateAsync(Contact entity);
        Task<IEnumerable<ListUserDto>> GetAllAsync();
        Task<ListUserDto> GetAsync(Guid id);
        Task<bool> RemoveAsync(Guid id);
        Task<bool> UpdateAsync(Contact entity);
    }

}