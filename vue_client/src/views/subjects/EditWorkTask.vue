<script setup lang="ts">
import { EAccessType } from '@/domain/EAccessType'
import type { IRole } from '@/domain/IRole'
import type { IWorkTask } from '@/domain/IWorkTask'
import type { IWorkTaskRole } from '@/domain/IWorkTaskRole'
import type { IUserWorkTask } from '@/domain/IUserWorkTask'
import RoleService from '@/services/RoleService'
import WorkTaskRoleService from '@/services/WorkTaskRoleService'
import WorkTaskService from '@/services/WorkTaskService'
import UserWorkTaskService from '@/services/UserWorkTaskService'
import { ref, watch, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { ETaskType } from '@/domain/ETaskType'
import { EField } from '@/domain/EField'

const router = useRouter()

const props = defineProps({
  id: String,
  subjectId: String
})

const userInfo = localStorage.getItem('userInfo')
const validationErrors = ref('')
const userId = JSON.parse(userInfo!).userId
const label = ref('')
const deadline = ref('')
const timeExpectancy = ref('')
const maxResult = ref(0)
const taskType = ref<ETaskType>()
const roleSelectList = ref<IRole[]>()
const roles = ref<string[]>([])
const accessType = ref<EAccessType>(EAccessType.Read)
const accessTypes = Object.values(EAccessType)
const taskTypes = Object.values(ETaskType)
const field = ref(0)
const fields = Object.values(EField)
const workTask = ref<IWorkTask>()
const userWorkTask = ref<IUserWorkTask>()
const workTaskRoles = ref<IWorkTaskRole[]>()
const loading = ref(false)
const loadData = async () => {
  loading.value = false
  roleSelectList.value = (await RoleService.getAll(userInfo!)).data
  workTask.value = (await WorkTaskService.get(props.id!, userInfo!)).data
  userWorkTask.value = (await UserWorkTaskService.get(props.id!, userInfo!)).data
  workTaskRoles.value = (await WorkTaskRoleService.getAllByWorkTask(props.id!, userInfo!)).data!

  label.value = workTask.value?.label!
  deadline.value = new Date(workTask.value?.deadline!).toISOString().substring(0, 16)
  timeExpectancy.value = workTask.value?.timeExpectancy!
  maxResult.value = workTask.value?.maxResult!
  taskType.value = workTask.value?.taskType!
  if (workTaskRoles.value.length > 0) {
    accessType.value = workTaskRoles.value![0].accessType!
    roles.value = workTaskRoles.value!.map((x) => x.roleId)
  }
  loading.value = false
}

const validateAndSave = async () => {
  let regExp = new RegExp('^([0-9]+\\.)?((2[0-3])|([0-1][0-9])):[0-5][0-9]:[0-5][0-9]$')
  if (label.value == '' || !regExp.test(timeExpectancy.value)) {
    validationErrors.value = "'label' field is required"
    return
  }
  console.log(roles.value)

  const res = await WorkTaskService.update(
    userInfo!,
    props.id!,
    label.value,
    deadline.value,
    timeExpectancy.value,
    maxResult.value,
    taskType.value!,
    field.value,
    props.subjectId!,
    roles.value[0] == '' ? [] : roles.value,
    accessType.value,
    workTask.value!.createdBy
  )
  if (!res.errors) {
    router.push({ name: 'Subjects', params: { subjectId: props.subjectId } })
  }
}

loadData()
watch([roleSelectList], async () => {})

onMounted(loadData)
</script>

<template>
  <h1>Edit</h1>

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
      <div
        v-if="
          workTask?.createdBy == userId ||
          workTaskRoles!
            .map((x) => x.accessType)
            ?.filter((x) => x == EAccessType.Admin)
            .pop()
        "
        class="form-group"
      >
        <label class="control-label" for="Ewent_SubjectId">Share</label>
        <select v-model="roles" class="form-control" multiple>
          <option></option>
          <option v-for="role in roleSelectList" :key="role.id" :value="role.id">
            {{ role.name }}
          </option>
        </select>
      </div>
      <div
        v-if="
          workTask?.createdBy == userId ||
          workTaskRoles!
            .map((x) => x.accessType)
            ?.filter((x) => x == EAccessType.Admin)
            .pop()
        "
        class="form-group"
      >
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
        <button @click="validateAndSave()" type="submit" value="Edit" class="btn btn-primary">
          Save
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
