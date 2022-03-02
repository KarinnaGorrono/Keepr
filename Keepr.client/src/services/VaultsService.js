import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {
    async getUserVaults() {
        const res = await api.get('account/vaults')
        logger.log(res.data)
        AppState.userVaults = res.data
    }

    async getKeepsByVaultID(vaultId) {
        const res = await api.get('api/vaults/' + vaultId + '/keeps')
        logger.log("Keeps in Vaults", res.data)

        AppState.keeps = res.data
    }

    setActiveVault(vault) {
        AppState.activeVault = vault
    }
    async createVault(vault) {
        const res = await api.post('api/vaults', vault)
        logger.log(res.data)
        AppState.userVaults.push(res.data)
    }

    async getProfileVaults(id) {
        const res = await api.get('api/profiles/' + id + '/vaults')
        logger.log("Profile Vaults", res.data)
        AppState.vaults = res.data
    }
    async deleteVault(vaultId) {
        const res = await api.delete('api/vaults/' + vaultId)
        logger.log(res.data)
        AppState.activeVault = {}
    }

    async getVaultById(vaultId) {
        const res = await api.get('api/vaults/' + vaultId)
        logger.log(res.data)
        AppState.activeVault = res.data
    }
    async addKeepToVault(keepId, vaultId) {
        const res = await api.post('api/vaultkeeps', { keepId: keepId, vaultId: vaultId })
        logger.log(res.data)
    }
    async removeKeepFromVault(vaultKeepId) {
        const res = await api.delete('api/vaultkeeps/' + vaultKeepId)
        logger.log(res.data)
        AppState.keeps = AppState.keeps.filter(k => k.vaultKeepId != vaultKeepId)
    }
}
export const vaultsService = new VaultsService()