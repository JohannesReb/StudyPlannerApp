import type { ITimeWindow } from './ITimeWindow'
import type { IWorkTask } from './IWorkTask'

export interface IWorkTaskTimeWindow {
  id: string
  workTaskId: string
  workTask: IWorkTask
  timeWindowId: string
  timeWindow: ITimeWindow
}
