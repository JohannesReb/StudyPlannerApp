"use client"

import AccountService from "@/services/AccountService";
import { AppContext } from "@/state/AppContext";
import { useRouter } from "next/navigation";
import { useContext, useState } from "react";

export default function Register() {
    const router = useRouter();
    const [firstName, setFirstName] = useState("aa");
    const [lastName, setLastName] = useState("aa");
    const [email, setEmail] = useState("aa@aa.ee");
    const [pwd, setPwd] = useState("Kala.maja1");
    const [pwdConf, setPwdConf] = useState("Kala.maja1");

    const [validationError, setvalidationError] = useState("");

    const { userInfo, setUserInfo } = useContext(AppContext)!;

    const validateAndRegister = async () => {
        if (email.length < 5 || pwd.length < 6) {
            setvalidationError("Invalid input lengths");
            return;
        }
        if (pwd != pwdConf) {
            setvalidationError("Passwords must be equal");
            return;
        }

        const response = await AccountService.register(firstName, lastName, email, pwd);
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
      <>
        <div className="row">
          <div className="col-md-4">
                <h2>Create a new account.</h2>
                <hr />
                
                <div className="form-floating mb-3">
                    <input
                      value={firstName}
                      onChange={(e) => { setFirstName(e.target.value); setvalidationError(""); }}
                      id="firstName" className="form-control" autoComplete="firstname" placeholder="FirstName"/>
                    <label htmlFor="Input_FirstName">First name</label>
                </div>
                <div className="form-floating mb-3">
                    <input
                      value={lastName}
                      onChange={(e) => { setLastName(e.target.value); setvalidationError(""); }}
                      id="lastName" className="form-control" autoComplete="lastname" placeholder="LastName"/>
                    <label htmlFor="Input_LastName">Last name</label>
                </div>
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
                <div className="form-floating mb-3">
                    <input 
                      value={pwdConf}
                      onChange={(e) => { setPwdConf(e.target.value); setvalidationError(""); }}
                      id="confirm-password" type="password" className="form-control" autoComplete="password" placeholder="password" />
                    <label htmlFor="Input_ConfirmPassword">Confirm Password</label>
                </div>
                <div>
                    <button onClick={(e) => validateAndRegister()} className="w-100 btn btn-lg btn-primary">Register</button>
                </div>
          </div>
        </div>
      </>
    );
}
