<script setup lang="ts">
import type { ITimeWindow } from '@/domain/ITimeWindow'
import TimeWindowService from '@/services/TimeWindowService'
import { onMounted, ref, watch } from 'vue'

let userInfo = localStorage.getItem('userInfo')
let timeWindows = ref<ITimeWindow[]>()
const loading = ref(true)
const loadData = async () => {
  loading.value = true
  timeWindows.value = (await TimeWindowService.getAll(userInfo!)).data
  loading.value = false
}

loadData()
watch([timeWindows], async (newValue, oldValue) => {})
onMounted(loadData)
</script>

<template>
  <h3>
    <p>
      <RouterLink class="link" :to="{ name: 'Timetable' }">Study Plan</RouterLink>
    </p>
  </h3>
  <h1>Time Windows</h1>

  <p>
    <RouterLink class="link" :to="{ name: 'TimeWindowCreate' }">Create New Time Window</RouterLink>
  </p>
  <table class="table">
    <thead>
      <tr>
        <th>From</th>
        <th>Until</th>
        <th></th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="timeWindow in timeWindows" :key="timeWindow.id">
        <td>{{ timeWindow.from }}</td>
        <td>{{ timeWindow.until }}</td>
        <td>
          <RouterLink class="link" :to="{ name: 'TimeWindowEdit', params: { id: timeWindow.id } }"
            >Edit</RouterLink
          >
          |
          <RouterLink class="link" :to="{ name: 'TimeWindowDelete', params: { id: timeWindow.id } }"
            >Delete</RouterLink
          >
        </td>
      </tr>
    </tbody>
  </table>
</template>
