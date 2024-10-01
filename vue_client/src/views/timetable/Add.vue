<script setup lang="ts">
import type { ITimeWindow } from '@/domain/ITimeWindow'
import TimeWindowService from '@/services/TimeWindowService'
import WorkTaskTimeWindowService from '@/services/WorkTaskTimeWindowService'
import { ref, watch } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const props = defineProps({
  userWorkTaskId: String,
  workTaskId: String
})

const userInfo = localStorage.getItem('userInfo')
const validationErrors = ref('')
const timeWindowId = ref('')
const timeWindows = ref<ITimeWindow[]>()
const loading = ref(true)
const loadData = async () => {
  loading.value = true
  timeWindows.value = (
    await TimeWindowService.getAllAvailable(props.userWorkTaskId!, userInfo!)
  ).data
  loading.value = false
}

const validateAndCreate = async () => {
  if (timeWindowId.value == '') {
    validationErrors.value = 'Time Window field is required'
    return
  }
  const res = await WorkTaskTimeWindowService.post(userInfo!, props.workTaskId!, timeWindowId.value)
  if (!res.errors) {
    router.push({ name: 'Timetable' })
  }
}

loadData()
watch(timeWindows, async (newValue, oldValue) => {
  console.log(timeWindows)
})
</script>

<template>
  <div v-if="loading">Loading...</div>
  <div v-else>
    <h1>Add</h1>

    <h4>Task to TimeWindow</h4>
    <hr />
    <div class="text-danger">
      {{ validationErrors }}
    </div>
    <div class="row">
      <div class="col-md-4">
        <div class="form-group">
          <label class="control-label" for="Ewent_SubjectId">TimeWindow</label>
          <select v-model="timeWindowId" class="form-control">
            <option v-for="timeWindow in timeWindows" :key="timeWindow.id" :value="timeWindow.id">
              {{ 'From ' + timeWindow.from + ' Until ' + timeWindow.until }}
            </option>
          </select>
        </div>
        <div class="form-group">
          <button @click="validateAndCreate()" type="submit" value="Add" class="btn btn-primary">
            Add
          </button>
        </div>
      </div>
    </div>

    <div>
      <RouterLink class="link" :to="{ name: 'Timetable' }">Back to List</RouterLink>
    </div>
  </div>
</template>
