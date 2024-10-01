<script setup lang="ts">
import type { ISubject } from '@/domain/ISubject'
import EventService from '@/services/EventService'
import SubjectService from '@/services/SubjectService'
import { ref, watch } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const props = defineProps({
  id: String
})

const userInfo = localStorage.getItem('userInfo')
let validationErrors = ref('')
let label = ref('')
let description = ref('')
let from = ref('')
let until = ref('')
let subjectId = ref('')
let subjects = ref<ISubject[]>()
const loading = ref(true)
const loadData = async () => {
  subjects.value = (await SubjectService.getAll(userInfo!)).data
  const event = (await EventService.get(props.id!, userInfo!)).data
  label.value = event?.label!
  description.value = event?.description!
  from.value = new Date(event?.from!).toISOString().substring(0, 16)
  until.value = new Date(event?.until!).toISOString().substring(0, 16)
  subjectId.value = event?.subjectId!
}

const validateAndSave = async () => {
  if (label.value == '' || from.value == '' || until.value == '' || subjectId.value == '') {
    validationErrors.value = "Only 'Description' field is not required"
    return
  }

  const res = await EventService.update(
    userInfo!,
    props.id!,
    label.value,
    description.value,
    from.value,
    until.value,
    subjectId.value
  )
  if (!res.errors) {
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
  <h1>Edit</h1>

  <h4>Event</h4>
  <hr />
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
        <label class="control-label" for="Ewent_SubjectId">SubjectId</label>
        <select v-model="subjectId" class="form-control">
          <option v-for="subject in subjects" :key="subject.id" :value="subject.id">
            {{ subject.label }}
          </option>
        </select>
      </div>
      <div class="form-group">
        <button @click="validateAndSave()" type="submit" value="Edit" class="btn btn-primary">
          Save
        </button>
      </div>
    </div>
  </div>

  <div>
    <RouterLink class="link" :to="{ name: 'Calendar' }">Back to List</RouterLink>
  </div>
</template>
