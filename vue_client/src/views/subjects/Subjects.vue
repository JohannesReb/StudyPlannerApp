<script setup lang="ts">
import { EAccessType } from '@/domain/EAccessType'
import { EStatus } from '@/domain/EStatus'
import type { ISubject } from '@/domain/ISubject'
import type { IWorkTask } from '@/domain/IWorkTask'
import type { IWorkTaskRole } from '@/domain/IWorkTaskRole'
import SubjectRoleService from '@/services/SubjectRoleService'
import SubjectService from '@/services/SubjectService'
import UserWorkTaskService from '@/services/UserWorkTaskService'
import WorkTaskRoleService from '@/services/WorkTaskRoleService'
import WorkTaskService from '@/services/WorkTaskService'
import { onMounted, ref, watch } from 'vue'

const props = defineProps({
  subjectId: String
})
let userInfo = localStorage.getItem('userInfo')
const userId = JSON.parse(userInfo!).userId
const subjects = ref<ISubject[]>()
const subject = ref<ISubject>()
const publicWorkTasks = ref<IWorkTask[]>()
const chosenWorkTasks = ref<IWorkTask[]>()
const workTaskRoles = ref<IWorkTaskRole[]>()
const subjectAccessTypes = ref<EAccessType[]>()
const loading = ref(false)
const loadData = async () => {
  loading.value = true
  try {
    subjects.value = (await SubjectService.getAllChosen(userInfo!)).data
    userInfo = localStorage.getItem('userInfo')
    workTaskRoles.value = (await WorkTaskRoleService.getAll(userInfo!)).data
    if (props.subjectId) {
      subject.value = subjects.value?.find((x) => x.id == props.subjectId)
      subjectAccessTypes.value = (
        await SubjectRoleService.getAll(props.subjectId, userInfo!)
      ).data?.map((x) => x.accessType)
      publicWorkTasks.value = (
        await WorkTaskService.getAllPublicBySubject(props.subjectId, userInfo!)
      ).data
      chosenWorkTasks.value = (
        await WorkTaskService.getAllChosenBySubject(props.subjectId, userInfo!)
      ).data
    } else {
      publicWorkTasks.value = (await WorkTaskService.getAllPublic(userInfo!)).data
      chosenWorkTasks.value = (await WorkTaskService.getAllChosen(userInfo!)).data
    }
  } catch (error) {
    console.log(error)
  } finally {
    loading.value = false
  }
}

const Add = async (workTaskId: string) => {
  loading.value = true
  await UserWorkTaskService.post(userInfo!, '00:00:00', null, 0, EStatus.Claimed, workTaskId)
  await loadData()
}
const Remove = async (id: string) => {
  loading.value = true
  await UserWorkTaskService.delete(id, userInfo!)
  await loadData()
}

watch(
  () => [props.subjectId],
  async () => {
    await loadData()
    console.log(publicWorkTasks.value)
    console.log(chosenWorkTasks.value)
  }
)

onMounted(loadData)
</script>

