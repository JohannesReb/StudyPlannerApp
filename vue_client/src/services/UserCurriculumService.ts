import axios from 'axios'
import type { IResultObject } from '@/domain/IResultObject'
import AccountService from './AccountService'
import type { IUserCurriculum } from '@/domain/IUserCurriculum'
import type { EStatus } from '@/domain/EStatus'

export default class UserCurriculumService {
  private constructor() {}

  private static httpClient = axios.create({
    baseURL: 'http://localhost:5223/api/v1/ApiUserCurriculum/'
  })

  private static async getQuery(userInfo: string): Promise<IResultObject<IUserCurriculum>> {
    try {
      const response = await UserCurriculumService.httpClient.get<IUserCurriculum>(
        'GetUserCurriculum',
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

  static async get(userInfo: string): Promise<IResultObject<IUserCurriculum>> {
    let res = await this.getQuery(userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        localStorage.setItem('userInfo', JSON.stringify(authResponse.data))
        res = await this.getQuery(JSON.stringify(authResponse.data))
      }
    }
    return res
  }

  private static async deleteQuery(
    id: string,
    userInfo: string
  ): Promise<IResultObject<IUserCurriculum>> {
    try {
      const response = await UserCurriculumService.httpClient.delete('DeleteUserCurriculum/' + id, {
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

  static async delete(id: string, userInfo: string): Promise<IResultObject<IUserCurriculum>> {
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
  ): Promise<IResultObject<IUserCurriculum>> {
    try {
      const response = await UserCurriculumService.httpClient.post<IUserCurriculum>(
        'PostUserCurriculum',
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
    status: EStatus,
    curriculumId: string
  ): Promise<IResultObject<IUserCurriculum>> {
    const postData = {
      status: status,
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
  ): Promise<IResultObject<IUserCurriculum>> {
    try {
      const response = await UserCurriculumService.httpClient.post<IUserCurriculum>(
        'UpdateUserCurriculum',
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
    status: EStatus,
    curriculumId: string
  ): Promise<IResultObject<IUserCurriculum>> {
    const postData = {
      id: id,
      status: status,
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
