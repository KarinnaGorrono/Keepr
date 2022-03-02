<template>
  <div class="container-fluid">
    <div class="row mt-5">
      <div class="col-md-2">
        <img
          class="rounded object-cover-fit"
          :src="activeProfile?.picture"
          alt=""
        />
      </div>
      <div class="col-md-3">
        <div class="row">
          <div class="col-md-12">
            <h1>{{ activeProfile.name?.split("@")[0] }}</h1>
          </div>
          <div class="col-md-12">
            <h4>
              Keeps: <span>{{ keeps?.length }}</span>
            </h4>
            <div class="col-md-12">
              <h4>
                Vaults: <span>{{ vaults?.length }}</span>
              </h4>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="row mt-5">
      <div class="col-md-12">
        <h3>Vaults</h3>
      </div>
      <div class="col-md-12">
        <div class="row ms-4">
          <div class="col-md-2 py-2" v-for="v in vaults" :key="v.id">
            <Vault :vault="v" />
          </div>
        </div>
      </div>
    </div>
    <div class="row mt-5">
      <div class="col-md-12">
        <h3>Keeps</h3>
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
  <KeepDetails />
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import { vaultsService } from '../services/VaultsService'
import { keepsService } from '../services/KeepsService'
import { useRoute } from 'vue-router'
import { accountService } from '../services/AccountService'
import { AppState } from '../AppState'
import Vault from '../components/Vault.vue'

import Pop from '../utils/Pop'
export default {
  components: { Vault },
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await vaultsService.getProfileVaults(route.params.id)
        await keepsService.getUserKeeps(route.params.id)
        await accountService.getProfile(route.params.id)

      } catch (error) {
        logger.error(error)
        Pop.toast(error, 'error')
      }
    })
    return {
      activeProfile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
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