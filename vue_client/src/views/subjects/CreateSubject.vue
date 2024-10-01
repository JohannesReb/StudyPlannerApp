<script setup lang="ts">
import { EAccessType } from '@/domain/EAccessType'
import type { IModule } from '@/domain/IModule'
import type { IRole } from '@/domain/IRole'
import ModuleService from '@/services/ModuleService'
import RoleService from '@/services/RoleService'
import SubjectService from '@/services/SubjectService'
import UserCurriculumService from '@/services/UserCurriculumService'
import { ref, watch, onMounted } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const userInfo = localStorage.getItem('userInfo')
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
const loading = ref(false)
const curriculumId = ref<string | undefined>('')
const accessTypes = Object.values(EAccessType)
const loadData = async () => {
  loading.value = true
  roleSelectList.value = (await RoleService.getAll(userInfo!)).data
  curriculumId.value = (await UserCurriculumService.get(userInfo!)).data?.curriculumId
  if (curriculumId.value) {
    modules.value = (await ModuleService.getAll(curriculumId.value)).data
  }
  loading.value = false
}

const validateAndCreate = async () => {
  if (label.value == '') {
    validationErrors.value = "'label' field is required"
    return
  }

  const res = await SubjectService.post(
    userInfo!,
    label.value,
    description.value,
    eap.value,
    moduleId.value,
    roles.value,
    semester.value,
    accessType.value
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
  <div v-if="loading">Loading...</div>
  <div v-else>
    <h1>Create</h1>

    <h4>Subject</h4>
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
      <RouterLink class="link" :to="{ name: 'Subjects' }">Back to List</RouterLink>
    </div>
  </div>
</template>
