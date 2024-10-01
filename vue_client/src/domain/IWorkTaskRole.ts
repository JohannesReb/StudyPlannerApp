import type { EAccessType } from './EAccessType'
import type { IRole } from './IRole'

export interface IWorkTaskRole {
  id: string
  roleId: string
  role: IRole
  workTaskId: string
  accessType: EAccessType
}
