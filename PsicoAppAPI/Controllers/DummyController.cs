using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.DTOs;
using PsicoAppAPI.Repositories.Interfaces;

namespace PsicoAppAPI.Controllers
{
    public class DummyController : BaseApiController
    {
        private readonly IClientRepository _clientRepository;
        private readonly ISpecialistRepository _specialistRepository;


        public DummyController(IClientRepository clientRepository, ISpecialistRepository specialistRepository)
        {
            _specialistRepository = specialistRepository;
            _clientRepository = clientRepository;
        }

        [HttpPost("get-clients")]
        public async Task<ActionResult> GetClients([FromBody] LoginUserDto loginUserDto)
        {
            var client = await _clientRepository.GetClientById(loginUserDto.Id);
            if (client == null) return BadRequest("Client not found");
            return Ok(client);
        }

        [HttpPost("get-specialists")]
        public async Task<ActionResult> GetSpecialists([FromBody] LoginUserDto loginUserDto)
        {
            var specialist = await _specialistRepository.GetSpecialistById(loginUserDto.Id);
            if (specialist == null) return BadRequest("Client not found");
            return Ok(specialist);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("admin-role-test")]
        public async Task<ActionResult> AdminRoleTest()
        {
            return Ok("Admin role test passed");
        }

        [Authorize(Roles = "CLIENT")]
        [HttpGet("client-role-test")]
        public async Task<ActionResult> ClientRoleTest()
        {
            return Ok("Client role test passed");
        }

        [Authorize(Roles = "SPECIALIST")]
        [HttpGet("specialist-role-test")]
        public async Task<ActionResult> SpecialistRoleTest()
        {
            return Ok("Specialist role test passed");
        }
    }


}