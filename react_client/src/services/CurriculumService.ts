import { IUserInfo } from "@/state/AppContext";
import axios, { AxiosResponse } from "axios";
import { IResultObject } from "./IResultObject";
import { ICurriculum } from "@/domain/ICurriculum";
import AccountService from "./AccountService";

export default class CurriculumService {
    private constructor() {

    }

    private static httpClient = axios.create({
        baseURL: 'http://localhost:5223/api/v1/ApiCurriculums/',
    });

    static async getAll(): Promise<IResultObject<ICurriculum[]>>{
        try {
            const response = await CurriculumService.httpClient.get<ICurriculum[]>("GetCurriculums");

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

    static async get(id: string): Promise<IResultObject<ICurriculum>>{
        try {
            const response = await CurriculumService.httpClient.get<ICurriculum>("GetCurriculum/" + id);

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

    static async delete(id: string, userInfo: string): Promise<IResultObject<ICurriculum>>{
        try {
            const response = await CurriculumService.httpClient.delete("DeleteCurriculum/" + id, {
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
        code: string,
        label: string,
        manager: string,
        from: string,
        until: string,
        semesters: number): Promise<IResultObject<ICurriculum>>{
        const postData = {
            code: code,
            label: label,
            manager: manager,
            from: from,
            until: until,
            semesters: semesters
        }
        try {
            const response = await CurriculumService.httpClient.post<ICurriculum>("PostCurriculum", postData, {
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
        code: string,
        label: string,
        manager: string,
        from: string,
        until: string,
        semesters: number): Promise<IResultObject<ICurriculum>>{
        const postData = {
            id: id,
            code: code,
            label: label,
            manager: manager,
            from: from,
            until: until,
            semesters: semesters
        }
        try {
            const response = await CurriculumService.httpClient.put<ICurriculum>("PutCurriculum/" + id, postData, {
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