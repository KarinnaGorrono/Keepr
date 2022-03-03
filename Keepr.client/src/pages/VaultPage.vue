<template>
  <div class="container-fluid">
    <div class="row d-flex justify-content-end my-5">
      <div class="col-md-10">
        <h1>{{ vault.name }}</h1>
        <p class="m-0">{{ vault.description }}</p>
      </div>
      <div class="col-md-2">
        <!-- WAT?????? -->
        <div>
          <button
            class="selectable btn btn-danger"
            v-if="account.id === vault.creatorId"
            @click="removeVault"
          >
            Delete Vault
          </button>
        </div>

        <!-- v-if="account.id === vault.creatorId" -->
      </div>
    </div>
    <div class="row mt-4">
      <div class="col-md-12 masonry-with-columns">
        <div class="keep" v-for="k in keeps" :key="k.id">
          <div class="content">
            <Keep :keep="k" />
          </div>
        </div>
      </div>
    </div>
    <KeepDetails />
  </div>
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import { vaultsService } from '../services/VaultsService'
import { useRoute, useRouter } from 'vue-router'
import { AppState } from '../AppState'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
export default {
  setup() {
    const router = useRouter()
    const route = useRoute()
    onMounted(async () => {
      try {
        await vaultsService.getVaultById(route.params.id)
        await vaultsService.getKeepsByVaultID(route.params.id)
      } catch (error) {
        router.push({
          name: 'Home'
        })
        Pop.toast(error, 'error')
      }
    })
    return {
      router,
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),


      async removeVault() {
        try {
          if (await Pop.confirm('Are you sure you want to delete this?')) {
            await vaultsService.removeVault(this.vault.id)
            Pop.toast('Vault has been deleted!')

          }
          router.push({
            name: 'Home'
          })
        } catch (error) {
          logger.error(error)
          Pop.toast('Not your vault to delete.')
        }
      }
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