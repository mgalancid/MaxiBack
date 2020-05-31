using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back.Models;
using Back.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Back.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;

namespace Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository<Users, UsersDTO> _dataRepository;
        private readonly CruzRojaContext _context;

        public UsersController(IUsersRepository<Users, UsersDTO> dataRepository, CruzRojaContext context)
        {
            _dataRepository = dataRepository;
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var users = _dataRepository.GetAllDto();
            return Ok(users);
        }

        // GET: api/users/id
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            var users = _dataRepository.GetDto(id);
            if (users == null)
            {
                return NotFound("User not found.");
            }

            return Ok(users);
        }

        //PUT: api/users/id
        [HttpPut("{id}")]
        public IActionResult PutUsers(int id, Users users)
        {
            if (id != users.IdUsers)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;
            _context.SaveChangesAsync();

            return NoContent();
        }


        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.IdUsers }, users);
        }

        //DELETE: api/users/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

    }
}
