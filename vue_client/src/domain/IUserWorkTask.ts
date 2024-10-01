import type { EStatus } from './EStatus'
import type { IWorkTask } from './IWorkTask'

export interface IUserWorkTask {
  id: string
  timeSpent: string
  completedAt: Date | null
  result: number | null
  status: EStatus
  workTaskId: string
  workTask: IWorkTask
}