<template>
  <div v-if="loading"><h1>Loading...</h1></div>
  <div v-else>
    <h1 v-if="!props.subjectId">Subjects</h1>
    <h1 v-else>{{ subject?.label }}</h1>
    <p v-if="!props.subjectId">
      <RouterLink class="link" :to="{ name: 'SubjectCreate' }">Create New Subject</RouterLink>
    </p>
    <p v-if="props.subjectId">
      <RouterLink class="link" :to="{ name: 'SubjectDetails', params: { id: props.subjectId } }"
        >Details</RouterLink
      >
      |
      <span
        v-if="
          subject?.createdBy == userId ||
          subjectAccessTypes?.filter((x) => x == EAccessType.Admin || x == EAccessType.Edit).pop()
        "
      >
        <RouterLink class="link" :to="{ name: 'SubjectEdit', params: { id: props.subjectId } }"
          >Edit</RouterLink
        >
        |
        <RouterLink class="link" :to="{ name: 'SubjectDelete', params: { id: props.subjectId } }"
          >Delete</RouterLink
        >
      </span>
    </p>
    <p v-if="props.subjectId">
      <RouterLink class="link" :to="{ name: 'TaskCreate', params: { id: props.subjectId } }"
        >Create New Task</RouterLink
      >
    </p>

    <h3>
      <p>
        <RouterLink class="link" :to="{ name: 'Subjects' }">All</RouterLink>
        <RouterLink
          v-for="subject in subjects"
          :key="subject.id"
          class="link"
          :to="{
            name: 'Subjects',
            params: { subjectId: subject.id }
          }"
        >
          | {{ subject.label }}</RouterLink
        >
      </p>
    </h3>

    <h3 v-if="props.subjectId">Description</h3>
    <div v-if="props.subjectId">{{ subject?.description }}</div>

    <table class="table">
      <tr>
        <td>
          <h4>Public Tasks</h4>
          <table class="table">
            <thead>
              <tr>
                <th>Label</th>
                <th>Deadline</th>
                <th>Time Expectancy</th>
                <th>Task Type</th>
                <th>Field</th>
                <th>Subject</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="workTask in publicWorkTasks" :key="workTask.id">
                <td>{{ workTask.label }}</td>
                <td>{{ workTask.deadline }}</td>
                <td>{{ workTask.timeExpectancy }}</td>
                <td>{{ workTask.taskType }}</td>
                <td>{{ workTask.field }}</td>
                <td>{{ workTask.subject.label }}</td>
                <td>
                  <button @click="Add(workTask.id)" class="link">Add</button>
                  <RouterLink
                    class="link"
                    :to="{
                      name: 'TaskDetails',
                      params: { id: workTask.id, subjectId: workTask.subjectId }
                    }"
                    >Details</RouterLink
                  >
                  <span
                    v-if="
                      workTask?.createdBy == userId ||
                      workTaskRoles
                        ?.filter((x) => x.workTaskId == workTask.id)
                        .map((x) => x.accessType)
                        ?.filter((x) => x == EAccessType.Admin || x == EAccessType.Edit)
                        .pop()
                    "
                  >
                    |
                    <RouterLink
                      class="link"
                      :to="{
                        name: 'TaskEdit',
                        params: { id: workTask.id, subjectId: workTask.subjectId }
                      }"
                      >Edit</RouterLink
                    >
                    |
                    <RouterLink
                      class="link"
                      :to="{
                        name: 'TaskDelete',
                        params: { id: workTask.id, subjectId: workTask.subjectId }
                      }"
                      >Delete</RouterLink
                    >
                  </span>
                </td>
              </tr>
            </tbody>
          </table>
        </td>
        <td></td>
        <td>
          <h4>Selected Tasks</h4>
          <table class="table">
            <thead>
              <tr>
                <th>Label</th>
                <th>Deadline</th>
                <th>Time Expectancy</th>
                <th>Task Type</th>
                <th>Field</th>
                <th>Subject</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="workTask in chosenWorkTasks" :key="workTask.id">
                <td>{{ workTask.label }}</td>
                <td>{{ workTask.deadline }}</td>
                <td>{{ workTask.timeExpectancy }}</td>
                <td>{{ workTask.taskType }}</td>
                <td>{{ workTask.field }}</td>
                <td>{{ workTask.subject.label }}</td>
                <td>
                  <button @click="Remove(workTask.id)" class="link">Remove</button>
                  <RouterLink
                    class="link"
                    :to="{
                      name: 'TaskDetails',
                      params: { id: workTask.id, subjectId: workTask.subjectId }
                    }"
                    >Details</RouterLink
                  >
                  |
                  <span
                    v-if="
                      workTask?.createdBy == userId ||
                      workTaskRoles
                        ?.filter((x) => x.workTaskId == workTask.id)
                        .map((x) => x.accessType)
                        ?.filter((x) => x == EAccessType.Admin || x == EAccessType.Edit)
                        .pop()
                    "
                  >
                    <RouterLink
                      class="link"
                      :to="{
                        name: 'TaskEdit',
                        params: { id: workTask.id, subjectId: workTask.subjectId }
                      }"
                      >Edit</RouterLink
                    >
                    |
                    <RouterLink
                      class="link"
                      :to="{
                        name: 'TaskDelete',
                        params: { id: workTask.id, subjectId: workTask.subjectId }
                      }"
                      >Delete</RouterLink
                    >
                  </span>
                </td>
              </tr>
            </tbody>
          </table>
        </td>
      </tr>
    </table>
  </div>
</template>

<style scoped></style>
