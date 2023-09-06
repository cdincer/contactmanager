using contactmanager.Dto;
using contactmanager.Entity;
using contactmanager.Repository;
using Microsoft.AspNetCore.Mvc;

namespace contactmanager.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
    private readonly IRepository _repository;

    public ContactController(IRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<bool> Post(CreateUserDto userDto)
    {
        Contact newContact = new(
            userDto.Salutation,
            userDto.Firstname,
            userDto.Lastname,
            userDto.Email,
            userDto.Displayname,
            userDto.Birthdate,
            userDto.PhoneNumber);
        return await _repository.CreateAsync(newContact);
    }

    [HttpGet]
    public async Task<IEnumerable<ListUserDto>> GetAllUser()
    {
        return await _repository.GetAllAsync();
    }

    [HttpGet("GetUser/{guid}")]
    public async Task<ListUserDto> GetUser(Guid guid)
    {
        return await _repository.GetAsync(guid);
    }

    [HttpPut]
    public async Task<bool> UpdateUser(UpdateUserDto userDto)
    {
        Contact newContact = new(
         userDto.Salutation,
         userDto.Firstname,
         userDto.Lastname,
         userDto.Email,
         userDto.Displayname,
         userDto.Birthdate,
         userDto.Phonenumber);
        newContact.Id = userDto.Id;
        return await _repository.UpdateAsync(newContact);
    }

    [HttpDelete]
    public async Task<bool> DeleteUser(Guid guid)
    {
        return await _repository.RemoveAsync(guid);
    }


}
