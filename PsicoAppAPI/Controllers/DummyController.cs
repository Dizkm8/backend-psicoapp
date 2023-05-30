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
            if(client == null) return BadRequest("Client not found");
            return Ok(client);
        }

        [HttpPost("get-specialists")]
        public async Task<ActionResult> GetSpecialists([FromBody] LoginUserDto loginUserDto)
        {
            var specialist = await _specialistRepository.GetSpecialistById(loginUserDto.Id);
            if(specialist == null) return BadRequest("Client not found");
            return Ok(specialist);
        }
    }


}