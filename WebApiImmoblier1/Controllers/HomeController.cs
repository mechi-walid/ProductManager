using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImmoblierManager.Models.Entities;
using ImmoblierManager.Services.AgentsServices;
using ImmoblierManager.Services.BienImmobilierservices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiImmoblier1.Controllers
{
    public class AgentController : Controller
    {
        private readonly IAgentServices _agentServices;
        private readonly IBienImmobilierServices _benImmobilierServices;
        public AgentController(IAgentServices agentServices, IBienImmobilierServices benImmobilierServices)
        {
            _agentServices = agentServices;
            _benImmobilierServices = benImmobilierServices;
        }

        [HttpGet]
        [Route("{idAgent}", Name = "GetAgent")]

        public async Task<IActionResult> GetAgentByIdAsyn(Guid idAgent)
        {
            var res = await _agentServices.GetAgentByIdAsync(idAgent);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [HttpGet]
        [Route("/Agent/GetAllAgent/")]
        public async Task<IActionResult> GetAllAgents()
        {
            var res = await _agentServices.GetAllAgentAsync();
            if (res == null)
            {
                return NotFound();
            }
            return Ok(new { data = res });
        }

        [HttpGet]
        [Route("{IdAgent}", Name = "DeleteAgent")]
        public async Task<IActionResult> DeleteAgentById([FromBody]Guid idAgent)
        {
            var res = await _agentServices.GetAgentByIdAsync(idAgent);

            if (res == null)
            {
                return NotFound($"Il n'existe pas un agent avec l'id {idAgent}");
            }
            _agentServices.DeleteAgent(res);
            if (await _agentServices.SaveChangeAsync())
            {
                return Ok("Agent supprimé avec succès");
            }
            return BadRequest("Suppression échoué...");
        }
        [HttpGet]
        [Route("{idBienImmoblier}", Name = "GetAgentByIdBienImmoblier")]
        public async Task<IActionResult> GetAgentByBienImmoblier(Guid idBienImmoblier)
        {
            var bienImmob = await _benImmobilierServices.GetBienImmoblierByIdAsync(idBienImmoblier);
            if (bienImmob == null)
            {
                return NotFound("Bien immoblier inexistant !");
            }
            var res = _agentServices.GetAllAgentByAgentByBienImmoblier(bienImmob);

            if (res == null)
            {
                return NotFound($"Il n'existe pas un agent pour cet bien-immoblier ");
            }
            return Ok(new { data = res });
        }
        [HttpGet]
        [Route("{IdAgent}", Name = "UpdateAgent")]
        public async Task<IActionResult> UpdateAgentById([FromBody]Agent agent, Guid idAgent)
        {
            var res = await _agentServices.GetAgentByIdAsync(idAgent);

            if (res == null)
            {
                return NotFound($"Il n'existe pas un agent avec l'id {idAgent}");
            }
            _agentServices.UpdateAgent(agent, idAgent);
            if (await _agentServices.SaveChangeAsync())
            {
                return Ok("Agent modifié avec succès");
            }
            return BadRequest("Modification échoué...");
        }


        public async Task<IActionResult> UpdateAgentById1([FromBody]Agent agent, Guid idAgent)
        {
            var res = await _agentServices.GetAgentByIdAsync(idAgent);

            if (res == null)
            {
                return NotFound($"Il n'existe pas un agent avec l'id {idAgent}");
            }
            _agentServices.UpdateAgent(agent, idAgent);
            if (await _agentServices.SaveChangeAsync())
            {
                return Ok("Agent modifié avec succès");
            }
            return BadRequest("Modification échoué...");
        }

    }
}

