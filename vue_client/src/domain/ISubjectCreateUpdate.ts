import type { EAccessType } from './EAccessType'
import type { ISubject } from './ISubject'

export interface ISubjectCreateUpdate {
  subject: ISubject
  roles: Array<string>
  semester: number
  accessType: EAccessType
}
