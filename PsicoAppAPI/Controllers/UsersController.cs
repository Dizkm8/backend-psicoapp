using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PsicoAppAPI.Data;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context) => _context = context;

        /// <summary>
        /// Get all users in database context
        /// </summary>
        /// <returns>All users collected</returns>
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }
         /// <summary>
        /// checks if the user exists in the database and if the entered password matches the one registered in the database
        /// </summary>
        /// <returns>user whose login credentials match</returns>
        [HttpGet]
        [Route("login")]
        public IActionResult Login(string id, string password)
        {
            if (UserExists(id)){
                if (PasswordExists(password)){
                    var user = _context.Users.FirstOrDefault(x => x.Rut == id);
                    return Ok(user);
                }
                else{
                    // if the password does not match returns not found error message(TEMPORALY MUST FIX LATER) //
                    return NotFound();
                }
            }  
            else{
                // if the id is no registered returns not found error message(TEMPORALY MUST FIX LATER) //
                return NotFound();

            }
        }

        /// <summary>
        /// Add a user in database context if user´s id is not registered in the data base
        /// </summary>
        /// <param name="user">User to add</param>
        /// <returns>User saved</returns>
        [HttpPost]
        [Route("sign-up")]
        public IActionResult AddUser(User user)
        {
            if (!UserExists(user.Rut)){
                _context.Users.Add(user);
                _context.SaveChanges();
                return Ok(user);
            }
            else{
                return NotFound();
                // if the id is already registered returns not found error message(TEMPORALY MUST FIX LATER) //
            }
        }

        /// <summary>
        /// Get a user by rut in database context
        /// </summary>
        /// <param name="rut">Rut identifier</param>
        /// <returns>User finded</returns>
        [HttpGet("{rut}")]
        public IActionResult GetUser(string rut)
        {
            var user = _context.Users.FirstOrDefault(x => x.Rut == rut);
            return Ok(user);
        }
        



        /// <summary>
        /// Change user status to enabled or disabled by their rut
        /// </summary>
        /// <param name="rut">user rut</param>
        /// <param name="isEnabled">enabled status or not</param>
        /// <returns>Task</returns>
        [HttpPut("{rut}/{isEnabled}")]
        public async Task<IActionResult> ChangeUserStatus( string rut, bool isEnabled)
        {
            if (!UserExists(rut))
            {
                return NotFound();
            }
            _context.Find<User>(rut).IsEnabled = isEnabled;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }
        
    
        [HttpPut("{rut}")]
        public async Task<IActionResult> UpdateUser(string rut, User user)
        {
            if (rut != user.Rut)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(rut))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Checks if exists a user by their rut
        /// </summary>
        /// <param name="rut">user rut</param>
        /// <returns>True if exists</returns>
        private bool UserExists(string rut)
        {
            return _context.Users.Any(e => e.Rut == rut);
        }

        private bool PasswordExists(string password)
        {
            return _context.Users.Any(e => e.Password == password);
        }
    }
}