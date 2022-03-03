<template>
  <Modal id="create-vault-modal">
    <template #modal-body>
      <form>
        <div class="mb-3">
          <label for="title" class="form-label">Title</label>
          <input
            type="text"
            v-model="vaultData.name"
            class="form-control"
            id="title"
            placeholder="Title.."
          />
        </div>
        <!-- Image not populating for vault yet -->
        <div class="mb-3">
          <label for="image" class="form-label">Image Url</label>
          <input
            type="text"
            v-model="vaultData.img"
            class="form-control"
            id="image"
            placeholder="URL..."
          />
        </div>
        <!-- <div class="mb-3">
          <label for="image" class="form-label">URL</label>
          <input
            type="text"
       
            id="image"
            placeholder="URL..."
          />
        </div> -->

        <div class="mb-3">
          <label for="description" class="form-label">Description</label>
          <textarea
            class="form-control"
            id="description"
            rows="3"
            v-model="vaultData.description"
          ></textarea>
        </div>
      </form>

      <div class="form-check form-switch">
        <input
          class="form-check-input"
          type="checkbox"
          role="switch"
          v-model="vaultData.isPrivate"
          id="vaultPrivate"
        />
        <label class="form-check-label" for="vaultPrivate">Private Vault</label>
      </div>
      <div class="text-end">
        <button
          title="Create Vault"
          class="btn btn-success"
          @click="createVault"
        >
          Create Vault
        </button>
      </div>
    </template>
  </Modal>
</template>


<script>
import { ref } from '@vue/reactivity'
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { Modal } from 'bootstrap'
export default {
  setup() {
    let vaultData = ref({})
    return {
      vaultData,
      async createVault() {
        try {
          await vaultsService.createVault(vaultData.value)
          Modal.getOrCreateInstance(document.getElementById("create-vault-modal")).hide()
          Pop.toast('Vault Created!', 'success')
        } catch (error) {
          logger.error(error)
          Pop.toast('Dont forget to input all info !', 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>