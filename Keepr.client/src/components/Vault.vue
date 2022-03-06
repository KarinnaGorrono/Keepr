<template>
  <div
    class="card bg-dark text-white rounded elevation-2 selectable"
    @click="currentVault"
  >
    <div class="text-center">
      <h5 class="Text">{{ vault.name }}</h5>
    </div>
  </div>
</template>


<script>
import { useRouter } from 'vue-router'
export default {
  props: {
    vault: { type: Object, required: true },
  },
  setup(props) {
    const router = useRouter()
    return {
      router,
      currentVault() {
        router.push({
          name: 'Vault',
          params: { id: `${props.vault.id}` }
        })

      },

      test() {
        logger.log
      }

    }
  }
}
</script>


<style lang="scss" scoped>
.Text {
  font-weight: 800;
  color: white;
}
</style>