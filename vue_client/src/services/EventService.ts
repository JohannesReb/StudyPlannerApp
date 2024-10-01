import axios from 'axios'
import type { IResultObject } from '@/domain/IResultObject'
import AccountService from './AccountService'
import type { IEvent } from '@/domain/IEvent'

export default class EventService {
  private constructor() {}

  private static httpClient = axios.create({
    baseURL: 'http://localhost:5223/api/v1/ApiEwents/'
  })

  private static async getAllQuery(userInfo: string): Promise<IResultObject<IEvent[]>> {
    try {
      const response = await EventService.httpClient.get<IEvent[]>('GetEvents', {
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

  static async getAll(userInfo: string): Promise<IResultObject<IEvent[]>> {
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

  private static async getAllPublicQuery(userInfo: string): Promise<IResultObject<IEvent[]>> {
    try {
      const response = await EventService.httpClient.get<IEvent[]>('GetPublicEvents', {
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

  static async getAllPublic(userInfo: string): Promise<IResultObject<IEvent[]>> {
    let res = await this.getAllPublicQuery(userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        res = await this.getAllPublicQuery(JSON.stringify(authResponse.data))
      }
    }
    return res
  }

  private static async getAllChosenQuery(userInfo: string): Promise<IResultObject<IEvent[]>> {
    try {
      const response = await EventService.httpClient.get<IEvent[]>('GetChosenEvents', {
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

  static async getAllChosen(userInfo: string): Promise<IResultObject<IEvent[]>> {
    let res = await this.getAllChosenQuery(userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        res = await this.getAllChosenQuery(JSON.stringify(authResponse.data))
      }
    }
    return res
  }

  private static async getQuery(id: string, userInfo: string): Promise<IResultObject<IEvent>> {
    try {
      const response = await EventService.httpClient.get<IEvent>('GetEvent/' + id, {
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

  static async get(id: string, userInfo: string): Promise<IResultObject<IEvent>> {
    let res = await this.getQuery(id, userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        res = await this.getQuery(id, JSON.stringify(authResponse.data))
      }
    }
    return res
  }

  private static async deleteQuery(id: string, userInfo: string): Promise<IResultObject<IEvent>> {
    try {
      const response = await EventService.httpClient.delete('DeleteEvent/' + id, {
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

  static async delete(id: string, userInfo: string): Promise<IResultObject<IEvent>> {
    let res = await this.deleteQuery(id, userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        res = await this.deleteQuery(id, JSON.stringify(authResponse.data))
      }
    }

    return res
  }

  private static async postQuery(
    postData: object,
    userInfo: string
  ): Promise<IResultObject<IEvent>> {
    console.log(postData)
    try {
      const response = await EventService.httpClient.post<IEvent>('PostEvent', postData, {
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
    description: string,
    from: string,
    until: string,
    subjectId: string
  ): Promise<IResultObject<IEvent>> {
    const postData = {
      label: label,
      description: description,
      from: from,
      until: until,
      subjectId: subjectId
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

  private static async updateQuery(
    postData: object,
    userInfo: string
  ): Promise<IResultObject<IEvent>> {
    try {
      const response = await EventService.httpClient.post<IEvent>('UpdateEvent', postData, {
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

  static async update(
    userInfo: string,
    id: string,
    label: string,
    description: string,
    from: string,
    until: string,
    subjectId: string
  ): Promise<IResultObject<IEvent>> {
    const postData = {
      id: id,
      label: label,
      description: description,
      from: from,
      until: until,
      subjectId: subjectId
    }
    let res = await this.updateQuery(postData, userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        res = await this.updateQuery(postData, JSON.stringify(authResponse.data))
      }
    }

    return res
  }
}
