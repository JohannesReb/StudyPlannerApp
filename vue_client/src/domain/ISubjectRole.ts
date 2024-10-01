import type { EAccessType } from './EAccessType'
import type { IRole } from './IRole'

export interface ISubjectRole {
  id: string
  roleId: string
  role: IRole
  subjectId: string
  accessType: EAccessType
}
