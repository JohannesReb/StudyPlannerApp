<script setup lang="ts">
import TimeWindowService from '@/services/TimeWindowService'
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const userInfo = localStorage.getItem('userInfo')
const validationErrors = ref('')
const from = ref('')
const until = ref('')

const validateAndCreate = async () => {
  if (from.value == '' || until.value == '') {
    validationErrors.value = "'from' and 'until' fields are required"
    return
  }

  const res = await TimeWindowService.post(userInfo!, from.value, until.value)
  if (res.data) {
    router.push({ name: 'TimeWindows' })
  }
}
</script>

<template>
  <h1>Create</h1>

  <h4>TimeWindow</h4>
  <hr />
  <div class="text-danger">
    {{ validationErrors }}
  </div>
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
        <button @click="validateAndCreate()" type="submit" value="Create" class="btn btn-primary">
          Create
        </button>
      </div>
    </div>
  </div>

  <div>
    <RouterLink class="link" :to="{ name: 'TimeWindows' }">Back to List</RouterLink>
  </div>
</template>
