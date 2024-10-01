"use client"

import AccountService from "@/services/AccountService";
import { AppContext } from "@/state/AppContext";
import { useRouter } from "next/navigation";
import { useContext, useState } from "react";

export default function Login() {
    const router = useRouter();
    const [email, setEmail] = useState("admin@eesti.ee");
    const [pwd, setPwd] = useState("Kala.maja1");

    const [validationError, setvalidationError] = useState("");

    const { userInfo, setUserInfo } = useContext(AppContext)!;

    const validateAndLogin = async () => {
        if (email.length < 5 || pwd.length < 6) {
            setvalidationError("Invalid input lengths");
            return;
        }

        const response = await AccountService.login(email, pwd);
        if (response.data) {
            "typeof window !== 'undefined' ? localStorage.setItem('userInfo', JSON.stringify(response.data, null, 4)) : null;"
            setUserInfo(response.data);
            //router.push("/");
            window.location.replace("/");
        }

        if (response.errors && response.errors.length > 0) {
            setvalidationError(response.errors[0]);
        }
    }

    return (
        <div className="row">
            <div className="col-md-5">
                <h2>Log in</h2>
                <hr />
                <div className="text-danger" role="alert">{validationError}</div>
                <div className="form-floating mb-3">
                    <input
                        value={email}
                        onChange={(e) => { setEmail(e.target.value); setvalidationError(""); }}
                        id="email" type="email" className="form-control" autoComplete="email" placeholder="name@example.com" />
                    <label htmlFor="email" className="form-label">Email</label>
                </div>
                <div className="form-floating mb-3">
                    <input
                        value={pwd}
                        onChange={(e) => { setPwd(e.target.value); setvalidationError(""); }}
                        id="password" type="password" className="form-control" autoComplete="password" placeholder="password" />
                    <label htmlFor="password" className="form-label">Password</label>
                </div>
                <div>
                    <button onClick={(e) => validateAndLogin()} className="w-100 btn btn-lg btn-primary">Log in</button>
                </div>
            </div>
        </div>

    );
}
