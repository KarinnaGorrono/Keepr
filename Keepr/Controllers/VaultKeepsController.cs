using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vaultKeepsService;

        public VaultKeepsController(VaultKeepsService vaultKeepsService)
        {
            _vaultKeepsService = vaultKeepsService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVaultKeep)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newVaultKeep.CreatorId = userInfo?.Id;
                VaultKeep vaultKeep = _vaultKeepsService.Create(newVaultKeep, userInfo);
                newVaultKeep.Creator = userInfo;
                return Ok(vaultKeep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            //     Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
            //     newVaultKeep.CreatorId = userInfo.Id;
            //     _vaultKeepsService.AddKeep(newVaultKeep.KeepId);
            //     VaultKeep vaultKeep = _vaultKeepsService.Create(newVaultKeep);
            //     return Ok(vaultKeep);
            //   }
            //   catch (System.Exception e)
            //   {
            //     return BadRequest(e.Message);
            //   }
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<String>> Delete(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                _vaultKeepsService.Delete(id, userInfo);
                return Ok("Deleted!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}