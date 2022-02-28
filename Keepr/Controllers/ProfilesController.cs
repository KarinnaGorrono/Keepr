using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _profilesService;
        private readonly KeepsService _keepsService;
        private readonly VaultsService _vaultsService;

        public ProfilesController(ProfilesService profilesService, KeepsService keepsService, VaultsService vaultsService)
        {
            _profilesService = profilesService;
            _keepsService = keepsService;
            _vaultsService = vaultsService;
        }

        [HttpGet("{id}/vaults")]
        public async Task<ActionResult<List<Vault>>> GetUsersVaults(string id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                List<Vault> v = _vaultsService.GetUsersVaults(id, userInfo?.Id);
                return Ok(v);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        [HttpGet("{id}")]
        public ActionResult<Profile> GetByProfileId(string id)
        {
            try
            {
                Profile profile = _profilesService.GetByProfileId(id);
                return Ok(profile);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}/keeps")]
        public ActionResult<List<Keep>> GetUsersKeeps(string id)
        {
            try
            {
                List<Keep> keep = _keepsService.GetUsersKeeps(id);
                return Ok(keep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}