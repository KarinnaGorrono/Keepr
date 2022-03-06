<template>
  <Modal id="keep-modal">
    <template #modal-body>
      <div class="container-fluid">
        <div></div>
        <div class="row">
          <div class="col-md-6 mb-3">
            <img
              height="500"
              class="w-100 object-fit-cover rounded"
              :src="keep.img"
              :alt="keep.title"
              :title="keep.title"
            />
          </div>
          <div class="col-6 text-center d-flex align-items-between">
            <div class="row">
              <div class="col-md-12 mb-4">
                <div class="row">
                  <p class="m-0 col-md-4" title="views">
                    <i class="mdi mdi-eye text-success"></i>
                    {{ keep.views }}
                  </p>

                  <p class="m-0 col-md-4" title="vaults">
                    <i class="mdi mdi-alpha-k-circle text-success"></i>
                    {{ keep.keeps }}
                  </p>
                </div>
              </div>
              <div class="info col-md-12">
                <h2>
                  {{ keep.name }}
                </h2>
                <p>{{ keep.description }}</p>
              </div>
              <div class="col-md-12 align-self-end">
                <div class="row pb-3 justify-content-between align-items-end">
                  <div class="col-md-4" v-if="account.id">
                    <button
                      class="btn dropdown-toggle"
                      type="button"
                      id="dropdownMenu"
                      data-bs-toggle="dropdown"
                      title="add"
                      aria-expanded="false"
                    >
                      Add to Vault
                    </button>
                    <ul class="dropdown-menu" aria-labelledby=" Menu">
                      <li
                        @click="addKeepToVault(v.id)"
                        v-for="v in userVaults"
                        :key="v.id"
                        class="selectable my-1"
                      >
                        <p>{{ v.name }}</p>
                      </li>
                    </ul>
                  </div>

                  <div class="col-md-1">
                    <!-- ----------------------- -->
                    <i
                      title="delete keep"
                      class="mdi mdi-trash-can mdi-24px selectable"
                      @click="removeKeep"
                      v-if="account.id === keep.creatorId"
                    ></i>
                    <!-- ----------------------------------- -->
                  </div>

                  <div class="col-md-4">
                    <div class="row align-items-end">
                      <div
                        class="p-0 d-flex flex-column"
                        v-if="account.id === keep.creatorId"
                      >
                        <router-link to="/account">
                          <img
                            :src="keep.creator?.picture"
                            alt="picture"
                            height="40"
                            class="rounded"
                          />
                          <p>
                            {{ keep.creator?.name.split("@")[0].toUpperCase() }}
                          </p>
                        </router-link>
                      </div>
                      <div v-else>
                        <router-link
                          :to="{
                            name: 'Profile',
                            params: { id: keep.creatorId },
                          }"
                        >
                          <img
                            :src="keep.creator?.picture"
                            alt="picture"
                            height="40"
                            class="rounded"
                          />
                          <p>
                            {{ keep.creator?.name.split("@")[0].toUpperCase() }}
                          </p>
                        </router-link>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { Modal } from 'bootstrap'
import { vaultsService } from '../services/VaultsService'
import { computed } from '@vue/reactivity'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
export default {
  setup() {

    return {
      account: computed(() => AppState.account),
      keep: computed(() => AppState.activeKeep),
      userVaults: computed(() => AppState.userVaults),

      async addKeepToVault(vaultId) {
        try {
          await vaultsService.addKeepToVault(this.keep.id, vaultId)

          Pop.toast('The keep has was successsfully added!')
        } catch (error) {
          logger.error(error)
          Pop.toast(error, 'error')
        }
      },
      async removeKeep() {
        try {
          if (await Pop.confirm('Are you sure ?')) {
            await keepsService.removeKeep(this.keep.id)
          }
          Modal.getOrCreateInstance(document.getElementById('keep-modal')).hide()
          Pop.toast('Keep has been deleted!')
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
.modalcard {
  background-color: white;
  height: 90vh;
  border-radius: 60px;
}
</style>