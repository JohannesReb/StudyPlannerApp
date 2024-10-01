import { ref, computed } from 'vue'
import { defineStore } from 'pinia'

export const useAuthStore = defineStore('auth', () => {
  // ref - state variables
  const jwt = ref<string | null>(null)
  const refreshToken = ref<string | null>(null)
  const userName = ref<string | null>(null)

  // computed - getters
  const isAuthenticated = computed<boolean>(() => !!jwt.value)

  // functions - actions

  // return your refs, computeds and functions
  return { jwt, refreshToken, userName, isAuthenticated }
})
