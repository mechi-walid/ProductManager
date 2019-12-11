using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImmoblierManager.Models.Entities;
using ImmoblierManager.Services.ClientServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiImmoblier1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : Controller
    {

        private readonly IClientServices _clientServices;

        public ClientController(IClientServices clientServices)
        {
            _clientServices = clientServices;
        }

        [HttpGet]
        [Route("{idClient}", Name = "GetClient")]

        public async Task<IActionResult> GetClientByIdAsyn(Guid idClient)
        {
            var res = await _clientServices.GetClientByIdAsync(idClient);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [HttpGet]
        [Route("/Client/GetAllClient/")]
        public async Task<IActionResult> GetAllClients()
        {
            var res = await _clientServices.GetAllClientAsync();
            if (res == null)
            {
                return NotFound();
            }
            return Ok(new { data = res });
        }

        [HttpGet]
        [Route("{IdClient}", Name = "DeleteClient")]
        public async Task<IActionResult> DeleteClientById([FromBody]Guid idClient)
        {
            var res = await _clientServices.GetClientByIdAsync(idClient);

            if (res == null)
            {
                return NotFound($"Il n'existe pas un client avec l'id {idClient}");
            }
            _clientServices.DeleteClient(res);
            if (await _clientServices.SaveChangeAsync())
            {
                return Ok("Client supprimé avec succès");
            }
            return BadRequest("Suppression échoué...");
        }
        [HttpGet]
        [Route("{TypeClient}", Name = "GetClient")]
        //public IActionResult GetClientByType(TypeClient typeClient)
        //{
        //   // var res = _clientServices.GetClientByType(typeClient);

        //    //if (res == null)
        //    //{
        //    //    return NotFound($"Il n'existe pas un client avec de type {typeClient}");
        //    //}
        //    //return Ok(new { data = res });
        //}
        [HttpGet]
        [Route("{IdClient}", Name = "UpdateClient")]
        public async Task<IActionResult> UpdateClientById([FromBody]Client client, Guid idClient)
        {
            var res = await _clientServices.GetClientByIdAsync(idClient);

            if (res == null)
            {
                return NotFound($"Il n'existe pas un client avec l'id {idClient}");
            }
            _clientServices.UpdateClient(client, idClient);
            if (await _clientServices.SaveChangeAsync())
            {
                return Ok("Client modifié avec succès");
            }
            return BadRequest("Modification échoué...");
        }


    }
}