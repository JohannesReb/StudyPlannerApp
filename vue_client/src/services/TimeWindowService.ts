import axios from 'axios'
import type { IResultObject } from '@/domain/IResultObject'
import AccountService from './AccountService'
import type { ITimeWindow } from '@/domain/ITimeWindow'

export default class TimeWindowService {
  private constructor() {}

  private static httpClient = axios.create({
    baseURL: 'http://localhost:5223/api/v1/ApiTimeWindows/'
  })

  private static async getAllQuery(userInfo: string): Promise<IResultObject<ITimeWindow[]>> {
    try {
      const response = await TimeWindowService.httpClient.get<ITimeWindow[]>('GetTimeWindows', {
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

  static async getAll(userInfo: string): Promise<IResultObject<ITimeWindow[]>> {
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

  private static async getAllAvailableQuery(
    userWorkTaskId: string,
    userInfo: string
  ): Promise<IResultObject<ITimeWindow[]>> {
    try {
      const response = await TimeWindowService.httpClient.get<ITimeWindow[]>(
        'GetAvailableTimeWindows/' + userWorkTaskId,
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

  static async getAllAvailable(
    userWorkTaskId: string,
    userInfo: string
  ): Promise<IResultObject<ITimeWindow[]>> {
    let res = await this.getAllAvailableQuery(userWorkTaskId, userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        localStorage.setItem('userInfo', JSON.stringify(authResponse.data))
        res = await this.getAllAvailableQuery(userWorkTaskId, JSON.stringify(authResponse.data))
      }
    }
    return res
  }

  private static async getAllActiveQuery(userInfo: string): Promise<IResultObject<ITimeWindow[]>> {
    try {
      const response = await TimeWindowService.httpClient.get<ITimeWindow[]>(
        'GetActiveTimeWindows',
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

  static async getAllActive(userInfo: string): Promise<IResultObject<ITimeWindow[]>> {
    let res = await this.getAllActiveQuery(userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        localStorage.setItem('userInfo', JSON.stringify(authResponse.data))
        res = await this.getAllActiveQuery(JSON.stringify(authResponse.data))
      }
    }
    return res
  }

  private static async getQuery(id: string, userInfo: string): Promise<IResultObject<ITimeWindow>> {
    try {
      const response = await TimeWindowService.httpClient.get<ITimeWindow>('GetTimeWindow/' + id, {
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

  static async get(id: string, userInfo: string): Promise<IResultObject<ITimeWindow>> {
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

  private static async getByWorkTaskIdQuery(
    workTaskId: string,
    userInfo: string
  ): Promise<IResultObject<ITimeWindow>> {
    try {
      const response = await TimeWindowService.httpClient.get<ITimeWindow>(
        'GetTimeWindowByWorkTaskId/' + workTaskId,
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

  static async getByWorkTaskId(
    workTaskId: string,
    userInfo: string
  ): Promise<IResultObject<ITimeWindow>> {
    let res = await this.getByWorkTaskIdQuery(workTaskId, userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        localStorage.setItem('userInfo', JSON.stringify(authResponse.data))
        res = await this.getByWorkTaskIdQuery(workTaskId, JSON.stringify(authResponse.data))
      }
    }
    return res
  }

  private static async deleteQuery(
    id: string,
    userInfo: string
  ): Promise<IResultObject<ITimeWindow>> {
    try {
      const response = await TimeWindowService.httpClient.delete('DeleteTimeWindow/' + id, {
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

  static async delete(id: string, userInfo: string): Promise<IResultObject<ITimeWindow>> {
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
  ): Promise<IResultObject<ITimeWindow>> {
    try {
      const response = await TimeWindowService.httpClient.post<ITimeWindow>(
        'PostTimeWindow',
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
    from: string,
    until: string
  ): Promise<IResultObject<ITimeWindow>> {
    const postData = {
      from: from,
      until: until
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
  ): Promise<IResultObject<ITimeWindow>> {
    try {
      const response = await TimeWindowService.httpClient.post<ITimeWindow>(
        'UpdateTimeWindow',
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
    from: string,
    until: string,
    freeTime: string
  ): Promise<IResultObject<ITimeWindow>> {
    const postData = {
      id: id,
      from: from,
      until: until,
      freeTime: freeTime
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
