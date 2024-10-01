import axios from 'axios'
import type { IResultObject } from '@/domain/IResultObject'
import type { ICurriculum } from '@/domain/ICurriculum'
import AccountService from './AccountService'

export default class CurriculumService {
  private constructor() {}

  private static httpClient = axios.create({
    baseURL: 'http://localhost:5223/api/v1/ApiCurriculums/'
  })

  static async getAll(): Promise<IResultObject<ICurriculum[]>> {
    try {
      const response = await CurriculumService.httpClient.get<ICurriculum[]>('GetCurriculums')

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

  static async get(id: string): Promise<IResultObject<ICurriculum>> {
    try {
      const response = await CurriculumService.httpClient.get<ICurriculum>('GetCurriculum/' + id)

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

  private static async deleteQuery(
    id: string,
    userInfo: string
  ): Promise<IResultObject<ICurriculum>> {
    try {
      const response = await CurriculumService.httpClient.delete('DeleteCurriculum/' + id, {
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

  static async delete(id: string, userInfo: string): Promise<IResultObject<ICurriculum>> {
    let res = await this.deleteQuery(id, userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        localStorage.setItem('userInfo', JSON.stringify(authResponse.data, null, 4))
        res = await this.deleteQuery(id, JSON.stringify(authResponse.data, null, 4))
      }
    }

    return res
  }

  private static async postQuery(
    postData: object,
    userInfo: string
  ): Promise<IResultObject<ICurriculum>> {
    try {
      const response = await CurriculumService.httpClient.post<ICurriculum>(
        'PostCurriculum',
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
    label: string,
    code: string,
    manager: string,
    from: string,
    until: string,
    semesters: number
  ): Promise<IResultObject<ICurriculum>> {
    const postData = {
      label: label,
      code: code,
      manager: manager,
      from: from,
      until: until,
      semesters: semesters
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
  ): Promise<IResultObject<ICurriculum>> {
    try {
      const response = await CurriculumService.httpClient.post<ICurriculum>(
        'UpdateCurriculum',
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
    code: string,
    label: string,
    manager: string,
    from: string,
    until: string,
    semesters: number
  ): Promise<IResultObject<ICurriculum>> {
    const postData = {
      id: id,
      code: code,
      label: label,
      manager: manager,
      from: from,
      until: until,
      semesters: semesters
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
