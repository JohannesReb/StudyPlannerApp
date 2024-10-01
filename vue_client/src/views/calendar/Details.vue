<script setup lang="ts">
import type { IEvent } from '@/domain/IEvent'
import EventService from '@/services/EventService'
import { ref, watch } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const props = defineProps({
  id: String
})

const userInfo = localStorage.getItem('userInfo')
let event = ref<IEvent>()
const loading = ref(true)
const loadData = async () => {
  event.value = (await EventService.get(props.id!, userInfo!)).data
}

loadData()
watch(event, async (newValue, oldValue) => {
  console.log(event)
  loading.value = false
})
</script>

<template>
  <h1>Details</h1>

  <div>
    <h4>Event</h4>
    <hr />
    <dl class="row">
      <dt class="col-sm-2">Label</dt>
      <dd class="col-sm-10">{{ event?.label }}</dd>
      <dt class="col-sm-2">Description</dt>
      <dd class="col-sm-10">{{ event?.description }}</dd>
      <dt class="col-sm-2">From</dt>
      <dd class="col-sm-10">{{ event?.from }}</dd>
      <dt class="col-sm-2">Until</dt>
      <dd class="col-sm-10">{{ event?.until }}</dd>
      <dt class="col-sm-2">Subject</dt>
      <dd class="col-sm-10">{{ event?.subject.label }}</dd>
    </dl>
    <RouterLink class="link" :to="{ name: 'Calendar' }">Back to List</RouterLink> |
    <RouterLink class="link" :to="{ name: 'EventEdit', params: { id: props.id! } }"
      >Edit</RouterLink
    >
  </div>
</template>
