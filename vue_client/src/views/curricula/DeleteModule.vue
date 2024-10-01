<script setup lang="ts">
import type { ICurriculum } from '@/domain/ICurriculum'
import type { IModule } from '@/domain/IModule'
import CurriculumService from '@/services/CurriculumService'
import ModuleService from '@/services/ModuleService'
import { ref, watch } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const props = defineProps({
  id: String,
  curriculumId: String
})

const userInfo = localStorage.getItem('userInfo')
const module = ref<IModule>()
const curriculum = ref<ICurriculum>()
const loading = ref(true)
const loadData = async () => {
  module.value = (await ModuleService.get(props.id!)).data
  curriculum.value = (await CurriculumService.get(props.curriculumId!)).data
}

const deleteAsync = async () => {
  const res = await ModuleService.delete(props.id!, userInfo!)
  if (!res.errors) {
    router.push({ name: 'Curricula', params: { curriculumId: props.curriculumId } })
  }
}

loadData()
watch(module, async (newValue, oldValue) => {
  console.log(module)
  loading.value = false
})
</script>

<template>
  <h1>Delete</h1>

  <h3>Are you sure you want to delete this?</h3>
  <div>
    <h4>Module</h4>
    <hr />
    <dl class="row">
      <dt class="col-sm-2">Label</dt>
      <dd class="col-sm-10">{{ module?.label }}</dd>
      <dt class="col-sm-2">EAP</dt>
      <dd class="col-sm-10">{{ module?.eap }}</dd>
      <dt class="col-sm-2">Curriculum</dt>
      <dd class="col-sm-10">{{ curriculum?.label }}</dd>
    </dl>
    <button @click="deleteAsync()" type="submit" value="Delete" class="btn btn-primary">
      Delete
    </button>
    |
    <RouterLink
      class="link"
      :to="{ name: 'Curricula', params: { curriculumId: props.curriculumId, moduleId: props.id } }"
      >Back to List</RouterLink
    >
  </div>
</template>
