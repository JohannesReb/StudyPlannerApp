import axios from 'axios'
import type { IResultObject } from '@/domain/IResultObject'
import AccountService from './AccountService'
import type { IWorkTaskTimeWindow } from '@/domain/IWorkTaskTimeWindow'

export default class WorkTaskTimeWindowService {
  private constructor() {}

  private static httpClient = axios.create({
    baseURL: 'http://localhost:5223/api/v1/ApiWorkTaskTimeWindows/'
  })

  private static async getAllQuery(
    userInfo: string
  ): Promise<IResultObject<IWorkTaskTimeWindow[]>> {
    try {
      const response = await WorkTaskTimeWindowService.httpClient.get<IWorkTaskTimeWindow[]>(
        'GetWorkTaskTimeWindows',
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

  static async getAll(userInfo: string): Promise<IResultObject<IWorkTaskTimeWindow[]>> {
    let res = await this.getAllQuery(userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        res = await this.getAllQuery(JSON.stringify(authResponse.data))
      }
    }
    return res
  }

  private static async deleteQuery(
    workTaskId: string,
    userInfo: string
  ): Promise<IResultObject<void>> {
    try {
      const response = await WorkTaskTimeWindowService.httpClient.delete(
        'DeleteWorkTaskTimeWindow/' + workTaskId,
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

  static async delete(workTaskId: string, userInfo: string): Promise<IResultObject<void>> {
    let res = await this.deleteQuery(workTaskId, userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        res = await this.deleteQuery(workTaskId, JSON.stringify(authResponse.data))
      }
    }

    return res
  }

  private static async postQuery(postData: object, userInfo: string): Promise<IResultObject<void>> {
    try {
      const response = await WorkTaskTimeWindowService.httpClient.post<void>(
        'PostWorkTaskTimeWindow',
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

  static async post(
    userInfo: string,
    workTaskId: string,
    timeWindowId: string
  ): Promise<IResultObject<void>> {
    const postData = {
      workTaskId: workTaskId,
      timeWindowId: timeWindowId
    }
    let res = await this.postQuery(postData, userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        res = await this.postQuery(postData, JSON.stringify(authResponse.data))
      }
    }

    return res
  }
}
