<template>
  <Modal id="create-keep-modal">
    <template #modal-body>
      <form>
        <div class="mb-3">
          <label for="title" class="form-label">Title</label>
          <input
            type="text"
            v-model="keepData.name"
            class="form-control"
            id="title"
            placeholder=""
          />
        </div>
        <div class="mb-3">
          <label for="description" class="form-label">Description</label>
          <textarea
            class="form-control"
            id="description"
            rows="3"
            v-model="keepData.description"
          ></textarea>
        </div>
        <div class="mb-3">
          <label for="image" class="form-label">Image Url</label>
          <input
            type="text"
            v-model="keepData.img"
            class="form-control"
            id="image"
            placeholder="URL..."
          />
        </div>
      </form>
      <div class="text-end">
        <button title="create Keep" class="btn btn-success" @click="createKeep">
          Create Keep
        </button>
      </div>
    </template>
  </Modal>
</template>


<script>
import { Modal } from 'bootstrap'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { ref } from '@vue/reactivity'
import { keepsService } from '../services/KeepsService'
export default {
  setup() {
    let keepData = ref({})
    return {
      keepData,
      async createKeep() {
        try {
          await keepsService.createKeep(keepData.value)
          Modal.getOrCreateInstance(document.getElementById("create-keep-modal")).hide()
          Pop.toast('Keep Created!', 'success')
        } catch (error) {
          logger.error(error)
          Modal.getOrCreateInstance(document.getElementById("create-keep-modal")).hide()
          Pop.toast('You forgot a description for your vault!', 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>