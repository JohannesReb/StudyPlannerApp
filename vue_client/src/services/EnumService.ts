import axios from 'axios'
import type { IResultObject } from '@/domain/IResultObject'
import AccountService from './AccountService'

export default class EnumService {
  private constructor() {}

  private static httpClient = axios.create({
    baseURL: 'http://localhost:5223/api/v1/Enums/'
  })

  static async GetAccessTypes(): Promise<IResultObject<object>> {
    try {
      const response = await EnumService.httpClient.get<object>('GetAccessTypes')

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

  static async GetFields(): Promise<IResultObject<object>> {
    try {
      const response = await EnumService.httpClient.get<object>('GetFields')

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

  static async GetSemesters(): Promise<IResultObject<object>> {
    try {
      const response = await EnumService.httpClient.get<object>('GetSemesters')

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

  static async GetStatuses(): Promise<IResultObject<object>> {
    try {
      const response = await EnumService.httpClient.get<object>('GetStatuses')

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

  static async GetTaskTypes(): Promise<IResultObject<object>> {
    try {
      const response = await EnumService.httpClient.get<object>('GetTaskTypes')

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
}
