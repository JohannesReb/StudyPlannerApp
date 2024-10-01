import { IUserInfo } from "@/state/AppContext";
import axios, { AxiosResponse } from "axios";
import { IResultObject } from "./IResultObject";
import AccountService from "./AccountService";
import { ISubject } from "@/domain/ISubject";

export default class SubjectService {
    private constructor() {

    }

    private static httpClient = axios.create({
        baseURL: 'http://localhost:5223/api/v1/ApiSubject/',
    });

    static async getAll(userInfo: string): Promise<IResultObject<ISubject[]>>{
        try {
            const response = await SubjectService.httpClient.get<ISubject[]>("GetSubjects",  {
                headers: {
                "Authorization": "Bearer " + JSON.parse(userInfo).jwt
            }});

            if (response.status < 300) {
                return {
                    data: response.data
                }
            }
            return {
                errors: [response.status.toString() + " " + response.statusText]
            }
        } catch (error: any) {
            return {
                errors: [JSON.stringify(error)]
            };
        }
    }

    static async get(id: string, userInfo: string): Promise<IResultObject<ISubject>>{
        try {
            const response = await SubjectService.httpClient.get<ISubject>("GetSubject/" + id,  {
                headers: {
                "Authorization": "Bearer " + JSON.parse(userInfo).jwt
            }});

            if (response.status < 300) {
                return {
                    data: response.data
                }
            }
            return {
                errors: [response.status.toString() + " " + response.statusText]
            }
        } catch (error: any) {
            return {
                errors: [JSON.stringify(error)]
            };
        }
    }

    static async delete(id: string, userInfo: string): Promise<IResultObject<ISubject>>{
        try {
            const response = await SubjectService.httpClient.delete("DeleteSubject/" + id, {
                headers: {
                    "Authorization": "Bearer " + JSON.parse(userInfo).jwt
                }});
            if (response.status < 300) {
                return {
                    data: response.data
                }
            }
            // else if(response.status == 401){
            //     const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
            //     if (authResponse.data) {
            //         localStorage.setItem('userInfo', JSON.stringify(authResponse.data, null, 4));
            //         const newResponse = await CurriculumService.httpClient.delete("DeleteCurriculum/" + id, {
            //             headers: {
            //                 "Authorization": "Bearer " + JSON.parse(userInfo).jwt
            //             }});

            //         if (newResponse.status < 300) {
            //             return {
            //                 data: response.data
            //             }
            //         }
            //     }
            // }
            return {
                errors: [response.status.toString() + " " + response.statusText]
            }
        } catch (error: any) {
            return {
                
                errors: [JSON.stringify(error)]
            };
        }
    }

    static async post(userInfo: string,
        label: string,
        description: string,
        eap: number,
        moduleId: string): Promise<IResultObject<ISubject>>{
        
        const postData = {
            label: label,
            description: description,
            eap: eap,
            moduleId: moduleId
        }
        try {
            const response = await SubjectService.httpClient.post<ISubject>("PostSubject", postData, {
                headers: {
                "Authorization": "Bearer " + JSON.parse(userInfo).jwt
            }});
            if (response.status < 300) {
                return {
                    data: response.data
                }
            }
            return {
                errors: [response.status.toString() + " " + response.statusText]
            }
        } catch (error: any) {
            console.log(error)
            // if (error.response){
            //     if(error.response.status === 401){
            //         try{
            //             const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
            //             console.log(authResponse)
            //             if (authResponse.data) {
            //                 localStorage.setItem('userInfo', JSON.stringify(authResponse.data, null, 4));
            //                 const newResponse = await CurriculumService.httpClient.post<ICurriculum>("PostCurriculum", postData, {
            //                     headers: {
            //                     "Authorization": "Bearer " + authResponse.data.jwt
            //                 }});
                
            //                 if (newResponse.status < 300) {
            //                     return {
            //                         data: newResponse.data
            //                     }
            //                 }
            //             }
            //         }
            //         catch(error: any){
            //             return{
            //                 errors: [JSON.stringify(error)]
            //             }
                        
            //         }
            //     }
            // }
            return {
                errors: [JSON.stringify(error)]
            };
        }
    }

    
    static async put(userInfo: string,
        id: string,
        label: string,
        description: string,
        eap: number,
        moduleId: string): Promise<IResultObject<ISubject>>{
        const putData = {
            id: id,
            label: label,
            description: description,
            eap: eap,
            moduleId: moduleId
        }
        try {
            const response = await SubjectService.httpClient.put<ISubject>("PutSubject/" + id, putData, {
                headers: {
                "Authorization": "Bearer " + JSON.parse(userInfo).jwt
            }});
            if (response.status < 300) {
                return {
                    data: response.data
                }
            }
            return {
                errors: [response.status.toString() + " " + response.statusText]
            }
        } catch (error: any) {
            console.log(error)
            // if (error.response){
            //     if(error.response.status === 401){
            //         try{
            //             const authResponse = await AccountService.refreshTokenData(JSON.parse(userInfo))
            //             console.log(authResponse)
            //             if (authResponse.data) {
            //                 localStorage.setItem('userInfo', JSON.stringify(authResponse.data, null, 4));
            //                 const newResponse = await CurriculumService.httpClient.post<ICurriculum>("PostCurriculum", postData, {
            //                     headers: {
            //                     "Authorization": "Bearer " + authResponse.data.jwt
            //                 }});
                
            //                 if (newResponse.status < 300) {
            //                     return {
            //                         data: newResponse.data
            //                     }
            //                 }
            //             }
            //         }
            //         catch(error: any){
            //             return{
            //                 errors: [JSON.stringify(error)]
            //             }
                        
            //         }
            //     }
            // }
            return {
                errors: [JSON.stringify(error)]
            };
        }
    }
}