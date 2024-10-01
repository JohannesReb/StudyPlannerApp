"use client"

import AccountService from "@/services/AccountService";
import { AppContext, IUserInfo } from "@/state/AppContext";
import Link from "next/link";
import { useContext, useState } from "react";

export default function Identity() {
    const userInfo = localStorage.getItem('userInfo');

    console.log(userInfo)
    return userInfo ? <LoggedIn /> : <LoggedOut />;

}


const LoggedIn = () => {
    const userInfo = JSON.parse(localStorage.getItem('userInfo')!);
    const [validationError, setvalidationError] = useState("");
    
    const doLogout = async () => {
        localStorage.clear();
        const response = await AccountService.logout(userInfo);
        if (response.data) {
            localStorage.setItem('logoutInfo', JSON.stringify(response.data, null, 4));
        }

        if (response.errors && response.errors.length > 0) {
            setvalidationError(response.errors[0]);
        }
        window.location.replace("/");
    }

    return (
        <ul className="navbar-nav">
            <li className="nav-item">
                <Link href="/" className="nav-link text-dark" title="Manage">Hello {userInfo!.firstname} {userInfo!.lastname}!</Link>
            </li>
            <li className="nav-item">
                <Link onClick={(e) => doLogout()} href="/" className="nav-link text-dark" title="Logout">Logout</Link>
            </li>
        </ul>
    );
}

const LoggedOut = () => {
    return (
        <ul className="navbar-nav">
            <li className="nav-item">
                <Link href="/Identity/register" className="nav-link text-dark">Register</Link>
            </li>
            <li className="nav-item">
                <Link href="/Identity/login" className="nav-link text-dark">Login</Link>
            </li>
        </ul>
    );
}