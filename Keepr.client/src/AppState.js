
import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  keeps: [],
  ActiveUser: {},
  userVaults: [],
  vaults: [],
  activeProfile: {},
  activeProfVaults: [],
  activeProfKeeps: [],
  activeVaultKeeps: [],
  activeVault: {},
  activeKeep: {},
  myVaults: [],
})
