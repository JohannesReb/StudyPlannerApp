<script setup lang="ts">
import { EStatus } from '@/domain/EStatus'
import type { ICurriculum } from '@/domain/ICurriculum'
import type { IModule } from '@/domain/IModule'
import type { ISubject } from '@/domain/ISubject'
import type { IUserCurriculum } from '@/domain/IUserCurriculum'
import type { IUserSubject } from '@/domain/IUserSubject'
import CurriculumService from '@/services/CurriculumService'
import ModuleService from '@/services/ModuleService'
import SubjectService from '@/services/SubjectService'
import UserCurriculumService from '@/services/UserCurriculumService'
import UserSubjectService from '@/services/UserSubjectService'
import { onMounted, ref, watch } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const props = defineProps({
  curriculumId: String,
  moduleId: String
})
let userInfo = localStorage.getItem('userInfo')
const curricula = ref<ICurriculum[]>()
const modules = ref<IModule[]>()
const userSubjects = ref<IUserSubject[]>()
const publicSubjects = ref<ISubject[]>()
const chosenSubjects = ref<ISubject[]>()
const curriculum = ref<ICurriculum>()
const userCurriculum = ref<IUserCurriculum>()
const module = ref<IModule>()
const loading = ref(true)
const completed = ref(0)
const missing = ref(0)
const declared = ref(0)
const notDeclared = ref(0)
const loadData = async () => {
  loading.value = true
  try {
    curricula.value = (await CurriculumService.getAll()).data
    if (props.curriculumId) {
      userCurriculum.value = (await UserCurriculumService.get(userInfo!)).data
      modules.value = (await ModuleService.getAll(props.curriculumId)).data
      userSubjects.value = (await UserSubjectService.getAll(userInfo!)).data
      userInfo = localStorage.getItem('userInfo')
      curriculum.value = curricula.value?.find((x) => x.id == props.curriculumId)
      publicSubjects.value = (
        await SubjectService.getAllPublicByCurriculum(props.curriculumId, userInfo!)
      ).data
      chosenSubjects.value = (
        await SubjectService.getAllChosenByCurriculum(props.curriculumId, userInfo!)
      ).data
    }
    if (props.moduleId) {
      publicSubjects.value = publicSubjects.value?.filter((x) => x.moduleId == props.moduleId)
      chosenSubjects.value = chosenSubjects.value?.filter((x) => x.moduleId == props.moduleId)
      module.value = modules.value?.find((x) => x.id == props.moduleId)
      userSubjects.value = userSubjects.value?.filter((x) => x.subject!.moduleId == props.moduleId)
    }
    calculate()
  } catch {
    console.log('Error')
  } finally {
    loading.value = false
  }
}

const calculate = () => {
  completed.value = 0
  missing.value = 0
  declared.value = 0
  notDeclared.value = 0
  userSubjects.value
    ?.filter((u) => chosenSubjects.value?.map((s) => s.id).includes(u.subjectId) && u.status == 3)
    .map((u) => u.subject!.eap)
    .forEach((x) => (completed.value += x ?? 0))
  missing.value -= completed.value

  props.moduleId
    ? (missing.value += module.value?.eap!)
    : modules.value?.map((m) => m.eap).forEach((x) => (missing.value += x))
  userSubjects.value
    ?.filter((u) => chosenSubjects.value?.map((s) => s.id).includes(u.subjectId) && u.status != 3)
    .map((u) => u.subject!.eap)
    .forEach((x) => (declared.value += x ?? 0))
  notDeclared.value = missing.value - declared.value
}
const Choose = async (curriculumId: string) => {
  loading.value = true
  await UserCurriculumService.post(userInfo!, EStatus.Claimed, curriculumId)
  await loadData()
}
const UnChoose = async (curriculumId: string) => {
  loading.value = true
  await UserCurriculumService.delete(curriculumId, userInfo!)
  await loadData()
}

const Add = async (subjectId: string) => {
  loading.value = true
  await UserSubjectService.post(userInfo!, null, EStatus.Claimed, 0, subjectId)
  await loadData()
}
const Remove = async (id: string) => {
  loading.value = true
  await UserSubjectService.delete(id, userInfo!)
  await loadData()
}
const Start = async (userSubject: IUserSubject) => {
  loading.value = true
  await UserSubjectService.update(
    userInfo!,
    userSubject.id,
    userSubject.grade,
    EStatus.Pending,
    userSubject.semester,
    userSubject.subjectId
  )
  await loadData()
}
const Finish = async (userSubject: IUserSubject) => {
  loading.value = true
  await UserSubjectService.update(
    userInfo!,
    userSubject.id,
    userSubject.grade,
    EStatus.Completed,
    userSubject.semester,
    userSubject.subjectId
  )
  await loadData()
}

watch(
  () => [props.curriculumId, props.moduleId],
  async ([newCurriculumId, newModuleId]) => {
    await loadData()
  }
)

onMounted(loadData)
</script>

