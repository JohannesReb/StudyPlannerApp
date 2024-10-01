import axios from 'axios'
import type { IResultObject } from '@/domain/IResultObject'
import AccountService from './AccountService'
import type { IModule } from '@/domain/IModule'

export default class ModuleService {
  private constructor() {}

  private static httpClient = axios.create({
    baseURL: 'http://localhost:5223/api/v1/ApiModules/'
  })

  static async getAll(curriculumId: string): Promise<IResultObject<IModule[]>> {
    try {
      const response = await ModuleService.httpClient.get<IModule[]>(
        'GetModulesByCurriculum/' + curriculumId
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

  static async get(id: string): Promise<IResultObject<IModule>> {
    try {
      const response = await ModuleService.httpClient.get<IModule>('GetModule/' + id)

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

  private static async deleteQuery(id: string, userInfo: string): Promise<IResultObject<IModule>> {
    try {
      const response = await ModuleService.httpClient.delete('DeleteModule/' + id, {
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

  static async delete(id: string, userInfo: string): Promise<IResultObject<IModule>> {
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
  ): Promise<IResultObject<IModule>> {
    try {
      const response = await ModuleService.httpClient.post<IModule>('PostModule', postData, {
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
    eap: number,
    curriculumId: string
  ): Promise<IResultObject<IModule>> {
    const postData = {
      label: label,
      eap: eap,
      curriculumId: curriculumId
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
  ): Promise<IResultObject<IModule>> {
    try {
      const response = await ModuleService.httpClient.post<IModule>('UpdateModule', postData, {
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
    eap: number,
    curriculumId: string
  ): Promise<IResultObject<IModule>> {
    const postData = {
      id: id,
      label: label,
      eap: eap,
      curriculumId: curriculumId
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
