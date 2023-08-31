using matelso.contactmanager.Dto;
using matelso.contactmanager.Entity;

namespace matelso.contactmanager.Repository
{
    public interface IRepository
    {
        Task CreateAsync(Contact entity);
        Task<IEnumerable<ListUserDto>> GetAllAsync();
        Task<ListUserDto> GetAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Contact entity);
    }

}