<template>
  <div v-if="loading"><h1>Loading...</h1></div>
  <div v-else>
    <h3>
      <p>
        <RouterLink
          v-for="curriculum1 in curricula"
          :key="curriculum1.id"
          class="link"
          :to="{ name: 'Curricula', params: { curriculumId: curriculum1.id } }"
          >{{ curriculum1.label }} |
        </RouterLink>
        <RouterLink class="link" :to="{ name: 'CurriculumCreate' }"
          >Create New Curriculum</RouterLink
        >
      </p>
    </h3>
    <!--  -->
    <div v-if="!props.curriculumId">
      <h1>Curricula</h1>
    </div>
    <div v-else>
      <h4>
        <p>
          <RouterLink
            class="link"
            :to="{ name: 'Curricula', params: { curriculumId: props.curriculumId } }"
            >All</RouterLink
          >
          <RouterLink
            v-for="module1 in modules"
            :key="module1.id"
            class="link"
            :to="{
              name: 'Curricula',
              params: { curriculumId: props.curriculumId, moduleId: module1.id }
            }"
          >
            | {{ module1.label }}</RouterLink
          >
        </p>
      </h4>
      <h1>{{ curriculum?.label }} ({{ curriculum?.code }})</h1>
      <div>
        <button v-if="!userCurriculum" @click="Choose(curriculum!.id)" class="link">Choose</button>
        <button v-else @click="UnChoose(curriculum!.id)" class="link">UnChoose</button>
        |
        <RouterLink
          class="link"
          :to="{ name: 'CurriculumDetails', params: { id: props.curriculumId } }"
          >Details</RouterLink
        >
        |
        <RouterLink
          class="link"
          :to="{ name: 'CurriculumEdit', params: { id: props.curriculumId } }"
          >Edit</RouterLink
        >
        |
        <RouterLink
          class="link"
          :to="{ name: 'CurriculumDelete', params: { id: props.curriculumId } }"
          >Delete</RouterLink
        >
      </div>
      <p>
        <RouterLink class="link" :to="{ name: 'ModuleCreate', params: { id: props.curriculumId } }"
          >Create New Module</RouterLink
        >
      </p>
    </div>
    <!--  -->
    <div v-if="!props.moduleId && props.curriculumId">
      <h2>All Modules</h2>
    </div>
    <!--  -->
    <div v-else-if="props.moduleId">
      <h2>{{ module?.label }} ({{ module?.eap }} EAP)</h2>
      <div>
        <RouterLink
          class="link"
          :to="{
            name: 'ModuleEdit',
            params: { id: props.moduleId, curriculumId: props.curriculumId }
          }"
          >Edit</RouterLink
        >
        |
        <RouterLink
          class="link"
          :to="{
            name: 'ModuleDelete',
            params: { id: props.moduleId, curriculumId: props.curriculumId }
          }"
          >Delete</RouterLink
        >
      </div>
    </div>

    <div v-if="props.curriculumId != null">
      <table class="table">
        <tr>
          <td>Completed EAP: {{ completed }}</td>
          <td>Missing EAP: {{ missing }}</td>
          <td>Declared EAP: {{ declared }}</td>
          <td>Not Declared EAP: {{ notDeclared }}</td>
        </tr>
      </table>
      <table class="table">
        <tr>
          <td>
            <h4>Public</h4>
            <table class="table">
              <thead>
                <tr>
                  <th>Label</th>
                  <th>EAP</th>
                  <th v-if="props.moduleId != null"></th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="subject1 in publicSubjects" :key="subject1.id">
                  <td>{{ subject1.label }}</td>
                  <td>{{ subject1.eap }}</td>
                  <td v-if="props.moduleId != null">
                    <button @click="Add(subject1.id)" class="link">Add</button>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td></td>
          <td>
            <h4>Selected</h4>
            <table class="table">
              <thead>
                <tr>
                  <th>Label</th>
                  <th>EAP</th>
                  <th v-if="props.moduleId != null"></th>
                </tr>
              </thead>
              <tbody v-if="props.moduleId != null">
                <tr v-for="userSubject in userSubjects" :key="userSubject.id">
                  <td>{{ userSubject.subject?.label }}</td>
                  <td>{{ userSubject.subject?.eap }}</td>
                  <td>
                    <button @click="Remove(userSubject.id)" class="link">Remove</button>
                    <button
                      v-if="userSubject.status == EStatus.Claimed"
                      @click="Start(userSubject)"
                      class="link"
                    >
                      Start
                    </button>
                    <button
                      v-else-if="userSubject.status == EStatus.Pending"
                      @click="Finish(userSubject)"
                      class="link"
                    >
                      Finish
                    </button>
                  </td>
                </tr>
              </tbody>
              <tbody v-else>
                <tr v-for="subject2 in chosenSubjects" :key="subject2.id">
                  <td>{{ subject2.label }}</td>
                  <td>{{ subject2.eap }}</td>
                </tr>
              </tbody>
            </table>
          </td>
        </tr>
      </table>
    </div>
  </div>
</template>

<style scoped></style>
