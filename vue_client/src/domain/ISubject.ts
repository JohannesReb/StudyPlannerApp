import type { IModule } from './IModule'

export interface ISubject {
  id: string
  label: string
  description: string | null
  eap: number | null
  moduleId: string | null
  module: IModule
  createdBy: string
}
