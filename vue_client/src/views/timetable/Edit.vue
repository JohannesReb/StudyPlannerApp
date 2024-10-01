<script setup lang="ts">
import type { ISubject } from '@/domain/ISubject'
import TimeWindowService from '@/services/TimeWindowService'
import SubjectService from '@/services/SubjectService'
import { ref, watch } from 'vue'
import { useRouter } from 'vue-router'
import type { ITimeWindow } from '@/domain/ITimeWindow'

const router = useRouter()

const props = defineProps({
  id: String
})

const userInfo = localStorage.getItem('userInfo')
const validationErrors = ref('')
const from = ref('')
const until = ref('')
const freeTime = ref('')
const timeWindow = ref<ITimeWindow>()
const loading = ref(true)
const loadData = async () => {
  timeWindow.value = (await TimeWindowService.get(props.id!, userInfo!)).data
  from.value = new Date(timeWindow.value?.from!).toISOString().substring(0, 16)
  until.value = new Date(timeWindow.value?.until!).toISOString().substring(0, 16)
  freeTime.value = timeWindow.value?.freeTime!
}

const validateAndSave = async () => {
  if (from.value == '' || until.value == '') {
    validationErrors.value = "'from' and 'until' fields are required"
    return
  }

  const res = await TimeWindowService.update(
    userInfo!,
    props.id!,
    from.value,
    until.value,
    freeTime.value
  )
  if (!res.errors) {
    router.push({ name: 'TimeWindows' })
  }
}

loadData()
watch(timeWindow, async (newValue, oldValue) => {
  loading.value = false
})
</script>

<template>
  <h1>Edit</h1>

  <h4>TimeWindow</h4>
  <hr />
  <div class="row">
    <div class="col-md-4">
      <div class="form-group">
        <label class="control-label" for="TimeWindow_From">From</label>
        <input v-model="from" class="form-control" type="datetime-local" />
      </div>
      <div class="form-group">
        <label class="control-label" for="TimeWindow_Until">Until</label>
        <input v-model="until" class="form-control" type="datetime-local" />
      </div>
      <div class="form-group">
        <button @click="validateAndSave()" type="submit" value="Edit" class="btn btn-primary">
          Save
        </button>
      </div>
    </div>
  </div>

  <div>
    <RouterLink class="link" :to="{ name: 'TimeWindows' }">Back to List</RouterLink>
  </div>
</template>
