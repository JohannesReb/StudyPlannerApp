import { IUserInfo } from "@/state/AppContext";
import axios, { AxiosResponse } from "axios";
import { IResultObject } from "./IResultObject";
import AccountService from "./AccountService";
import { IModule } from "@/domain/IModule";

export default class ModuleService {
    private constructor() {

    }

    private static httpClient = axios.create({
        baseURL: 'http://localhost:5223/api/v1/ApiModules/',
    });

    static async getAll(curriculumId: string): Promise<IResultObject<IModule[]>>{
        try {
            const response = await ModuleService.httpClient.get<IModule[]>("GetModulesByCurriculum/" + curriculumId);

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

    static async get(id: string): Promise<IResultObject<IModule>>{
        try {
            const response = await ModuleService.httpClient.get<IModule>("GetModule/" + id);

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

    static async delete(id: string, userInfo: string): Promise<IResultObject<IModule>>{
        try {
            const response = await ModuleService.httpClient.delete("DeleteModule/" + id, {
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
        eap: number,
        curriculumId: string): Promise<IResultObject<IModule>>{
        
        const postData = {
            label: label,
            eap: eap,
            curriculumId: curriculumId
        }
        try {
            const response = await ModuleService.httpClient.post<IModule>("PostModule", postData, {
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
        eap: string,
        curriculumId: string): Promise<IResultObject<IModule>>{
        const putData = {
            id: id,
            label: label,
            eap: eap,
            curriculumId: curriculumId
        }
        try {
            const response = await ModuleService.httpClient.put<IModule>("PutModule/" + id, putData, {
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