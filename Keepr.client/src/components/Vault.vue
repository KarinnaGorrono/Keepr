<template>
  <div
    class="card bg-dark text-white rounded elevation-2 grad selectable"
    @click="setActiveVault"
  >
    <div class="text-center">
      <h5 class="card-title title-text">{{ vault.name }}</h5>
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
      setActiveVault() {
        router.push({
          name: 'Vault',
          params: { id: `${props.vault.id}` }
        })
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
</style>