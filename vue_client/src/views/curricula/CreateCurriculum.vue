<script setup lang="ts">
import CurriculumService from '@/services/CurriculumService'
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const userInfo = localStorage.getItem('userInfo')
let validationErrors = ref('')
let label = ref('')
let code = ref('')
let from = ref('')
let until = ref('')
let manager = ref('')
let semesters = ref(0)

const validateAndCreate = async () => {
  if (label.value == '' || from.value == '' || manager.value == '' || code.value == '') {
    validationErrors.value = "Fields 'label', 'code', 'from' and 'manager' are required"
    return
  }
  console.log(from.value)

  const res = await CurriculumService.post(
    userInfo!,
    label.value,
    code.value,
    manager.value,
    from.value,
    until.value,
    semesters.value
  )
  if (res.data) {
    router.push({ name: 'Curricula' })
  }
}
</script>

<template>
  <h1>Create</h1>

  <h4>Curriculum</h4>
  <hr />
  <div class="text-danger">
    {{ validationErrors }}
  </div>
  <div class="row">
    <div class="col-md-4">
      <div class="form-group">
        <label class="control-label" for="Ewent_Label">Label</label>
        <input v-model="label" autocomplete="label" class="form-control" type="text" />
      </div>
      <div class="form-group">
        <label class="control-label" for="Ewent_Description">Code</label>
        <input v-model="code" autocomplete="code" class="form-control" type="text" />
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
        <label class="control-label" for="Ewent_Until">Manager</label>
        <input v-model="manager" autocomplete="manager" class="form-control" type="text" />
      </div>
      <div class="form-group">
        <label class="control-label" for="Ewent_Until">Semesters</label>
        <input v-model="semesters" class="form-control" type="number" />
      </div>
      <div class="form-group">
        <button @click="validateAndCreate()" type="submit" value="Create" class="btn btn-primary">
          Create
        </button>
      </div>
    </div>
  </div>

  <div>
    <RouterLink class="link" :to="{ name: 'Curricula' }">Back to List</RouterLink>
  </div>
</template>
