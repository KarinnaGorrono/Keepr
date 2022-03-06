<template>
  <div
    class="card cardRound text-white elevation-2 selectable"
    @click.stop="setActiveKeep"
  >
    <img :src="keep.img" class="card-img cardRound" alt="Image for keep" />
    <div
      class="card-img-overlay d-flex align-items-end justify-content-between"
    >
      <div class="text-center">
        <h5 class="card-title Text">{{ keep.name }}</h5>
        <button
          title="Delete from vault"
          class="btn btn-danger"
          v-if="route.name === 'Vault' && account.id === vault.creatorId"
          @click="removeKeepFromVault"
        >
          <!-- && account.id === vaultKeeps.creatorId -->
          Remove from Vault
        </button>
      </div>

      <div
        v-if="
          account.id === keep.creatorId &&
          route.name != 'Profile' &&
          route.name != 'Account'
        "
      >
        <div @click.stop="routeToAccount">
          <img
            :src="keep.creator?.picture"
            alt="picture"
            height="40"
            class="rounded"
          />
        </div>
      </div>
      <div
        v-if="
          route.name != 'Profile' &&
          route.name != 'Account' &&
          keep.creatorId != account.id
        "
      >
        <div @click.stop="routeToProfile">
          <img
            :src="keep.creator?.picture"
            alt="picture"
            height="40"
            class="rounded"
          />
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { useRoute, useRouter } from 'vue-router'
import { logger } from '../utils/Logger'
import { Modal } from 'bootstrap'
import { vaultsService } from '../services/VaultsService'
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'

import Pop from '../utils/Pop'


export default {
  props: {
    keep: { type: Object, required: true }
  },
  setup(props) {
    const route = useRoute()
    const router = useRouter()
    return {
      route,
      account: computed(() => AppState.account),
      vault: computed(() => AppState.activeVault),
      async setActiveKeep() {
        try {
          await keepsService.setActiveKeep(props.keep)
          if (document.getElementById("keep-modal")) {
            Modal.getOrCreateInstance(document.getElementById("keep-modal")).toggle()
          }
        } catch (error) {
          logger.error(error)
        }
      },
      routeToProfile() {
        router.push({
          name: 'Profile',
          params: { id: `${props.keep.creatorId}` }
        })
      },
      async removeKeepFromVault() {
        try {
          await vaultsService.removeKeepFromVault(props.keep.vaultKeepId)
          Pop.toast('Keep removed from vault', 'success')
        } catch (error) {
          logger.error(error)
          Pop.toast('error')
        }
      },

      routeToAccount() {
        router.push({
          name: 'Account',
        })
      },




    }
  }
}
</script>


<style lang="scss" scoped>
.Text {
  font-weight: 800;
  color: white;
}
.cardRound {
  border-radius: 0px;
}
.image {
  border-radius: 50px;
}
.name {
  font-size: 1.7vw;
  color: rgb(255, 255, 255);
}
</style>