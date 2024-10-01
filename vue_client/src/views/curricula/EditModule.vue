<script setup lang="ts">
import type { IModule } from '@/domain/IModule'
import ModuleService from '@/services/ModuleService'
import { ref, watch } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const props = defineProps({
  id: String,
  curriculumId: String
})

const userInfo = localStorage.getItem('userInfo')
const validationErrors = ref('')
const label = ref('')
const eap = ref(0)
const module = ref<IModule>()
const loading = ref(true)
const loadData = async () => {
  module.value = (await ModuleService.get(props.id!)).data
  label.value = module.value?.label!
  eap.value = module.value?.eap!
}

const validateAndSave = async () => {
  if (label.value == '') {
    validationErrors.value = "Fields 'label' and 'curriculum' are required"
    return
  }

  const res = await ModuleService.update(
    userInfo!,
    props.id!,
    label.value,
    eap.value,
    props.curriculumId!
  )
  if (!res.errors) {
    router.push({
      name: 'Curricula',
      params: { moduleId: props.id, curriculumId: props.curriculumId }
    })
  }
}

loadData()
watch(module, async (newValue, oldValue) => {
  console.log(module)
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
        <label class="control-label" for="Ewent_Description">EAP</label>
        <input v-model="eap" autocomplete="code" class="form-control" type="number" />
      </div>
      <div class="form-group">
        <button @click="validateAndSave()" type="submit" value="Edit" class="btn btn-primary">
          Save
        </button>
      </div>
    </div>
  </div>

  <div>
    <RouterLink
      class="link"
      :to="{ name: 'Curricula', params: { moduleId: props.id, curriculumId: props.curriculumId } }"
      >Back to List</RouterLink
    >
  </div>
</template>
