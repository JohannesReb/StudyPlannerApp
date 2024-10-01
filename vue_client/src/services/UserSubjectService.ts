import axios from 'axios'
import type { IResultObject } from '@/domain/IResultObject'
import AccountService from './AccountService'
import type { IUserSubject } from '@/domain/IUserSubject'
import type { EStatus } from '@/domain/EStatus'

export default class UserSubjectService {
  private constructor() {}

  private static httpClient = axios.create({
    baseURL: 'http://localhost:5223/api/v1/ApiUserSubjects/'
  })

  private static async getAllQuery(userInfo: string): Promise<IResultObject<IUserSubject[]>> {
    try {
      const response = await UserSubjectService.httpClient.get<IUserSubject[]>('GetUserSubjects', {
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

  static async getAll(userInfo: string): Promise<IResultObject<IUserSubject[]>> {
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

  private static async getQuery(
    id: string,
    userInfo: string
  ): Promise<IResultObject<IUserSubject>> {
    try {
      const response = await UserSubjectService.httpClient.get<IUserSubject>(
        'GetUserSubject/' + id,
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

  static async get(id: string, userInfo: string): Promise<IResultObject<IUserSubject>> {
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
  ): Promise<IResultObject<IUserSubject>> {
    try {
      const response = await UserSubjectService.httpClient.delete('DeleteUserSubject/' + id, {
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

  static async delete(id: string, userInfo: string): Promise<IResultObject<IUserSubject>> {
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
  ): Promise<IResultObject<IUserSubject>> {
    try {
      const response = await UserSubjectService.httpClient.post<IUserSubject>(
        'PostUserSubject',
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
    grade: number | null,
    status: EStatus,
    semester: number,
    subjectId: string
  ): Promise<IResultObject<IUserSubject>> {
    const postData = {
      grade: grade,
      status: status,
      semester: semester,
      subjectId: subjectId
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
  ): Promise<IResultObject<IUserSubject>> {
    try {
      const response = await UserSubjectService.httpClient.post<IUserSubject>(
        'UpdateUserSubject',
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
    grade: number | null,
    status: EStatus,
    semester: number,
    subjectId: string
  ): Promise<IResultObject<IUserSubject>> {
    const postData = {
      id: id,
      grade: grade,
      status: status,
      semester: semester,
      subjectId: subjectId
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
