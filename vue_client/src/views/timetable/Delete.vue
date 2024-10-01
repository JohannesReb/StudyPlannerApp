<script setup lang="ts">
import type { ITimeWindow } from '@/domain/ITimeWindow'
import TimeWindowService from '@/services/TimeWindowService'
import { ref, watch } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const props = defineProps({
  id: String
})

const userInfo = localStorage.getItem('userInfo')
let timeWindow = ref<ITimeWindow>()
const loading = ref(true)
const loadData = async () => {
  loading.value = true
  timeWindow.value = (await TimeWindowService.get(props.id!, userInfo!)).data
  loading.value = false
}

const deleteAsync = async () => {
  const res = await TimeWindowService.delete(props.id!, userInfo!)
  if (!res.errors) {
    router.push({ name: 'TimeWindows' })
  }
}

loadData()
watch(timeWindow, async (newValue, oldValue) => {})
</script>

<template>
  <h1>Delete</h1>

  <h3>Are you sure you want to delete this?</h3>
  <div>
    <h4>TimeWindow</h4>
    <hr />
    <dl class="row">
      <dt class="col-sm-2">From</dt>
      <dd class="col-sm-10">{{ timeWindow?.from }}</dd>
      <dt class="col-sm-2">Until</dt>
      <dd class="col-sm-10">{{ timeWindow?.until }}</dd>
    </dl>
    <button @click="deleteAsync()" type="submit" value="Delete" class="btn btn-danger">
      Delete
    </button>
    |
    <RouterLink class="link" :to="{ name: 'TimeWindows' }">Back to List</RouterLink>
  </div>
</template>
