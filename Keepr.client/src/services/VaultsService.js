import { api } from "./AxiosService"
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"

class VaultsService {
    async getVaultsForUser() {
        const res = await api.get('account/vaults')
        logger.log(res.data)
        AppState.userVaults = res.data
    }

    async getKeepsByVaultID(vaultId) {
        const res = await api.get('api/vaults/' + vaultId + '/keeps')
        logger.log("Keeps from the vaults!!", res.data)

        AppState.keeps = res.data
    }

    currentVault(thisVault) {
        AppState.activeVault = thisVault
    }
    async createVault(vault) {
        const res = await api.post('api/vaults', vault)
        logger.log(res.data)
        AppState.userVaults.push(res.data)
    }

    async getVaultsForProfile(PVid) {
        const res = await api.get('api/profiles/' + PVid + '/vaults')
        logger.log("Vaults for profile", res.data)
        AppState.vaults = res.data
    }
    async removeVault(vaultId) {
        const res = await api.delete('api/vaults/' + vaultId)
        logger.log(res.data)
        AppState.activeVault = {}
    }

    async getVaultById(vaultId) {
        const res = await api.get('api/vaults/' + vaultId)
        logger.log(res.data)
        AppState.activeVault = res.data
    }
    async addKeepToVault(keepID, vaultID) {
        const res = await api.post('api/vaultkeeps', { keepID: keepID, vaultID: vaultID })
        logger.log(res.data)
    }
    async removeKeepFromVault(vaultKeepId) {
        const res = await api.delete('api/vaultkeeps/' + vaultKeepId)
        logger.log(res.data)
        AppState.keeps = AppState.keeps.filter(k => k.vaultKeepId != vaultKeepId)
    }
}
export const vaultsService = new VaultsService()