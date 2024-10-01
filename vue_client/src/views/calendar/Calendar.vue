<script setup lang="ts">
import type { IEvent } from '@/domain/IEvent'
import type { IWorkTaskTimeWindow } from '@/domain/IWorkTaskTimeWindow'
import EventService from '@/services/EventService'
import WorkTaskTimeWindowService from '@/services/WorkTaskTimeWindowService'
import { ref, watch } from 'vue'

const userInfo = localStorage.getItem('userInfo')
let events = ref<IEvent[]>()
let workTaskTimeWindows = ref<IWorkTaskTimeWindow[]>()
const loading = ref(true)
const loadData = async () => {
  events.value = (await EventService.getAll(userInfo!)).data
  workTaskTimeWindows.value = (await WorkTaskTimeWindowService.getAll(userInfo!)).data
}
loadData()
watch(events, async (newValue, oldValue) => {
  console.log(events)
  loading.value = false
})
</script>

<template>
  <h1>Calendar</h1>
  <p>
    <RouterLink class="link" :to="{ name: 'EventCreate' }">Create New Event</RouterLink>
  </p>
  <table class="table">
    <tr>
      <td>
        <h4>Events</h4>
        <table class="table">
          <thead>
            <tr>
              <th>Label</th>
              <th>From</th>
              <th>Until</th>
              <th>Subject</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="event in events" :key="event.id">
              <td>{{ event.label }}</td>
              <td>{{ event.from }}</td>
              <td>{{ event.until }}</td>
              <td>{{ event.subject.label }}</td>
              <td>
                <RouterLink class="link" :to="{ name: 'EventDetails', params: { id: event.id } }"
                  >Details</RouterLink
                >
                |
                <RouterLink class="link" :to="{ name: 'EventEdit', params: { id: event.id } }"
                  >Edit</RouterLink
                >
                |
                <RouterLink class="link" :to="{ name: 'EventDelete', params: { id: event.id } }"
                  >Delete</RouterLink
                >
              </td>
            </tr>
          </tbody>
        </table>
      </td>
      <td></td>
      <td>
        <h4>Tasks</h4>
        <table class="table">
          <thead>
            <tr>
              <th>Label</th>
              <th>Deadline</th>
              <th>Time Expectancy</th>
              <th>Task Type</th>
              <th>Field</th>
              <th>From</th>
              <th>Until</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="workTaskTimeWindow in workTaskTimeWindows" :key="workTaskTimeWindow.id">
              <td>{{ workTaskTimeWindow.workTask.label }}</td>
              <td>{{ workTaskTimeWindow.workTask.deadline }}</td>
              <td>{{ workTaskTimeWindow.workTask.timeExpectancy }}</td>
              <td>{{ workTaskTimeWindow.workTask.taskType }}</td>
              <td>{{ workTaskTimeWindow.workTask.field }}</td>
              <td>{{ workTaskTimeWindow.timeWindow.from }}</td>
              <td>{{ workTaskTimeWindow.timeWindow.until }}</td>
            </tr>
          </tbody>
        </table>
      </td>
    </tr>
  </table>
</template>

<style scoped></style>
