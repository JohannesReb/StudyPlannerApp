<script setup lang="ts">
import type { ISubject } from '@/domain/ISubject'
import EventService from '@/services/EventService'
import SubjectService from '@/services/SubjectService'
import { ref, watch } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const userInfo = localStorage.getItem('userInfo')
const validationErrors = ref('')
const label = ref('')
const description = ref('')
const from = ref('')
const until = ref('')
const subjectId = ref('')
const subjects = ref<ISubject[]>()
const loading = ref(true)
const loadData = async () => {
  subjects.value = (await SubjectService.getAll(userInfo!)).data
}

const validateAndCreate = async () => {
  if (label.value == '' || from.value == '' || until.value == '' || subjectId.value == '') {
    validationErrors.value = "Only 'Description' field is not required"
    return
  }

  const res = await EventService.post(
    userInfo!,
    label.value,
    description.value,
    from.value,
    until.value,
    subjectId.value
  )
  if (res.data) {
    router.push({ name: 'Calendar' })
  }
}

loadData()
watch(subjects, async (newValue, oldValue) => {
  console.log(subjects)
  loading.value = false
})
</script>

<template>
  <h1>Create</h1>

  <h4>Event</h4>
  <hr />
  <div class="text-danger">
    {{ validationErrors }}
  </div>
  <div class="row">
    <div class="col-md-4">
      <div class="form-group">
        <label class="control-label" for="Ewent_Label">Label</label>
        <input v-model="label" class="form-control" type="text" />
      </div>
      <div class="form-group">
        <label class="control-label" for="Ewent_Description">Description</label>
        <input v-model="description" class="form-control" type="text" />
      </div>
      <div class="form-group">
        <label class="control-label" for="Ewent_From">From</label>
        <input v-model="from" class="form-control" type="datetime-local" />
      </div>
      <div class="form-group">
        <label class="control-label" for="Ewent_Until">Until</label>
        <input v-model="until" class="form-control" type="datetime-local" />
      </div>
      <div class="form-group">
        <label class="control-label" for="Ewent_SubjectId">Subject</label>
        <select v-model="subjectId" class="form-control">
          <option v-for="subject in subjects" :key="subject.id" :value="subject.id">
            {{ subject.label }}
          </option>
        </select>
      </div>
      <div class="form-group">
        <button @click="validateAndCreate()" type="submit" value="Create" class="btn btn-primary">
          Create
        </button>
      </div>
    </div>
  </div>

  <div>
    <RouterLink class="link" :to="{ name: 'Calendar' }">Back to List</RouterLink>
  </div>
</template>
