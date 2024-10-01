import axios from 'axios'
import type { IResultObject } from '@/domain/IResultObject'
import AccountService from './AccountService'
import type { IWorkTask } from '@/domain/IWorkTask'
import type { ETaskType } from '@/domain/ETaskType'
import type { EField } from '@/domain/EField'
import type { EAccessType } from '@/domain/EAccessType'
import type { TimeSpan } from '@/domain/TimeSpan'

export default class WorkTaskService {
  private constructor() {}

  private static httpClient = axios.create({
    baseURL: 'http://localhost:5223/api/v1/ApiWorkTasks/'
  })

  private static async getAllQuery(userInfo: string): Promise<IResultObject<IWorkTask[]>> {
    try {
      const response = await WorkTaskService.httpClient.get<IWorkTask[]>('GetWorkTasks', {
        headers: {
          Authorization: 'Bearer ' + JSON.parse(userInfo).jwt
        }
      })

      if (response.status < 300) {
        return {
          data: response.data
        }
      }
      return {
        errors: [response.status.toString() + ' ' + response.statusText]
      }
    } catch (error: any) {
      return {
        errors: [JSON.stringify(error)]
      }
    }
  }

  static async getAll(userInfo: string): Promise<IResultObject<IWorkTask[]>> {
    let res = await this.getAllQuery(userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        localStorage.setItem('userInfo', JSON.stringify(authResponse.data))
        res = await this.getAllQuery(JSON.stringify(authResponse.data))
      }
    }
    return res
  }

  private static async getAllUnPlannedQuery(userInfo: string): Promise<IResultObject<IWorkTask[]>> {
    try {
      const response = await WorkTaskService.httpClient.get<IWorkTask[]>(
        'GetAllUnPlannedWorkTasks',
        {
          headers: {
            Authorization: 'Bearer ' + JSON.parse(userInfo).jwt
          }
        }
      )

      if (response.status < 300) {
        return {
          data: response.data
        }
      }
      return {
        errors: [response.status.toString() + ' ' + response.statusText]
      }
    } catch (error: any) {
      return {
        errors: [JSON.stringify(error)]
      }
    }
  }

  static async getAllUnPlanned(userInfo: string): Promise<IResultObject<IWorkTask[]>> {
    let res = await this.getAllUnPlannedQuery(userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        localStorage.setItem('userInfo', JSON.stringify(authResponse.data))
        res = await this.getAllUnPlannedQuery(JSON.stringify(authResponse.data))
      }
    }
    return res
  }

  private static async getAllChosenQuery(userInfo: string): Promise<IResultObject<IWorkTask[]>> {
    try {
      const response = await WorkTaskService.httpClient.get<IWorkTask[]>(
        'GetAllChosenSortedWorkTasks',
        {
          headers: {
            Authorization: 'Bearer ' + JSON.parse(userInfo).jwt
          }
        }
      )

      if (response.status < 300) {
        return {
          data: response.data
        }
      }
      return {
        errors: [response.status.toString() + ' ' + response.statusText]
      }
    } catch (error: any) {
      return {
        errors: [JSON.stringify(error)]
      }
    }
  }

  static async getAllChosen(userInfo: string): Promise<IResultObject<IWorkTask[]>> {
    let res = await this.getAllChosenQuery(userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        localStorage.setItem('userInfo', JSON.stringify(authResponse.data))
        res = await this.getAllChosenQuery(JSON.stringify(authResponse.data))
      }
    }
    return res
  }

  private static async getAllPublicQuery(userInfo: string): Promise<IResultObject<IWorkTask[]>> {
    try {
      const response = await WorkTaskService.httpClient.get<IWorkTask[]>(
        'GetAllPublicSortedWorkTasks',
        {
          headers: {
            Authorization: 'Bearer ' + JSON.parse(userInfo).jwt
          }
        }
      )

      if (response.status < 300) {
        return {
          data: response.data
        }
      }
      return {
        errors: [response.status.toString() + ' ' + response.statusText]
      }
    } catch (error: any) {
      return {
        errors: [JSON.stringify(error)]
      }
    }
  }

  static async getAllPublic(userInfo: string): Promise<IResultObject<IWorkTask[]>> {
    let res = await this.getAllPublicQuery(userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        localStorage.setItem('userInfo', JSON.stringify(authResponse.data))
        res = await this.getAllPublicQuery(JSON.stringify(authResponse.data))
      }
    }
    return res
  }

  private static async getAllBySubjectQuery(
    subjectId: string,
    userInfo: string
  ): Promise<IResultObject<IWorkTask[]>> {
    try {
      const response = await WorkTaskService.httpClient.get<IWorkTask[]>(
        'GetAllSortedOfSubjectWorkTasks/' + subjectId,
        {
          headers: {
            Authorization: 'Bearer ' + JSON.parse(userInfo).jwt
          }
        }
      )

      if (response.status < 300) {
        return {
          data: response.data
        }
      }
      return {
        errors: [response.status.toString() + ' ' + response.statusText]
      }
    } catch (error: any) {
      return {
        errors: [JSON.stringify(error)]
      }
    }
  }

  static async getAllBySubject(
    subjectId: string,
    userInfo: string
  ): Promise<IResultObject<IWorkTask[]>> {
    let res = await this.getAllBySubjectQuery(subjectId, userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        localStorage.setItem('userInfo', JSON.stringify(authResponse.data))
        res = await this.getAllBySubjectQuery(subjectId, JSON.stringify(authResponse.data))
      }
    }
    return res
  }

  private static async getAllPublicBySubjectQuery(
    subjectId: string,
    userInfo: string
  ): Promise<IResultObject<IWorkTask[]>> {
    try {
      const response = await WorkTaskService.httpClient.get<IWorkTask[]>(
        'GetAllPublicSortedOfSubjectWorkTasks/' + subjectId,
        {
          headers: {
            Authorization: 'Bearer ' + JSON.parse(userInfo).jwt
          }
        }
      )

      if (response.status < 300) {
        return {
          data: response.data
        }
      }
      return {
        errors: [response.status.toString() + ' ' + response.statusText]
      }
    } catch (error: any) {
      return {
        errors: [JSON.stringify(error)]
      }
    }
  }

  static async getAllPublicBySubject(
    subjectId: string,
    userInfo: string
  ): Promise<IResultObject<IWorkTask[]>> {
    let res = await this.getAllPublicBySubjectQuery(subjectId, userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        localStorage.setItem('userInfo', JSON.stringify(authResponse.data))
        res = await this.getAllPublicBySubjectQuery(subjectId, JSON.stringify(authResponse.data))
      }
    }
    return res
  }

  private static async getAllChosenBySubjectQuery(
    subjectId: string,
    userInfo: string
  ): Promise<IResultObject<IWorkTask[]>> {
    try {
      const response = await WorkTaskService.httpClient.get<IWorkTask[]>(
        'GetAllChosenSortedOfSubjectWorkTasks/' + subjectId,
        {
          headers: {
            Authorization: 'Bearer ' + JSON.parse(userInfo).jwt
          }
        }
      )

      if (response.status < 300) {
        return {
          data: response.data
        }
      }
      return {
        errors: [response.status.toString() + ' ' + response.statusText]
      }
    } catch (error: any) {
      return {
        errors: [JSON.stringify(error)]
      }
    }
  }

  static async getAllChosenBySubject(
    subjectId: string,
    userInfo: string
  ): Promise<IResultObject<IWorkTask[]>> {
    let res = await this.getAllChosenBySubjectQuery(subjectId, userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        localStorage.setItem('userInfo', JSON.stringify(authResponse.data))
        res = await this.getAllChosenBySubjectQuery(subjectId, JSON.stringify(authResponse.data))
      }
    }
    return res
  }

  private static async getQuery(id: string, userInfo: string): Promise<IResultObject<IWorkTask>> {
    try {
      const response = await WorkTaskService.httpClient.get<IWorkTask>('GetWorkTask/' + id, {
        headers: {
          Authorization: 'Bearer ' + JSON.parse(userInfo).jwt
        }
      })

      if (response.status < 300) {
        return {
          data: response.data
        }
      }
      return {
        errors: [response.status.toString() + ' ' + response.statusText]
      }
    } catch (error: any) {
      return {
        errors: [JSON.stringify(error)]
      }
    }
  }

  static async get(id: string, userInfo: string): Promise<IResultObject<IWorkTask>> {
    let res = await this.getQuery(id, userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        localStorage.setItem('userInfo', JSON.stringify(authResponse.data))
        res = await this.getQuery(id, JSON.stringify(authResponse.data))
      }
    }
    return res
  }

  private static async deleteQuery(
    id: string,
    userInfo: string
  ): Promise<IResultObject<IWorkTask>> {
    try {
      const response = await WorkTaskService.httpClient.delete('DeleteWorkTask/' + id, {
        headers: {
          Authorization: 'Bearer ' + JSON.parse(userInfo).jwt
        }
      })
      if (response.status < 300) {
        return {
          data: response.data
        }
      }
      return {
        errors: [response.status.toString() + ' ' + response.statusText]
      }
    } catch (error: any) {
      return {
        errors: [JSON.stringify(error)]
      }
    }
  }

  static async delete(id: string, userInfo: string): Promise<IResultObject<IWorkTask>> {
    let res = await this.deleteQuery(id, userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        localStorage.setItem('userInfo', JSON.stringify(authResponse.data))
        res = await this.deleteQuery(id, JSON.stringify(authResponse.data))
      }
    }

    return res
  }

  private static async postQuery(
    postData: object,
    userInfo: string
  ): Promise<IResultObject<IWorkTask>> {
    try {
      const response = await WorkTaskService.httpClient.post<IWorkTask>('PostWorkTask', postData, {
        headers: {
          Authorization: 'Bearer ' + JSON.parse(userInfo).jwt
        }
      })
      if (response.status < 300) {
        return {
          data: response.data
        }
      }
      return {
        errors: [response.status.toString() + ' ' + response.statusText]
      }
    } catch (error: any) {
      return {
        errors: [JSON.stringify(error)]
      }
    }
  }

  static async post(
    userInfo: string,
    label: string,
    deadline: string,
    timeExpectancy: string,
    maxResult: number,
    taskType: ETaskType,
    field: EField,
    subjectId: string,
    roles: string[],
    accessType: EAccessType
  ): Promise<IResultObject<IWorkTask>> {
    const postData = {
      workTask: {
        label: label,
        deadline: deadline,
        timeExpectancy: timeExpectancy,
        maxResult: maxResult,
        taskType: taskType,
        field: field,
        subjectId: subjectId
      },
      roles: roles,
      accessType: accessType
    }
    let res = await this.postQuery(postData, userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        localStorage.setItem('userInfo', JSON.stringify(authResponse.data))
        res = await this.postQuery(postData, JSON.stringify(authResponse.data))
      }
    }

    return res
  }

  private static async updateQuery(
    postData: object,
    userInfo: string
  ): Promise<IResultObject<IWorkTask>> {
    try {
      const response = await WorkTaskService.httpClient.post<IWorkTask>(
        'UpdateWorkTask',
        postData,
        {
          headers: {
            Authorization: 'Bearer ' + JSON.parse(userInfo).jwt
          }
        }
      )
      if (response.status < 300) {
        return {
          data: response.data
        }
      }
      return {
        errors: [response.status.toString() + ' ' + response.statusText]
      }
    } catch (error: any) {
      return {
        errors: [JSON.stringify(error)]
      }
    }
  }

  static async update(
    userInfo: string,
    id: string,
    label: string,
    deadline: string,
    timeExpectancy: string,
    maxResult: number,
    taskType: ETaskType,
    field: EField,
    subjectId: string,
    roles: string[],
    accessType: EAccessType,
    createdBy: string
  ): Promise<IResultObject<IWorkTask>> {
    const postData = {
      workTask: {
        id: id,
        label: label,
        deadline: deadline,
        timeExpectancy: timeExpectancy,
        maxResult: maxResult,
        taskType: taskType,
        field: field,
        subjectId: subjectId,
        createdBy: createdBy
      },
      roles: roles,
      accessType: accessType
    }
    let res = await this.updateQuery(postData, userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        localStorage.setItem('userInfo', JSON.stringify(authResponse.data))
        res = await this.updateQuery(postData, JSON.stringify(authResponse.data))
      }
    }

    return res
  }
}
