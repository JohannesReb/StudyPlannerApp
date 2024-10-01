import axios from 'axios'
import type { IResultObject } from '@/domain/IResultObject'
import AccountService from './AccountService'
import type { ISubject } from '@/domain/ISubject'
import type { EAccessType } from '@/domain/EAccessType'

export default class SubjectService {
  private constructor() {}

  private static httpClient = axios.create({
    baseURL: 'http://localhost:5223/api/v1/ApiSubject/'
  })

  private static async getAllQuery(userInfo: string): Promise<IResultObject<ISubject[]>> {
    try {
      const response = await SubjectService.httpClient.get<ISubject[]>('GetSubjects', {
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

  static async getAll(userInfo: string): Promise<IResultObject<ISubject[]>> {
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

  private static async getAllPublicQuery(userInfo: string): Promise<IResultObject<ISubject[]>> {
    try {
      const response = await SubjectService.httpClient.get<ISubject[]>('GetPublicSubjects', {
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

  static async getAllPublic(userInfo: string): Promise<IResultObject<ISubject[]>> {
    let res = await this.getAllPublicQuery(userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        localStorage.setItem('userInfo', JSON.stringify(authResponse.data))
        res = await this.getAllPublicQuery(JSON.stringify(authResponse.data))
      }
    }
    return res
  }

  private static async getAllChosenQuery(userInfo: string): Promise<IResultObject<ISubject[]>> {
    try {
      const response = await SubjectService.httpClient.get<ISubject[]>('GetChosenSubjects', {
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

  static async getAllChosen(userInfo: string): Promise<IResultObject<ISubject[]>> {
    let res = await this.getAllChosenQuery(userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        localStorage.setItem('userInfo', JSON.stringify(authResponse.data))
        res = await this.getAllChosenQuery(JSON.stringify(authResponse.data))
      }
    }
    return res
  }

  private static async getAllByModuleQuery(
    moduleId: string,
    userInfo: string
  ): Promise<IResultObject<ISubject[]>> {
    try {
      const response = await SubjectService.httpClient.get<ISubject[]>(
        'GetSubjectsByModule/' + moduleId,
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

  static async getAllByModule(
    moduleId: string,
    userInfo: string
  ): Promise<IResultObject<ISubject[]>> {
    let res = await this.getAllByModuleQuery(moduleId, userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        localStorage.setItem('userInfo', JSON.stringify(authResponse.data))
        res = await this.getAllByModuleQuery(moduleId, JSON.stringify(authResponse.data))
      }
    }
    return res
  }

  private static async getAllPublicByCurriculumQuery(
    curriculumId: string,
    userInfo: string
  ): Promise<IResultObject<ISubject[]>> {
    try {
      const response = await SubjectService.httpClient.get<ISubject[]>(
        'GetPublicSubjectsByCurriculum/' + curriculumId,
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

  static async getAllPublicByCurriculum(
    curriculumId: string,
    userInfo: string
  ): Promise<IResultObject<ISubject[]>> {
    let res = await this.getAllPublicByCurriculumQuery(curriculumId, userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        localStorage.setItem('userInfo', JSON.stringify(authResponse.data))
        res = await this.getAllPublicByCurriculumQuery(
          curriculumId,
          JSON.stringify(authResponse.data)
        )
      }
    }
    return res
  }

  private static async getAllChosenByCurriculumQuery(
    curriculumId: string,
    userInfo: string
  ): Promise<IResultObject<ISubject[]>> {
    try {
      const response = await SubjectService.httpClient.get<ISubject[]>(
        'GetChosenSubjectsByCurriculum/' + curriculumId,
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

  static async getAllChosenByCurriculum(
    curriculumId: string,
    userInfo: string
  ): Promise<IResultObject<ISubject[]>> {
    let res = await this.getAllChosenByCurriculumQuery(curriculumId, userInfo)
    if (res.errors) {
      const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
      console.log(authResponse)
      if (authResponse.data) {
        localStorage.setItem('userInfo', JSON.stringify(authResponse.data))
        res = await this.getAllChosenByCurriculumQuery(
          curriculumId,
          JSON.stringify(authResponse.data)
        )
      }
    }
    return res
  }

  private static async getQuery(id: string, userInfo: string): Promise<IResultObject<ISubject>> {
    try {
      const response = await SubjectService.httpClient.get<ISubject>('GetSubject/' + id, {
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

  static async get(id: string, userInfo: string): Promise<IResultObject<ISubject>> {
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

  private static async deleteQuery(id: string, userInfo: string): Promise<IResultObject<ISubject>> {
    try {
      const response = await SubjectService.httpClient.delete('DeleteSubject/' + id, {
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

  static async delete(id: string, userInfo: string): Promise<IResultObject<ISubject>> {
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
  ): Promise<IResultObject<ISubject>> {
    try {
      const response = await SubjectService.httpClient.post<ISubject>('PostSubject', postData, {
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
    eap: number,
    moduleId: string,
    roles: string[],
    semesters: number,
    accessType: EAccessType
  ): Promise<IResultObject<ISubject>> {
    const postData = {
      subject: {
        label: label,
        description: description,
        eap: eap,
        moduleId: moduleId
      },
      roles: roles,
      semesters: semesters,
      accessType: accessType
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
  ): Promise<IResultObject<ISubject>> {
    try {
      const response = await SubjectService.httpClient.post<ISubject>('UpdateSubject', postData, {
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
    eap: number,
    moduleId: string,
    roles: string[],
    semesters: number,
    accessType: EAccessType,
    createdBy: string
  ): Promise<IResultObject<ISubject>> {
    const postData = {
      subject: {
        id: id,
        label: label,
        description: description,
        eap: eap,
        moduleId: moduleId,
        createdBy: createdBy
      },
      roles: roles,
      semesters: semesters,
      accessType: accessType
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
