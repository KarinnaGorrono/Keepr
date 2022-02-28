using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _vaultRepo;

        public VaultsService(VaultsRepository vaultRepo)
        {
            _vaultRepo = vaultRepo;
        }

        internal Vault Create(Vault newVault, Profile userInfo)
        {
            if (newVault.CreatorId != userInfo?.Id)
            {
                throw new Exception("Are you signed in?");
            }
            return _vaultRepo.Create(newVault);
        }

        internal Vault GetVaultById(int id, Profile userInfo)
        {
            Vault foundVault = _vaultRepo.GetVaultById(id);
            if (foundVault == null)
            {
                throw new Exception("Invalid ID");
            }
            if (foundVault.CreatorId != userInfo?.Id && foundVault.IsPrivate)
            {
                throw new Exception("Invalid ID");
            }
            return foundVault;
        }

        internal Vault Edit(Vault updatedVault, string userId)
        {
            Vault originalVault = _vaultRepo.GetVaultById(updatedVault.Id);
            if (originalVault.CreatorId != userId)
            {
                throw new Exception("Unauthorized to Delete");
            }
            else if (originalVault == null)
            {
                throw new Exception("Invalid Id VS");
            }
            updatedVault.CreatorId = updatedVault.CreatorId != null ? updatedVault.CreatorId : originalVault.CreatorId;
            updatedVault.Name = updatedVault.Name != null ? updatedVault.Name : originalVault.Name;
            updatedVault.Description = updatedVault.Description != null ? updatedVault.Description : originalVault.Description;
            updatedVault.IsPrivate = updatedVault.IsPrivate ? updatedVault.IsPrivate : originalVault.IsPrivate;
            return _vaultRepo.Edit(updatedVault);
        }

        internal List<Vault> GetMyVaults(string userId)
        {
            List<Vault> vault = _vaultRepo.GetUsersVaults(userId);
            return vault;
        }

        internal List<Vault> GetUsersVaults(string id, string userId)
        {
            List<Vault> vault = _vaultRepo.GetUsersVaults(id);
            if (userId != id)
            {
                return vault.FindAll(v => v.IsPrivate == false);

            }
            return vault;
        }

        internal void Delete(int id, string userId)
        {
            Vault toDelete = _vaultRepo.GetVaultById(id);
            if (toDelete.CreatorId != userId)
            {
                throw new Exception("Unauthorized to Delete");
            }
            _vaultRepo.Delete(id);
        }
    }
}