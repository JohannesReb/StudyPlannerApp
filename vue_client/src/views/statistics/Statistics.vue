<script setup lang="ts">
import { EField } from '@/domain/EField'
import { EStatus } from '@/domain/EStatus'
import { ETaskType } from '@/domain/ETaskType'
import type { ISubject } from '@/domain/ISubject'
import type { IUserWorkTask } from '@/domain/IUserWorkTask'
import SubjectService from '@/services/SubjectService'
import UserWorkTaskService from '@/services/UserWorkTaskService'
import { onMounted, ref, watch } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const props = defineProps({
  subjectId: String
})
let userInfo = localStorage.getItem('userInfo')
const errors = ref<string[]>()
const userWorkTasks = ref<IUserWorkTask[]>()
const subjects = ref<ISubject[]>()
const subject = ref<ISubject>()
const loading = ref(false)
const TotalTimeSpent = ref(0)
const TasksCompleted = ref(0)
const TasksNotYetCompleted = ref(0)
const loadData = async () => {
  loading.value = true
  try {
    const uwtResponse = await UserWorkTaskService.getAll(userInfo!)
    if (uwtResponse.errors) {
      errors.value?.push(uwtResponse.errors[0])
    }
    userWorkTasks.value = uwtResponse.data
    userInfo = localStorage.getItem('userInfo')
    const ssResponse = await SubjectService.getAll(userInfo!)
    if (ssResponse.errors) {
      errors.value?.push(ssResponse.errors[0])
    }
    subjects.value = ssResponse.data
    if (props.subjectId) {
      const sResponse = await SubjectService.get(props.subjectId!, userInfo!)
      if (sResponse.errors) {
        errors.value?.push(sResponse.errors[0])
      }
      subject.value = sResponse.data
    }
    calculate()
  } catch (error) {
    console.log(error)
  } finally {
    loading.value = false
  }
}

const calculate = () => {
  TotalTimeSpent.value = 0
  TasksCompleted.value = 0
  TasksNotYetCompleted.value = 0
  userWorkTasks
    .value!.map((u) => u.timeSpent)
    .forEach(
      (u) =>
        (TotalTimeSpent.value +=
          parseInt(u.split(':')[0]) +
          parseInt(u.split(':')[1]) / 60 +
          parseInt(u.split(':')[2]) / 3600)
    )
  TotalTimeSpent.value = Math.round(TotalTimeSpent.value)
  TasksCompleted.value = userWorkTasks.value!.filter((w) => w.status == EStatus.Completed).length
  TasksNotYetCompleted.value = userWorkTasks.value!.filter(
    (w) => w.status != EStatus.Completed
  ).length
}

watch(
  () => [props.subjectId],
  async ([newsubjectId]) => {
    await loadData()
  }
)

onMounted(loadData)
</script>

<template>
  <div v-if="loading">
    <h1>Loading...</h1>
  </div>
  <div v-else>
    <h1>Statistics</h1>
    <!--
    <h2 v-if="props.subjectId">{{ subject?.label }}</h2>
    <h2 v-else>All</h2>

    <h3>
      <p>
        <RouterLink class="link" :to="{ name: 'Statistics' }">All</RouterLink>
        |
        <RouterLink
          v-for="subject1 in subjects"
          :key="subject1.id"
          class="link"
          :to="{ name: 'Statistics', params: { subjectId: subject1.id } }"
          >{{ subject1.label }} |
        </RouterLink>
      </p>
    </h3> -->

    <table class="table">
      <thead>
        <tr>
          <th>Total Time Spent</th>
          <th>Tasks Completed</th>
          <th>Tasks Not Yet Completed</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>{{ TotalTimeSpent }} h</td>
          <td>{{ TasksCompleted }}</td>
          <td>{{ TasksNotYetCompleted }}</td>
        </tr>
      </tbody>
      <tr>
        <td>
          <table class="table">
            <thead>
              <tr>
                <th>Label</th>
                <th>Deadline</th>
                <th>Time Expectancy</th>
                <th>Task Type</th>
                <th>Field</th>
                <th>Status</th>
                <th>Result</th>
                <th>TimeSpent</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="userWorkTask in userWorkTasks" :key="userWorkTask.id">
                <td>{{ userWorkTask.workTask?.label }}</td>
                <td>{{ userWorkTask.workTask?.deadline }}</td>
                <td>{{ userWorkTask.workTask?.timeExpectancy }}</td>
                <td>{{ ETaskType[userWorkTask.workTask?.taskType] }}</td>
                <td>{{ EField[userWorkTask.workTask?.field] }}</td>
                <td>{{ EStatus[userWorkTask.status] }}</td>
                <td>{{ userWorkTask.result }}</td>
                <td>{{ userWorkTask.timeSpent }}</td>
              </tr>
            </tbody>
          </table>
        </td>
      </tr>
    </table>
  </div>
</template>

<style scoped></style>
