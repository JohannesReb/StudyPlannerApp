import type { EStatus } from './EStatus'
import type { ISubject } from './ISubject'

export interface IUserSubject {
  id: string
  grade: number | null
  status: EStatus
  semester: number
  subjectId: string
  subject: ISubject
}
