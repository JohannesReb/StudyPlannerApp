import axios from 'axios'
import type { IResultObject } from '@/domain/IResultObject'
import AccountService from './AccountService'
import type { IWorkTaskRole } from '@/domain/IWorkTaskRole'

export default class WorkTaskRoleService {
  private constructor() {}

  private static httpClient = axios.create({
    baseURL: 'http://localhost:5223/api/v1/ApiWorkTaskRoles/'
  })

  private static async getAllQuery(userInfo: string): Promise<IResultObject<IWorkTaskRole[]>> {
    try {
      const response = await WorkTaskRoleService.httpClient.get<IWorkTaskRole[]>(
        'GetWorkTaskRoles',
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

  static async getAll(userInfo: string): Promise<IResultObject<IWorkTaskRole[]>> {
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

  private static async getAllByWorkTaskQuery(
    workTaskId: string,
    userInfo: string
  ): Promise<IResultObject<IWorkTaskRole[]>> {
    try {
      const response = await WorkTaskRoleService.httpClient.get<IWorkTaskRole[]>(
        'GetWorkTaskRolesByWorkTask/' + workTaskId,
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

  static async getAllByWorkTask(
    workTaskId: string,
    userInfo: string
  ): Promise<IResultObject<IWorkTaskRole[]>> {
    let res = await this.getAllByWorkTaskQuery(workTaskId, userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        localStorage.setItem('userInfo', JSON.stringify(authResponse.data))
        res = await this.getAllByWorkTaskQuery(workTaskId, JSON.stringify(authResponse.data))
      }
    }
    return res
  }
}
