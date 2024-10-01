import Home from '@/views/Home.vue'
import Login from '@/views/identity/Login.vue'
import Register from '@/views/identity/Register.vue'
import Profile from '@/views/identity/Profile.vue'
import { createRouter, createWebHistory } from 'vue-router'
import Calendar from '@/views/calendar/Calendar.vue'
import EventCreate from '@/views/calendar/Create.vue'
import EventDelete from '@/views/calendar/Delete.vue'
import EventDetails from '@/views/calendar/Details.vue'
import EventEdit from '@/views/calendar/Edit.vue'
import Curricula from '@/views/curricula/Curricula.vue'
import CurriculumCreate from '@/views/curricula/CreateCurriculum.vue'
import CurriculumDelete from '@/views/curricula/DeleteCurriculum.vue'
import CurriculumDetails from '@/views/curricula/Details.vue'
import CurriculumEdit from '@/views/curricula/EditCurriculum.vue'
import ModuleCreate from '@/views/curricula/CreateModule.vue'
import ModuleDelete from '@/views/curricula/DeleteModule.vue'
import ModuleEdit from '@/views/curricula/EditModule.vue'
import Statistics from '@/views/statistics/Statistics.vue'
import Subjects from '@/views/subjects/Subjects.vue'
import SubjectCreate from '@/views/subjects/CreateSubject.vue'
import SubjectDelete from '@/views/subjects/DeleteSubject.vue'
import SubjectDetails from '@/views/subjects/SubjectDetails.vue'
import SubjectEdit from '@/views/subjects/EditSubject.vue'
import TaskCreate from '@/views/subjects/CreateWorkTask.vue'
import TaskDelete from '@/views/subjects/DeleteWorkTask.vue'
import TaskDetails from '@/views/subjects/WorkTaskDetails.vue'
import TaskEdit from '@/views/subjects/EditWorkTask.vue'
import Timetable from '@/views/timetable/Timetable.vue'
import TimeWindowCreate from '@/views/timetable/Create.vue'
import TimeWindowDelete from '@/views/timetable/Delete.vue'
import TimeWindowEdit from '@/views/timetable/Edit.vue'
import Add from '@/views/timetable/Add.vue'
import Finish from '@/views/timetable/Finish.vue'
import Pause from '@/views/timetable/Pause.vue'
import TimeWindows from '@/views/timetable/TimeWindows.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'Home',
      component: Home
    },
    {
      path: '/identity/login',
      name: 'Login',
      component: Login
    },
    {
      path: '/identity/register',
      name: 'Register',
      component: Register
    },
    {
      path: '/identity/profile',
      name: 'Profile',
      component: Profile
    },
    {
      path: '/calendar/calendar',
      name: 'Calendar',
      component: Calendar
    },
    {
      path: '/calendar/create',
      name: 'EventCreate',
      component: EventCreate
    },
    {
      path: '/calendar/delete:id',
      name: 'EventDelete',
      component: EventDelete,
      props: true
    },
    {
      path: '/calendar/details:id',
      name: 'EventDetails',
      component: EventDetails,
      props: true
    },
    {
      path: '/calendar/edit:id',
      name: 'EventEdit',
      component: EventEdit,
      props: true
    },
    {
      path: '/curricula/curricula/:curriculumId?/:moduleId?',
      name: 'Curricula',
      component: Curricula,
      props: true
    },
    {
      path: '/curricula/createCurriculum',
      name: 'CurriculumCreate',
      component: CurriculumCreate
    },
    {
      path: '/curricula/deleteCurriculum/:id',
      name: 'CurriculumDelete',
      component: CurriculumDelete,
      props: true
    },
    {
      path: '/curricula/details/:id',
      name: 'CurriculumDetails',
      component: CurriculumDetails,
      props: true
    },
    {
      path: '/curricula/editCurriculum/:id',
      name: 'CurriculumEdit',
      component: CurriculumEdit,
      props: true
    },
    {
      path: '/curricula/createModule/:curriculumId',
      name: 'ModuleCreate',
      component: ModuleCreate,
      props: true
    },
    {
      path: '/curricula/deleteModule/:id/:curriculumId',
      name: 'ModuleDelete',
      component: ModuleDelete,
      props: true
    },
    {
      path: '/curricula/editModule/:id/:curriculumId',
      name: 'ModuleEdit',
      component: ModuleEdit,
      props: true
    },
    {
      path: '/statistics/statistics/:subjectId?',
      name: 'Statistics',
      component: Statistics,
      props: true
    },
    {
      path: '/subjects/subjects/:subjectId?',
      name: 'Subjects',
      component: Subjects,
      props: true
    },
    {
      path: '/subjects/createSubject',
      name: 'SubjectCreate',
      component: SubjectCreate
    },
    {
      path: '/subjects/deleteSubject/:id',
      name: 'SubjectDelete',
      component: SubjectDelete,
      props: true
    },
    {
      path: '/subjects/SubjectDetails/:id',
      name: 'SubjectDetails',
      component: SubjectDetails,
      props: true
    },
    {
      path: '/subjects/editSubject/:id',
      name: 'SubjectEdit',
      component: SubjectEdit,
      props: true
    },
    {
      path: '/subjects/createWorkTask/:subjectId',
      name: 'TaskCreate',
      component: TaskCreate,
      props: true
    },
    {
      path: '/subjects/deleteWorkTask/:id/:subjectId?',
      name: 'TaskDelete',
      component: TaskDelete,
      props: true
    },
    {
      path: '/subjects/WorkTaskDetails/:id/:subjectId?',
      name: 'TaskDetails',
      component: TaskDetails,
      props: true
    },
    {
      path: '/subjects/editWorkTask/:id/:subjectId?',
      name: 'TaskEdit',
      component: TaskEdit,
      props: true
    },
    {
      path: '/timetable/timetable',
      name: 'Timetable',
      component: Timetable
    },
    {
      path: '/timetable/create',
      name: 'TimeWindowCreate',
      component: TimeWindowCreate
    },
    {
      path: '/timetable/delete/:id',
      name: 'TimeWindowDelete',
      component: TimeWindowDelete,
      props: true
    },
    {
      path: '/timetable/edit/:id',
      name: 'TimeWindowEdit',
      component: TimeWindowEdit,
      props: true
    },
    {
      path: '/timetable/add/:userWorkTaskId/:workTaskId',
      name: 'Add',
      component: Add,
      props: true
    },
    {
      path: '/timetable/finish/:id',
      name: 'Finish',
      component: Finish,
      props: true
    },
    {
      path: '/timetable/pause/:id',
      name: 'Pause',
      component: Pause,
      props: true
    },
    {
      path: '/timetable/timeWindows',
      name: 'TimeWindows',
      component: TimeWindows
    }
  ]
})

export default router
