<template>
  <div class="container-fluid">
    <div class="row mt-5">
      <div class="col-md-2">
        <img class="rounded object-cover-fit" :src="account.picture" alt="" />
      </div>
      <div class="col-md-3">
        <div class="row">
          <!-- 
 <div class="justified">
            <div class="d-flex">
              <p class="font m-0 username">{{ activeProfile.name }}</p>
              <i -->

          <div class="col-md-12">
            <h1>{{ account.name?.split("@")[0] }}</h1>
          </div>
          <div class="col-md-12">
            <h4>
              Keeps: <span>{{ keeps?.length }}</span>
            </h4>
          </div>
          <div class="col-md-12">
            <h4>
              Vaults: <span>{{ vaults?.length }}</span>
            </h4>
          </div>
        </div>
      </div>
    </div>
    <!-- Vaults start here -->
    <div class="row mt-5">
      <div class="col-md-12">
        <h3>
          Vaults
          <i
            title="create vault"
            class="mdi mdi-plus text-success selectable"
            data-bs-toggle="modal"
            data-bs-target="#create-vault-modal"
          ></i>
          <i> </i>
        </h3>
      </div>
      <div class="col-md-12">
        <div class="row ms-4">
          <div class="col-md-2 my-2" v-for="v in vaults" :key="v.id">
            <Vault :vault="v" />
          </div>
        </div>
      </div>
      <div></div>
    </div>
    <!-- Keeps start here -->
    <div class="row mt-5">
      <div class="col-md-12">
        <h3>
          Keeps
          <i
            data-bs-toggle="modal"
            data-bs-target="#create-keep-modal"
            title="create vault"
            class="mdi mdi-plus text-success selectable"
          ></i>
        </h3>
      </div>
      <div class="col-md-12">
        <div class="row ms-4">
          <div class="col-md-12 masonry-with-columns">
            <div class="keep" v-for="k in keeps" :key="k.id">
              <div>
                <Keep :keep="k" />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <AddNewVault />
  <KeepDetails />
  <AddNewKeep />
</template>


<script>
import { vaultsService } from '../services/VaultsService'
import { computed, onMounted } from '@vue/runtime-core'
import Vault from '../components/Vault.vue'
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'
import Pop from '../utils/Pop'
export default {
  components: { Vault },
  setup() {
    onMounted(async () => {
      try {
        await vaultsService.getVaultsForUser()
        await keepsService.getMyKeeps()
      } catch (error) {
        logger.error(error)
        Pop.toast(error, 'error')
      }
    })
    return {
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.userVaults),
      vaultKeeps: computed(() => AppState.vaultKeeps)
    }
  }
}
</script>


<style lang="scss" scoped>
.masonry-with-columns {
  column-count: 4;
  column-gap: 1em;
}

.keep {
  background-color: #eee;
  display: inline-block;
  margin: 0 0 1em;
  width: 100%;
}
</style>
