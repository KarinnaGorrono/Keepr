<template>
  <nav
    class="
      navbarColor
      navbar navbar-expand-lg
      px-3
      d-flex
      justify-content-between
      align-items-center
    "
  >
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center text-light">
        <h1>KEEPER</h1>
      </div>
    </router-link>

    <span class="navbar-text">
      <button
        class="btn selectable text-dark text-uppercase my-2"
        @click="login"
        v-if="!user.isAuthenticated"
      >
        Login
      </button>

      <div class="dropdown my-2" v-else>
        <div
          class="dropdown-toggle selectable rounded"
          data-bs-toggle="dropdown"
          aria-expanded="false"
          id="authDropdown"
        >
          <img
            :src="user.picture"
            alt="user photo"
            height="40"
            class="rounded"
          />
          <span class="mx-3 text-dark">{{ user.name }}</span>
        </div>
        <div
          class="dropdown-menu p-0 list-group w-100"
          aria-labelledby="authDropdown"
        >
          <router-link :to="{ name: 'Account' }">
            <div class="list-group-item list-group-item-action hoverable">
              Manage Account
            </div>
          </router-link>
          <div
            class="list-group-item list-group-item-action hoverable text-danger"
            @click="logout"
          >
            <i class="mdi mdi-logout"></i>
            logout
          </div>
        </div>
      </div>
    </span>
  </nav>
</template>

<script>
import { AuthService } from '../services/AuthService'
import { AppState } from '../AppState'
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import { Offcanvas } from 'bootstrap'
export default {
  setup() {
    const router = useRouter()
    return {
      router,
      user: computed(() => AppState.user),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      },
      routeTo(route) {
        router.push({
          name: route,
        })
        Offcanvas.getOrCreateInstance(document.getElementById("offcanvasNavbar")).hide()
      }
    }
  }
}
</script>

<style scoped>
.navbarColor {
  background-color: rgb(1, 233, 156);
}
.dropdown-menu {
  user-select: none;
  display: block;
  transform: scale(0);
  transition: all 0.15s linear;
}
.dropdown-menu.show {
  transform: scale(1);
}
.hoverable {
  cursor: pointer;
}
a:hover {
  text-decoration: none;
}
.nav-link {
  text-transform: uppercase;
}
.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
</style>
