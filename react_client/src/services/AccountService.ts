import { IUserInfo } from "@/state/AppContext";
import axios from "axios";
import { IResultObject } from "./IResultObject";

export default class AccountService {
    private constructor() {

    }

    private static httpClient = axios.create({
        baseURL: 'http://localhost:5223/api/v1/identity/account/',
    });

    static async register(firstName: string, lastName: string, email: string, pwd: string): Promise<IResultObject<IUserInfo>> {
        const registerData = {
            firstName: firstName,
            lastName: lastName,
            email: email,
            password: pwd
        }
        try {
            const response = await AccountService.httpClient.post<IUserInfo>("register", registerData);
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


    static async login(email: string, pwd: string): Promise<IResultObject<IUserInfo>> {
        const loginData = {
            email: email,
            password: pwd
        }
        try {
            const response = await AccountService.httpClient.post<IUserInfo>("login", loginData);
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


    static async logout(userInfo: IUserInfo): Promise<IResultObject<IUserInfo>> {
        const logoutData = {
            refreshToken: userInfo.refreshToken}
        try {
            const response = await AccountService.httpClient.post<IUserInfo>("logout", logoutData, {
                headers: {
                    "Authorization": "Bearer " + userInfo.jwt
                }
            });
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


    static async refreshTokenData(userInfo: IUserInfo): Promise<IResultObject<IUserInfo>> {
        try {
            const response = await AccountService.httpClient.post<IUserInfo>("refreshTokenData", userInfo);
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
}