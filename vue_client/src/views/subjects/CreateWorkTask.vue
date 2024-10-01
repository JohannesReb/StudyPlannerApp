<script setup lang="ts">
import WorkTaskService from '@/services/WorkTaskService'
import { ref, watch, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { EAccessType } from '@/domain/EAccessType'
import type { IRole } from '@/domain/IRole'
import RoleService from '@/services/RoleService'
import { ETaskType } from '@/domain/ETaskType'
import { EField } from '@/domain/EField'

const router = useRouter()
const props = defineProps({
  subjectId: String
})
const userInfo = localStorage.getItem('userInfo')
const validationErrors = ref('')
const label = ref('')
const deadline = ref('')
const timeExpectancy = ref('')
const maxResult = ref(0)
const taskType = ref(0)
const field = ref(0)
const roleSelectList = ref<IRole[]>()
const roles = ref<string[]>([])
const accessType = ref<EAccessType>(EAccessType.Read)
const accessTypes = Object.values(EAccessType)
const taskTypes = Object.values(ETaskType)
const fields = Object.values(EField)
const loading = ref(false)
const loadData = async () => {
  loading.value = true
  roleSelectList.value = (await RoleService.getAll(userInfo!)).data
  loading.value = false
}
const validateAndCreate = async () => {
  let regExp = new RegExp('^([0-9]+\\.)?((2[0-3])|([0-1][0-9])):[0-5][0-9]:[0-5][0-9]$')
  if (label.value == '' || !regExp.test(timeExpectancy.value)) {
    validationErrors.value = "'label' field is required"
    return
  }

  const res = await WorkTaskService.post(
    userInfo!,
    label.value,
    deadline.value,
    timeExpectancy.value,
    maxResult.value,
    taskType.value,
    field.value,
    props.subjectId!,
    roles.value,
    accessType.value
  )
  if (res.data) {
    router.push({ name: 'Subjects', params: { subjectId: props.subjectId } })
  }
}
loadData()
watch([roleSelectList], async () => {})

onMounted(loadData)
</script>

<template>
  <h1>Create</h1>

  <h4>WorkTask</h4>
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
        <label class="control-label" for="Ewent_Description">Deadline</label>
        <input v-model="deadline" class="form-control" type="datetime-local" />
      </div>
      <div class="form-group">
        <label class="control-label" for="Ewent_Description">Time Expectancy (d.)hh:mm:ss</label>
        <input v-model="timeExpectancy" class="form-control" type="text" />
      </div>
      <div class="form-group">
        <label class="control-label" for="Ewent_Description">Max Result</label>
        <input v-model="maxResult" class="form-control" type="number" />
      </div>
      <div class="form-group">
        <label asp-for="AccessType" class="control-label">Task Type</label>
        <select v-model="taskType" class="form-control">
          <option
            v-for="i in taskTypes.length / 2"
            :key="taskTypes[i + taskTypes.length / 2 - 1]"
            :value="taskTypes[i + taskTypes.length / 2 - 1]"
          >
            {{ taskTypes[i - 1] }}
          </option>
        </select>
      </div>
      <div class="form-group">
        <label asp-for="AccessType" class="control-label">Field</label>
        <select v-model="field" class="form-control">
          <option
            v-for="i in fields.length / 2"
            :key="fields[i + fields.length / 2 - 1]"
            :value="fields[i + fields.length / 2 - 1]"
          >
            {{ fields[i - 1] }}
          </option>
        </select>
      </div>
      <div class="form-group">
        <label class="control-label" for="Ewent_SubjectId">Share</label>
        <select v-model="roles" class="form-control" multiple>
          <option></option>
          <option v-for="role in roleSelectList" :key="role.id" :value="role.id">
            {{ role.name }}
          </option>
        </select>
      </div>
      <div class="form-group">
        <label asp-for="AccessType" class="control-label">Rights</label>
        <select v-model="accessType" class="form-control">
          <option
            v-for="i in accessTypes.length / 2"
            :key="accessTypes[i + accessTypes.length / 2 - 1]"
            :value="accessTypes[i + accessTypes.length / 2 - 1]"
          >
            {{ accessTypes[i - 1] }}
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
    <RouterLink class="link" :to="{ name: 'Subjects', params: { subjectId: props.subjectId } }"
      >Back to List</RouterLink
    >
  </div>
</template>
