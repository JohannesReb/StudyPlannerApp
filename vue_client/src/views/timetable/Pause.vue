<script setup lang="ts">
import { EStatus } from '@/domain/EStatus'
import type { IUserWorkTask } from '@/domain/IUserWorkTask'
import UserWorkTaskService from '@/services/UserWorkTaskService'
import { ref, watch } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const props = defineProps({
  id: String
})

const userInfo = localStorage.getItem('userInfo')
const validationErrors = ref('')
const timeSpent = ref('')
const userWorkTask = ref<IUserWorkTask>()
const loading = ref(true)
const loadData = async () => {
  loading.value = true
  userWorkTask.value = (await UserWorkTaskService.get(props.id!, userInfo!)).data
  timeSpent.value = userWorkTask.value?.timeSpent!
  loading.value = false
}

const validateAndCreate = async () => {
  if (timeSpent.value == '') {
    timeSpent.value = '00:00:00'
  }
  const res = await UserWorkTaskService.update(
    userInfo!,
    props.id!,
    timeSpent.value,
    userWorkTask.value?.completedAt!,
    userWorkTask.value?.result!,
    EStatus.Paused,
    userWorkTask.value?.workTaskId!
  )
  if (!res.errors) {
    router.push({ name: 'Timetable' })
  }
}

loadData()
watch(userWorkTask, async (newValue, oldValue) => {})
</script>

<template>
  <h1>Pause</h1>

  <h4>Task Completion</h4>
  <hr />
  <div class="text-danger">
    {{ validationErrors }}
  </div>
  <div class="row">
    <div class="col-md-4">
      <div class="form-group">
        <label class="control-label" for="Ewent_Description">Time Spent (d.)hh:mm:ss</label>
        <input v-model="timeSpent" class="form-control" type="text" />
      </div>
      <div class="form-group">
        <button @click="validateAndCreate()" type="submit" value="Add" class="btn btn-primary">
          Pause
        </button>
      </div>
    </div>
  </div>

  <div>
    <RouterLink class="link" :to="{ name: 'Timetable' }">Back to List</RouterLink>
  </div>
</template>
