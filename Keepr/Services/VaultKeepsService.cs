using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _vaultKeepsRepo;
        private readonly VaultsRepository _vaultRepo;

        public VaultKeepsService(VaultKeepsRepository vaultKeepsRepo, VaultsRepository vaultRepo)
        {
            _vaultKeepsRepo = vaultKeepsRepo;
            _vaultRepo = vaultRepo;
        }

        internal VaultKeep Create(VaultKeep newVaultKeep, Profile userInfo)
        {
            Vault vault = _vaultRepo.GetVaultById(newVaultKeep.VaultId);
            if (newVaultKeep.CreatorId != userInfo?.Id)
            {
                throw new Exception("error");
            }
            else if (vault.CreatorId != userInfo?.Id)
            {
                throw new Exception("error");
            }
            return _vaultKeepsRepo.Create(newVaultKeep);
        }

        internal List<VaultKeepViewModel> GetKeepsByVaultId(int id, Profile userInfo)
        {

            Vault vault = _vaultRepo.GetVaultById(id);
            if (vault == null)
            {
                throw new Exception("Vault not found");
            }
            else if (vault.CreatorId == userInfo?.Id || !vault.IsPrivate)
            {
                List<VaultKeepViewModel> vaultKeep = _vaultKeepsRepo.GetKeepsByVaultId(id);
                if (vaultKeep == null)
                {
                    throw new Exception("Keep not found");
                }
                return vaultKeep;
            }
            throw new Exception("error");

        }

        internal VaultKeep GetVaultKeepById(int id)
        {
            VaultKeep foundVaultKeep = _vaultKeepsRepo.GetVaultKeepById(id);
            if (foundVaultKeep == null)
            {
                throw new Exception("error");
            }
            return foundVaultKeep;
        }

        internal void Delete(int id, Profile userInfo)
        {
            VaultKeep vaultKeep = GetVaultKeepById(id);
            if (vaultKeep.CreatorId != userInfo.Id)
            {
                throw new Exception("Unauthorized to Delete");
            }
            _vaultKeepsRepo.Delete(id);
        }
    }
}
