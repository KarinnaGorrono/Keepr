using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _keepsRepo;

        public KeepsService(KeepsRepository keepsRepo)
        {
            _keepsRepo = keepsRepo;
        }
        internal Keep Create(Keep newKeep)
        {
            return _keepsRepo.Create(newKeep);
        }

        internal object GetAllKeeps()
        {
            return _keepsRepo.GetAllKeeps();
        }

        internal Keep GetKeepById(int id)
        {
            Keep found = _keepsRepo.GetKeepById(id);
            if (found == null)
            {
                throw new Exception("INVALID ID");
            }
            return found;
        }

        internal Keep Edit(Keep updatedKeep, string userId)
        {
            Keep originalKeep = GetKeepById(updatedKeep.Id);
            if (originalKeep.CreatorId != userId)
            {
                throw new Exception("Unauthorized to delete");
            }

            updatedKeep.CreatorId = updatedKeep.CreatorId != null ? updatedKeep.CreatorId : originalKeep.CreatorId;
            updatedKeep.Name = updatedKeep.Name != null ? updatedKeep.Name : originalKeep.Name;
            updatedKeep.Description = updatedKeep.Description != null ? updatedKeep.Description : originalKeep.Description;
            updatedKeep.Img = updatedKeep.Img != null ? updatedKeep.Img : originalKeep.Img;
            return _keepsRepo.Edit(updatedKeep);
        }

        internal List<Keep> GetUsersKeeps(string id)
        {
            return _keepsRepo.GetUsersKeeps(id);
        }

        internal void Delete(int id, string userId)
        {
            Keep toDelete = GetKeepById(id);
            if (toDelete.CreatorId != userId)
            {
                throw new Exception("Unauthorized to delete");
            }
            _keepsRepo.Delete(id);
        }
    }
}