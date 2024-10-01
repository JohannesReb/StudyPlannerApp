<script setup lang="ts">
import { EAccessType } from '@/domain/EAccessType'
import type { IModule } from '@/domain/IModule'
import type { IRole } from '@/domain/IRole'
import type { ISubject } from '@/domain/ISubject'
import type { ISubjectRole } from '@/domain/ISubjectRole'
import type { IUserSubject } from '@/domain/IUserSubject'
import ModuleService from '@/services/ModuleService'
import RoleService from '@/services/RoleService'
import SubjectRoleService from '@/services/SubjectRoleService'
import SubjectService from '@/services/SubjectService'
import UserCurriculumService from '@/services/UserCurriculumService'
import UserSubjectService from '@/services/UserSubjectService'
import { ref, watch, onMounted } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const props = defineProps({
  id: String
})

const userInfo = localStorage.getItem('userInfo')
const userId = JSON.parse(userInfo!).userId
const validationErrors = ref('')
const label = ref('')
const description = ref('')
const eap = ref(0)
const moduleId = ref('')
const roleSelectList = ref<IRole[]>()
const roles = ref<string[]>([])
const semester = ref(0)
const accessType = ref<EAccessType>(EAccessType.Read)
const modules = ref<IModule[]>()
const curriculumId = ref<string | undefined>('')
const accessTypes = Object.values(EAccessType)
const subject = ref<ISubject>()
const userSubject = ref<IUserSubject>()
const subjectRoles = ref<ISubjectRole[]>()
const subjectAccessTypes = ref<EAccessType[]>()
const loading = ref(false)
const loadData = async () => {
  loading.value = false
  roleSelectList.value = (await RoleService.getAll(userInfo!)).data
  curriculumId.value = (await UserCurriculumService.get(userInfo!)).data?.curriculumId
  if (curriculumId.value) {
    modules.value = (await ModuleService.getAll(curriculumId.value)).data
  }
  subject.value = (await SubjectService.get(props.id!, userInfo!)).data
  subjectAccessTypes.value = (await SubjectRoleService.getAll(props.id!, userInfo!)).data?.map(
    (x) => x.accessType
  )
  userSubject.value = (await UserSubjectService.get(props.id!, userInfo!)).data
  subjectRoles.value = (await SubjectRoleService.getAll(props.id!, userInfo!)).data!

  label.value = subject.value?.label!
  description.value = subject.value?.description!
  eap.value = subject.value?.eap!
  semester.value = userSubject.value?.semester!
  description.value = subject.value?.description!
  moduleId.value = subject.value?.moduleId!
  if (subjectRoles.value.length > 0) {
    accessType.value = subjectRoles.value![0].accessType!
    roles.value = subjectRoles.value!.map((x) => x.roleId)
  }
  loading.value = false
}

const validateAndSave = async () => {
  if (label.value == '') {
    validationErrors.value = "'label' field is required"
    return
  }

  const res = await SubjectService.update(
    userInfo!,
    props.id!,
    label.value,
    description.value,
    eap.value,
    moduleId.value,
    roles.value,
    semester.value,
    accessType.value,
    subject.value!.createdBy
  )
  if (res.data) {
    router.push({ name: 'Subjects' })
  }
}

loadData()
watch([modules, roleSelectList], async () => {})

onMounted(loadData)
</script>

<template>
  <h1>Edit</h1>

  <h4>Subject</h4>
  <hr />
  <div class="row">
    <div class="col-md-4">
      <div class="form-group">
        <label class="control-label" for="Ewent_Label">Label</label>
        <input v-model="label" class="form-control" type="text" />
      </div>
      <div class="form-group">
        <label class="control-label" for="Ewent_Description">Description</label>
        <input v-model="description" class="form-control" type="text" />
      </div>
      <div class="form-group">
        <label class="control-label" for="Ewent_Description">EAP</label>
        <input v-model="eap" class="form-control" type="number" />
      </div>
      <div class="form-group">
        <label class="control-label" for="Ewent_SubjectId">Module</label>
        <select v-model="moduleId" class="form-control">
          <option></option>
          <option v-for="module in modules" :key="module.id" :value="module.id">
            {{ module.label }}
          </option>
        </select>
      </div>
      <div class="form-group">
        <label class="control-label" for="Ewent_Description">Semester</label>
        <input v-model="semester" class="form-control" type="number" />
      </div>
      <div
        v-if="
          subject?.createdBy == userId ||
          subjectAccessTypes?.filter((x) => x == EAccessType.Admin).pop()
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
          subject?.createdBy == userId ||
          subjectAccessTypes?.filter((x) => x == EAccessType.Admin).pop()
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
    <RouterLink class="link" :to="{ name: 'Subjects' }">Back to List</RouterLink>
  </div>
</template>
