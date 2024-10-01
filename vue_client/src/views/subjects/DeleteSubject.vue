<script setup lang="ts">
import { EAccessType } from '@/domain/EAccessType'
import { EStatus } from '@/domain/EStatus'
import type { ISubject } from '@/domain/ISubject'
import type { ISubjectRole } from '@/domain/ISubjectRole'
import type { IUserSubject } from '@/domain/IUserSubject'
import SubjectRoleService from '@/services/SubjectRoleService'
import SubjectService from '@/services/SubjectService'
import UserSubjectService from '@/services/UserSubjectService'
import { ref, watch } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const props = defineProps({
  id: String
})

const userInfo = localStorage.getItem('userInfo')
const userSubject = ref<IUserSubject>()
const subject = ref<ISubject>()
const subjectRoles = ref<ISubjectRole[]>([])
const subRolesStr = ref('')
const loading = ref(true)
const loadData = async () => {
  subject.value = (await SubjectService.get(props.id!, userInfo!)).data
  userSubject.value = (await UserSubjectService.get(props.id!, userInfo!)).data
  subjectRoles.value = (await SubjectRoleService.getAll(props.id!, userInfo!)).data!
  if (subRolesStr.value.length >= 2) {
    subRolesStr.value = subRolesStr.value.substring(0, -2)
  }
  loading.value = false
}

const deleteAsync = async () => {
  const res = await SubjectService.delete(props.id!, userInfo!)
  if (!res.errors) {
    router.push({ name: 'Subjects' })
  }
}

loadData()
watch([subject, userSubject, subjectRoles], async (newValue, oldValue) => {
  console.log(userSubject)
})
</script>

<template>
  <div v-if="loading">
    <h1>Loading...</h1>
  </div>
  <div v-else>
    <h1>Delete</h1>

    <h3>Are you sure you want to delete this?</h3>
    <div>
      <h4>Subject</h4>
      <hr />
      <dl class="row">
        <dt class="col-sm-2">Label</dt>
        <dd class="col-sm-10">{{ subject?.label }}</dd>
        <dt class="col-sm-2">Description</dt>
        <dd class="col-sm-10">{{ subject?.description }}</dd>
        <dt class="col-sm-2">EAP</dt>
        <dd class="col-sm-10">{{ subject?.eap }}</dd>
        <dt class="col-sm-2">Module</dt>
        <dd class="col-sm-10">{{ subject?.module?.label }}</dd>
        <dt class="col-sm-2">Status</dt>
        <dd class="col-sm-10">{{ EStatus[userSubject!.status] }}</dd>
        <dt v-if="subjectRoles.length >= 1" class="col-sm-2">Shared</dt>
        <dd v-if="subjectRoles.length >= 1" class="col-sm-10">
          {{ subjectRoles.map((x) => x.role.name).join(' | ') }}
        </dd>
        <dt v-if="subjectRoles.length >= 1" class="col-sm-2">Rights</dt>
        <dd v-if="subjectRoles.length >= 1" class="col-sm-10">
          {{ EAccessType[subjectRoles[0].accessType] }}
        </dd>
      </dl>
      <button @click="deleteAsync()" type="submit" value="Delete" class="btn btn-danger">
        Delete
      </button>
      |
      <RouterLink class="link" :to="{ name: 'Subjects', params: { subjectId: props.id } }"
        >Back to List</RouterLink
      >
    </div>
  </div>
</template>
