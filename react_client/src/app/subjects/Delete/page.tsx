"use client"

import { ISubject } from "@/domain/ISubject";
import SubjectService from "@/services/SubjectService";
import { AppContext } from "@/state/AppContext";
import { useContext, useEffect, useState } from "react";

export default function DeleteSubject() {
    const [isLoading, setIsLoading] = useState(true);
    const [subject, setSubject] = useState<ISubject>();
    const [validationError, setvalidationError] = useState("");
    const userInfo = "typeof window !== 'undefined' ? localStorage.getItem('userInfo') : null"
    const subjectId = "typeof window !== 'undefined' ? localStorage.getItem(subjectId) : null"
    const Delete = async () => {
        if (userInfo != undefined){
            const response = await SubjectService.delete(subjectId!, userInfo);
            if (response.errors && response.errors.length > 0) {
                setvalidationError(response.errors[0]);
            } else {
                window.location.replace("/subjects");
            }
        } else {
            window.location.replace("/Identity/login");
        }
    };

    const loadData = async () => {
        const response = await SubjectService.get(subjectId!, userInfo!)
        if (response.data) {
            setSubject(response.data);
        }
        setIsLoading(false);
    };
    useEffect(() => { loadData() }, []);

    if (isLoading) return (<h1>Delete Subject - LOADING</h1>);
    return (
        <>
            <h1>Delete Subject</h1>
            <div className="text-danger" role="alert">{validationError}</div>
            <table className="table">
                <thead>
                    <tr>
                        <th>
                            Label
                        </th>
                        <th>
                            Description
                        </th>
                        <th>
                            EAP
                        </th>
                        <th>
                            Module
                        </th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr key={subject!.id}>
                        <td>
                            {subject!.label}
                        </td>
                        <td>
                            {subject!.description}
                        </td>
                        <td>
                            {subject!.eap}
                        </td>
                        <td>
                            {subject!.moduleId}
                        </td>
                    </tr>
                </tbody>
            </table>
            <div>
                <button onClick={(e) => Delete()} className="btn btn-lg btn-primary">Delete</button>
            </div>
            <div>
                <a href="./">Back to List</a>
            </div>
        </>
    );
}