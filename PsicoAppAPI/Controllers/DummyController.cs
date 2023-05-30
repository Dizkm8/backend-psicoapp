using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.DTOs;
using PsicoAppAPI.Repositories.Interfaces;

namespace PsicoAppAPI.Controllers
{
    public class DummyController : BaseApiController
    {
        private readonly IClientRepository _clientRepository;

        public DummyController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpPost("get-clients")]
        public async Task<IActionResult> GetClients([FromBody] LoginUserDto loginUserDto)
        {
            var client = await _clientRepository.GetClientById(loginUserDto.Id);
            if(client == null) return BadRequest("Client not found");
            return Ok(client);
        }
    }


}