<template>
  <div
    class="card carddiv text-white elevation-2 selectable"
    @click.stop="setActiveKeep"
  >
    <img :src="keep.img" class="card-img carddiv" alt="keep img" />
    <div
      class="
        card-img-overlay
        grad
        d-flex
        align-items-end
        justify-content-between
      "
    >
      <div class="text-center">
        <h5 class="card-title title-text">{{ keep.name }}</h5>
        <button
          v-if="route.name === 'Vault'"
          title="remove from vault"
          class="btn btn-danger"
          @click="removeKeepFromVault"
        >
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
            alt="user photo"
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
            alt="user photo"
            height="40"
            class="rounded"
          />
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { Modal } from 'bootstrap'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { useRoute, useRouter } from 'vue-router'
import { vaultsService } from '../services/VaultsService'

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
      routeToAccount() {
        router.push({
          name: 'Account',
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
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.grad {
  background-image: linear-gradient(black);
}
.title-text {
  font-weight: 800;
  color: white;
  text-shadow: -1px -1px 0 #000, 1px -1px 0 #000, -1px 1px 0 #000,
    1px 1px 0 #000;
}
.carddiv {
  border-radius: 0px;
}
.image {
  border-radius: 50px;
}
.name {
  font-size: 1.7vw;
  color: rgb(255, 255, 255);
}
.profpic {
  height: 2.3vw;
  width: 2.3vw;
  border-radius: 50%;
  object-fit: cover;
}
@media only screen and (max-width: 500px) {
  .name {
    font-size: 2vh;
  }
  .profpic {
    height: 3vh;
    width: 3vh;
  }
}
</style>