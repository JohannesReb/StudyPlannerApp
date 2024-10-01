import type { EField } from './EField'
import type { ETaskType } from './ETaskType'
import type { ISubject } from './ISubject'

export interface IWorkTask {
  id: string
  deadline: Date | null
  label: string
  timeExpectancy: string
  maxResult: number | null
  taskType: ETaskType
  subjectId: string
  subject: ISubject
  field: EField
  createdBy: string
}
