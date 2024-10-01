<script setup lang="ts">
import type { ICurriculum } from '@/domain/ICurriculum'
import CurriculumService from '@/services/CurriculumService'
import ModuleService from '@/services/ModuleService'
import { ref, watch } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const props = defineProps({
  curriculumId: String
})
const userInfo = localStorage.getItem('userInfo')
const validationErrors = ref('')
const label = ref('')
const eap = ref(0)
const loading = ref(true)
const curricula = ref<ICurriculum[]>()

const loadData = async () => {
  curricula.value = (await CurriculumService.getAll()).data
}

const validateAndCreate = async () => {
  if (label.value == '') {
    validationErrors.value = "Fields 'label' and 'curriculum' are required"
    return
  }

  const res = await ModuleService.post(userInfo!, label.value, eap.value, props.curriculumId!)
  if (res.data) {
    router.push({ name: 'Curricula', params: { curriculumId: props.curriculumId } })
  }
}
loadData()
watch(curricula, async (newValue, oldValue) => {
  console.log(curricula)
  loading.value = false
})
</script>

<template>
  <h1>Create</h1>

  <h4>Module</h4>
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
        <label class="control-label" for="Ewent_Description">EAP</label>
        <input v-model="eap" autocomplete="code" class="form-control" type="number" />
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
