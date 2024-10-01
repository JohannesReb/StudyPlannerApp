<script setup lang="ts">
import router from '@/router'
import AccountService from '@/services/AccountService'
import { useAuthStore } from '@/stores/auth'

const authStore = useAuthStore()
const userInfo = JSON.parse(localStorage.getItem('userInfo')!)

const doLogout = async () => {
  localStorage.clear()
  const response = await AccountService.logout(userInfo)
  if (response.data) {
    localStorage.setItem('logoutInfo', JSON.stringify(response.data, null, 4))
  }
}
</script>
<template>
  <ul v-if="!userInfo?.jwt" class="navbar-nav">
    <li class="nav-item">
      <RouterLink class="nav-link text-dark" :to="{ name: 'Register' }">Register</RouterLink>
    </li>
    <li class="nav-item">
      <RouterLink class="nav-link text-dark" :to="{ name: 'Login' }">Login</RouterLink>
    </li>
  </ul>
  <ul v-else class="navbar-nav">
    <li class="nav-item">
      <RouterLink class="nav-link text-dark" :to="{ name: 'Profile' }"
        >Hello, {{ userInfo.firstname + ' ' + userInfo.lastname }}!</RouterLink
      >
    </li>
    <li class="nav-item">
      <a @click="doLogout()" class="nav-link text-dark" href="/">Logout</a>
    </li>
  </ul>
</template>
