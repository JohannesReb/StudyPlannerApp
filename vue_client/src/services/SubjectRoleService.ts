import axios from 'axios'
import type { IResultObject } from '@/domain/IResultObject'
import AccountService from './AccountService'
import type { ISubjectRole } from '@/domain/ISubjectRole'

export default class SubjectRoleService {
  private constructor() {}

  private static httpClient = axios.create({
    baseURL: 'http://localhost:5223/api/v1/ApiSubjectRoles/'
  })

  private static async getAllQuery(
    subjectId: string,
    userInfo: string
  ): Promise<IResultObject<ISubjectRole[]>> {
    try {
      const response = await SubjectRoleService.httpClient.get<ISubjectRole[]>(
        'GetSubjectRoles/' + subjectId,
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

  static async getAll(subjectId: string, userInfo: string): Promise<IResultObject<ISubjectRole[]>> {
    let res = await this.getAllQuery(subjectId, userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        localStorage.setItem('userInfo', JSON.stringify(authResponse.data))
        res = await this.getAllQuery(subjectId, JSON.stringify(authResponse.data))
      }
    }
    return res
  }
}
