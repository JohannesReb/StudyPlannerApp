import axios from 'axios'
import type { IResultObject } from '@/domain/IResultObject'
import AccountService from './AccountService'
import type { IRole } from '@/domain/IRole'

export default class RoleService {
  private constructor() {}

  private static httpClient = axios.create({
    baseURL: 'http://localhost:5223/api/v1/ApiRoles/'
  })

  private static async getAllQuery(userInfo: string): Promise<IResultObject<IRole[]>> {
    try {
      const response = await RoleService.httpClient.get<IRole[]>('GetRoles', {
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

  static async getAll(userInfo: string): Promise<IResultObject<IRole[]>> {
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
}
