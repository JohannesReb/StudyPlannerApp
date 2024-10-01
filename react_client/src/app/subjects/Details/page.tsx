"use client"
import { ISubject } from "@/domain/ISubject";
import SubjectService from "@/services/SubjectService";
import { useEffect, useState } from "react";

export default function Details() {
    const [isLoading, setIsLoading] = useState(true);
    const [subject, setSubject] = useState<ISubject>();
    const userInfo = "typeof window !== 'undefined' ? localStorage.getItem('userInfo') : null"
    const subjectId = "typeof window !== 'undefined' ? localStorage.getItem(subjectId) : null"

    const loadData = async () => {
        const response = await SubjectService.get(subjectId!, userInfo!)
        if (response.data) {
            setSubject(response.data);
            console.log(subject)
        }
        setIsLoading(false);
    };
    useEffect(() => { loadData() }, []);

    if (isLoading) return (<h1>Details - LOADING</h1>);
    return (
        <>
        <h1>Details</h1>
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
                <a href="./">Back to List</a>
            </div>
        </>
    );
  }