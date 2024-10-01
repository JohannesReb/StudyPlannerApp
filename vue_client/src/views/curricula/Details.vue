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
let curriculum = ref<ICurriculum>()
const loading = ref(true)
const loadData = async () => {
  curriculum.value = (await CurriculumService.get(props.id!)).data
}

loadData()
watch(curriculum, async (newValue, oldValue) => {
  console.log(curriculum)
  loading.value = false
})
</script>

<template>
  <h1>Details</h1>

  <div>
    <h4>Curriculum</h4>
    <hr />
    <dl class="row">
      <dt class="col-sm-2">Label</dt>
      <dd class="col-sm-10">{{ curriculum?.label }}</dd>
      <dt class="col-sm-2">Code</dt>
      <dd class="col-sm-10">{{ curriculum?.code }}</dd>
      <dt class="col-sm-2">From</dt>
      <dd class="col-sm-10">{{ curriculum?.from }}</dd>
      <dt class="col-sm-2">Until</dt>
      <dd class="col-sm-10">{{ curriculum?.until }}</dd>
      <dt class="col-sm-2">Manager</dt>
      <dd class="col-sm-10">{{ curriculum?.manager }}</dd>
      <dt class="col-sm-2">Semesters</dt>
      <dd class="col-sm-10">{{ curriculum?.semesters }}</dd>
    </dl>
    <RouterLink class="link" :to="{ name: 'Curricula', params: { curriculumId: props.id } }"
      >Back to List</RouterLink
    >
    |
    <RouterLink class="link" :to="{ name: 'CurriculumEdit', params: { id: props.id! } }"
      >Edit</RouterLink
    >
  </div>
</template>
