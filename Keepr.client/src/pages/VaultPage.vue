<template>
  <div class="container-fluid">
    <div class="row text-center my-5">
      <h1>{{ vault.name }}</h1>
      <p class="m-0">{{ vault.description }}</p>
      <i class="selectable mdi mdi-trash-can" @click="removeVault"></i>
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

      async removeVault() {
        try {
          if (await Pop.confirm('Are you sure you want to delete this vault?')) {
            await vaultsService.deleteVault(this.vault.id)
            Pop.toast('Vault Deleted!')

          }
          router.push({
            name: 'Home'
          })
        } catch (error) {
          logger.error(error)
          Pop.toast('This is not your vault to delete.')
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