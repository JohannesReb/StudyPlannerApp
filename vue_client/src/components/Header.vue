<script setup lang="ts">
import router from '@/router'
import AccountService from '@/services/AccountService'
import Identity from '@/components/Identity.vue'
import { useAuthStore } from '@/stores/auth'

const authStore = useAuthStore()
const userInfo = JSON.parse(localStorage.getItem('userInfo')!)

const doLogout = async () => {
  localStorage.clear()
  const response = await AccountService.logout(userInfo)
  if (response.data) {
    localStorage.setItem('logoutInfo', JSON.stringify(response.data, null, 4))
  }
  console.log(localStorage.getItem('userInfo'))
  router.push('/')
}
</script>

<template>
  <header>
    <nav
      class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3"
    >
      <div class="container">
        <a class="navbar-brand" href="/">WebApp</a>
        <button
          class="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target=".navbar-collapse"
          aria-controls="navbarSupportedContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
          <ul class="navbar-nav flex-grow-1">
            <li class="nav-item">
              <RouterLink class="nav-link text-dark" to="/">Home</RouterLink>
            </li>
            <li class="nav-item">
              <RouterLink class="nav-link text-dark" :to="{ name: 'Calendar' }"
                >Calendar</RouterLink
              >
            </li>
            <li class="nav-item">
              <RouterLink class="nav-link text-dark" :to="{ name: 'Timetable' }"
                >Timetable</RouterLink
              >
            </li>
            <li class="nav-item">
              <RouterLink class="nav-link text-dark" :to="{ name: 'Curricula' }"
                >Curricula</RouterLink
              >
            </li>
            <li class="nav-item">
              <RouterLink class="nav-link text-dark" :to="{ name: 'Subjects' }"
                >Subjects</RouterLink
              >
            </li>
            <li class="nav-item">
              <RouterLink class="nav-link text-dark" :to="{ name: 'Statistics' }"
                >Statistics</RouterLink
              >
            </li>
          </ul>
          <Identity />
        </div>
      </div>
    </nav>
  </header>
</template>

<style scoped></style>
