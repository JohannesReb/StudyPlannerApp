import type { ISubject } from './ISubject'

export interface IEvent {
  id: string
  label: string
  description: string
  from: Date
  until: Date
  subjectId: string
  subject: ISubject
}
