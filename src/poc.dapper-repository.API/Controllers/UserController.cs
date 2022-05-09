using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using poc.dapper_repository.API.Dtos.Request;
using poc.dapper_repository.Domain.Entities;
using poc.dapper_repository.Domain.Interfaces;

namespace poc.dapper_repository.API.Controllers;

[ApiController]
[Route("Users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserResquestDto userRequestDto)
    {
        var user = new User(userRequestDto.Name, userRequestDto.LastName, userRequestDto.Age);

        var result = await _userService.AddUser(user);

        return result == null ? BadRequest() : Ok(user);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var result = await _userService.GetUserById(id);
        
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        return Ok(await _userService.GetAllUsers());
    }


    [HttpDelete]
    public async Task<IActionResult> RemoveUser(Guid id)
    {
        var result = await _userService.RemoveUser(id);

        return result ? Ok() : NotFound();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUser(UserResquestDto newUserDto, Guid id)
    {
        var user = new User(newUserDto.Name, newUserDto.LastName, newUserDto.Age);
        var result = await _userService.UpdateUser(user, id);

        return result ? NoContent() : BadRequest();
    }
}
