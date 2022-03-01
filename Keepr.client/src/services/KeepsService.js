import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {
    async getAll() {
        const res = await api.get("api/keeps")
        AppState.keeps = res.data
    }
    async getByVaultKeeps(vaultId) {
        const res = await api.get("api/vaults/" + vaultId + "/keeps")
        AppState.activeVaultKeeps = res.data
    }
    async getKeepById(id) {
        const res = await api.get("api/keeps/" + id)
        AppState.activeKeep = res.data
    }
    async createKeep(newKeep) {
        const res = await api.post("api/keeps", newKeep)
        AppState.activeProfKeeps.push(res.data)
    }
    async removeKeep(id) {
        const res = await api.delete("api/keeps/" + id)
        let newKeeps = AppState.keeps?.filter(k => k.id !== id)
        AppState.keeps = newKeeps
        let newActProfKeeps = AppState.activeProfKeeps?.filter(k => k.id !== id)
        AppState.activeProfKeeps = newActProfKeeps

        let newActvaultKeeps = AppState.activeVaultKeeps?.filter(k => k.id !== id)
        AppState.activeVaultKeeps = newActvaultKeeps
    }
    async addKeepToVault(keepId, vaultId) {
        await api.post("api/vaultkeeps", { keepId: keepId, vaultId: vaultId })
    }
    async removeKeepFromVault(id) {
        const res = await api.delete("api/vaultkeeps/" + id)
        let newVK = AppState.activeVaultKeeps.filter(k => k.vaultKeepId !== id)
        AppState.activeVaultKeeps = newVK
    }


    async editKeep(id, input) {
        const res = await api.put("api/keeps/" + id, input)

        AppState.activeKeep = res.data
    }
}

export const keepsService = new KeepsService()