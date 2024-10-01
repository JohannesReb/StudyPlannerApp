<script setup lang="ts">
import type { ICurriculum } from '@/domain/ICurriculum'
import CurriculumService from '@/services/CurriculumService'
import { ref, watch } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const props = defineProps({
  id: String
})

const userInfo = localStorage.getItem('userInfo')
const validationErrors = ref('')
const label = ref('')
const code = ref('')
const from = ref('')
const until = ref('')
const manager = ref('')
const semesters = ref(0)
const curriculum = ref<ICurriculum>()
const loading = ref(true)
const loadData = async () => {
  curriculum.value = (await CurriculumService.get(props.id!)).data
  label.value = curriculum.value?.label!
  code.value = curriculum.value?.code!
  from.value = new Date(curriculum.value?.from!).toISOString().substring(0, 16)
  until.value = new Date(curriculum.value?.until!).toISOString().substring(0, 16)
  manager.value = curriculum.value?.manager!
  semesters.value = curriculum.value?.semesters!
}

const validateAndSave = async () => {
  if (label.value == '' || code.value == '' || from.value == '' || manager.value == '') {
    validationErrors.value = "Fields 'label', 'code', 'from' and 'manager' are required"
    return
  }

  const res = await CurriculumService.update(
    userInfo!,
    props.id!,
    code.value,
    label.value,
    manager.value,
    from.value,
    until.value,
    semesters.value
  )
  if (!res.errors) {
    router.push({ name: 'Curricula', params: { curriculumId: props.id } })
  }
}

loadData()
watch(curriculum, async (newValue, oldValue) => {
  console.log(curriculum)
  loading.value = false
})
</script>

<template>
  <h1>Edit</h1>

  <h4>Ewent</h4>
  <hr />
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
        <button @click="validateAndSave()" type="submit" value="Edit" class="btn btn-primary">
          Save
        </button>
      </div>
    </div>
  </div>

  <div>
    <RouterLink class="link" :to="{ name: 'Curricula', params: { curriculumId: props.id } }"
      >Back to List</RouterLink
    >
  </div>
</template>
