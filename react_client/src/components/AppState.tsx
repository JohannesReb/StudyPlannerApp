"use client"

import { AppContext, IUserInfo } from "@/state/AppContext";
import { useState } from "react";

export default function AppState({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {

    const [userInfo, setUserInfo] = useState<IUserInfo | null>(null);

    return (
        <AppContext.Provider value={{ userInfo, setUserInfo }}>
            {children}
        </AppContext.Provider>
    );
}