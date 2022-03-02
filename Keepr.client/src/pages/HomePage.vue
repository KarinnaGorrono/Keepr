<template>
  <div class="container-fluid">
    <div class="homePage masonry-with-columns">
      <div class="thing pt-1" v-for="k in keeps" :key="k.id">
        <Keep :keep="k" />
      </div>
    </div>
  </div>
  <div>
    <KeepModal />
  </div>
</template>

<script>
import { computed, onBeforeUnmount, onMounted } from '@vue/runtime-core'
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'
import Pop from '../utils/Pop'
export default {
  name: 'Home',
  setup() {
    onMounted(async () => {
      try {
        await keepsService.getAllKeeps()

      } catch (error) {
        logger.error(error)
        Pop.toast(error, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.keeps),
    }
  }
}
</script>

<style scoped lang="scss">
.homePage {
  margin-top: 5vh;
}
.masonry-with-columns {
  column-count: 4;
  column-gap: 2em;
}

.thing {
  display: inline-block;
  margin: 0 0 1em;
  width: 100%;
}
@media only screen and (max-width: 500px) {
  .masonry {
    column-count: 2;
    column-gap: 1em;
  }
  .thing {
    display: inline-block;
    margin: 0 0 0.5em;
    width: 100%;
  }
}
</style>
