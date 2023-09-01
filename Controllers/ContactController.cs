using matelso.contactmanager.Dto;
using matelso.contactmanager.Entity;
using matelso.contactmanager.Repository;
using Microsoft.AspNetCore.Mvc;

namespace matelso.contactmanager.Controllers;

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
    public async Task Post(CreateUserDto userDto)
    {
        Contact newContact = new(
            userDto.Salutation,
            userDto.Firstname,
            userDto.Lastname,
            userDto.Email,
            userDto.Displayname,
            userDto.Birthddate,
            userDto.PhoneNumber);
        await _repository.CreateAsync(newContact);
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
}
