using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        // NOTE GroupUp best ref for sequel strings
        internal VaultKeep Create(VaultKeep newVaultKeep)
        {
            string sql = @"
      UPDATE keeps
      SET keeps = keeps + 1
      WHERE id = @KeepId;
      INSERT INTO vaultkeeps(creatorId, vaultId, keepId)
      VALUES (@CreatorId, @VaultId, @KeepId);
      SELECT LAST_INSERT_ID()
      ;";
            int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
            newVaultKeep.Id = id;
            return newVaultKeep;
        }

        // NOTE Get VaultKeeps @ID error???
        internal List<VaultKeepViewModel> GetKeepsByVaultId(int vaultId)
        {
            string sql = @"
      SELECT 
      k.*,
      vk.id AS vaultKeepId,
      a.*
      FROM vaultkeeps vk
      JOIN  keeps k ON k.id = vk.keepId
      JOIN accounts a ON k.creatorId = a.id  
     WHERE vk.vaultId = @vaultId 
      ;";
            return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (vaultKeep, account) =>
            {
                vaultKeep.Creator = account;
                return vaultKeep;
            }, new { vaultId }).ToList();
        }

        internal VaultKeep GetVaultKeepById(int id)
        {
            string sql = @"SELECT * FROM vaultkeeps WHERE id = @id;";
            return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
        }




        // TODO Got the keeps to decrement!! but now vaultkeep delete is not passing tests  😢
        internal void Delete(int id, int keepId)
        {
            string sql = @"
            UPDATE keeps
            SET
            keeps = keeps -1
            WHERE id = @keepId;
        DELETE FROM vaultkeeps WHERE id = @id LIMIT 1;";
            int rows = _db.Execute(sql, new { id, keepId });
            if (rows == 0)
            {
                throw new Exception("Invalid Id");
            }

        }

    }
}