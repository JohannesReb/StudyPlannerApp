<script setup lang="ts">
import { EField } from '@/domain/EField'
import { EStatus } from '@/domain/EStatus'
import { ETaskType } from '@/domain/ETaskType'
import type { ITimeWindow } from '@/domain/ITimeWindow'
import type { IUserWorkTask } from '@/domain/IUserWorkTask'
import type { IWorkTaskTimeWindow } from '@/domain/IWorkTaskTimeWindow'
import TimeWindowService from '@/services/TimeWindowService'
import UserWorkTaskService from '@/services/UserWorkTaskService'
import WorkTaskTimeWindowService from '@/services/WorkTaskTimeWindowService'
import { onMounted, ref, watch } from 'vue'

let userInfo = localStorage.getItem('userInfo')
let timeWindows = ref<ITimeWindow[]>()
let workTaskTimeWindows = ref<IWorkTaskTimeWindow[]>()
let userWorkTasks = ref<IUserWorkTask[]>()
const loading = ref(true)
const loadData = async () => {
  loading.value = true
  timeWindows.value = (await TimeWindowService.getAllActive(userInfo!)).data
  userInfo = localStorage.getItem('userInfo')
  workTaskTimeWindows.value = (await WorkTaskTimeWindowService.getAll(userInfo!)).data
  userWorkTasks.value = (await UserWorkTaskService.getAll(userInfo!)).data
  loading.value = false
}

const Remove = async (id: string) => {
  loading.value = true
  await WorkTaskTimeWindowService.delete(id, userInfo!)
  await loadData()
}

const Start = async (userWorkTask: IUserWorkTask) => {
  loading.value = true
  await UserWorkTaskService.update(
    userInfo!,
    userWorkTask.id,
    userWorkTask.timeSpent,
    userWorkTask.completedAt,
    userWorkTask.result,
    EStatus.Pending,
    userWorkTask.workTaskId
  )
  await loadData()
}

loadData()
watch([timeWindows, workTaskTimeWindows, userWorkTasks], async (newValue, oldValue) => {})
onMounted(loadData)
</script>
<template>
  <div v-if="loading"><h1>Loading...</h1></div>
  <div v-else>
    <h3>
      <p>
        <RouterLink class="link" :to="{ name: 'TimeWindows' }">Time Windows</RouterLink>
      </p>
    </h3>
    <h1>Study Plan</h1>

    <table class="table">
      <tr>
        <td>
          <h4>Time Windows</h4>
          <table class="table">
            <thead>
              <tr>
                <th>From</th>
                <th>Until</th>
                <th>FreeTime</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="timeWindow in timeWindows" :key="timeWindow.id">
                <td>{{ timeWindow.from }}</td>
                <td>{{ timeWindow.until }}</td>
                <td>{{ timeWindow.freeTime }}</td>
              </tr>
            </tbody>
          </table>
        </td>
        <td>
          <h4>Tasks</h4>
          <table class="table">
            <thead>
              <tr>
                <th>Label</th>
                <th>Deadline</th>
                <th>TimeExpectancy</th>
                <th>TaskType</th>
                <th>Field</th>
                <th>Status</th>
                <th>TimeSpent</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="userWorkTask in userWorkTasks" :key="userWorkTask.id">
                <td>{{ userWorkTask.workTask.label }}</td>
                <td>{{ userWorkTask.workTask.deadline }}</td>
                <td>{{ userWorkTask.workTask.timeExpectancy }}</td>
                <td>{{ ETaskType[userWorkTask.workTask.taskType] }}</td>
                <td>{{ EField[userWorkTask.workTask.field] }}</td>
                <td>{{ EStatus[userWorkTask.status] }}</td>
                <td>{{ userWorkTask.timeSpent }}</td>
                <td>
                  <div
                    v-if="
                      userWorkTask.status == EStatus.Claimed ||
                      userWorkTask.status == EStatus.Paused
                    "
                  >
                    <a
                      v-if="
                        workTaskTimeWindows
                          ?.filter((x) => x.workTaskId == userWorkTask.workTaskId)
                          .pop()
                      "
                      class="link"
                      @click="Remove(userWorkTask.workTaskId)"
                      >Remove</a
                    >

                    <RouterLink
                      v-else
                      class="link"
                      :to="{
                        name: 'Add',
                        params: {
                          userWorkTaskId: userWorkTask.id,
                          workTaskId: userWorkTask.workTaskId
                        }
                      }"
                      >Add</RouterLink
                    >
                    |
                    <a @click="Start(userWorkTask)" class="link">Start</a>
                  </div>
                  <div v-if="userWorkTask.status == EStatus.Pending">
                    <RouterLink
                      class="link"
                      :to="{ name: 'Pause', params: { id: userWorkTask.id } }"
                      >Pause</RouterLink
                    >
                    |
                    <RouterLink
                      class="link"
                      :to="{ name: 'Finish', params: { id: userWorkTask.id } }"
                      >Finish</RouterLink
                    >
                  </div>
                  <div v-if="userWorkTask.status == EStatus.Completed">
                    <RouterLink
                      class="link"
                      :to="{ name: 'Finish', params: { id: userWorkTask.id } }"
                      >Edit</RouterLink
                    >
                  </div>
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
