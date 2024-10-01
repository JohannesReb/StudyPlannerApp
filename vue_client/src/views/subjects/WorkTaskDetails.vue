<script setup lang="ts">
import { EAccessType } from '@/domain/EAccessType'
import { EStatus } from '@/domain/EStatus'
import type { IWorkTask } from '@/domain/IWorkTask'
import type { IWorkTaskRole } from '@/domain/IWorkTaskRole'
import type { IUserWorkTask } from '@/domain/IUserWorkTask'
import WorkTaskRoleService from '@/services/WorkTaskRoleService'
import WorkTaskService from '@/services/WorkTaskService'
import UserWorkTaskService from '@/services/UserWorkTaskService'
import { ref, watch } from 'vue'
import { useRouter } from 'vue-router'
import { EField } from '@/domain/EField'

const router = useRouter()

const props = defineProps({
  id: String,
  subjectId: String
})

const userInfo = localStorage.getItem('userInfo')
const userWorkTask = ref<IUserWorkTask>()
const workTask = ref<IWorkTask>()
const workTaskRoles = ref<IWorkTaskRole[]>([])
const subRolesStr = ref('')
const loading = ref(true)
const loadData = async () => {
  workTask.value = (await WorkTaskService.get(props.id!, userInfo!)).data
  userWorkTask.value = (await UserWorkTaskService.get(props.id!, userInfo!)).data
  workTaskRoles.value = (await WorkTaskRoleService.getAllByWorkTask(props.id!, userInfo!)).data!
  if (subRolesStr.value.length >= 2) {
    subRolesStr.value = subRolesStr.value.substring(0, -2)
  }
  loading.value = false
}

loadData()
watch([workTask, userWorkTask, workTaskRoles], async (newValue, oldValue) => {
  console.log(userWorkTask)
})
</script>

<template>
  <h1>Details</h1>

  <div>
    <h4>Event</h4>
    <hr />
    <dl class="row">
      <dt class="col-sm-2">Label</dt>
      <dd class="col-sm-10">{{ workTask?.label }}</dd>
      <dt class="col-sm-2">Deadline</dt>
      <dd class="col-sm-10">{{ workTask?.deadline }}</dd>
      <dt class="col-sm-2">Time Expectancy</dt>
      <dd class="col-sm-10">{{ workTask?.timeExpectancy }}</dd>
      <dt class="col-sm-2">Max Result</dt>
      <dd class="col-sm-10">{{ workTask?.maxResult }}</dd>
      <dt class="col-sm-2">Task Type</dt>
      <dd class="col-sm-10">{{ EStatus[workTask!.taskType] }}</dd>
      <dt class="col-sm-2">Field</dt>
      <dd class="col-sm-10">{{ EField[workTask!.field] }}</dd>
      <dt class="col-sm-2">Subject</dt>
      <dd class="col-sm-10">{{ workTask?.subject.label }}</dd>
      <dt class="col-sm-2">Status</dt>
      <dd class="col-sm-10">{{ EStatus[userWorkTask!.status] }}</dd>
      <dt v-if="workTaskRoles.length >= 1" class="col-sm-2">Shared</dt>
      <dd v-if="workTaskRoles.length >= 1" class="col-sm-10">
        {{ workTaskRoles.map((x) => x.role.name).join(' | ') }}
      </dd>
      <dt v-if="workTaskRoles.length >= 1" class="col-sm-2">Rights</dt>
      <dd v-if="workTaskRoles.length >= 1" class="col-sm-10">
        {{ EAccessType[workTaskRoles[0].accessType] }}
      </dd>
    </dl>
    <RouterLink class="link" :to="{ name: 'Subjects', params: { subjectId: props.subjectId } }"
      >Back to List</RouterLink
    >
  </div>
</template>
