"use client"

import { ISubject } from "@/domain/ISubject";
import SubjectService from "@/services/SubjectService";
import { AppContext } from "@/state/AppContext";
import { useContext, useEffect, useState } from "react";

export default function Subjects() {
    const [isLoading, setIsLoading] = useState(true);
    const [Subjects, setSubjects] = useState<ISubject[]>([]);
    // const { userInfo, setUserInfo } = useContext(AppContext)!;
    const [SubjectId, setSubjectId] = useState("");
    const userInfo = "typeof window !== 'undefined' ? localStorage.getItem('userInfo') : null"


    const loadData = async () => {
        const response = await SubjectService.getAll(userInfo!)
        console.log(response)
        if (response.data) {
          setSubjects(response.data);
        }

        setIsLoading(false);
    };
    useEffect(() => { loadData() }, []);

    if (isLoading) return (<h1>Subjects - LOADING</h1>);

    return (
        <>
            <h1>Subjects</h1>

            <p>
                <a href="/subjects/Create">Create New</a>
            </p>
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
                            ModuleId
                        </th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {Subjects.map((item) =>
                        <tr key={item.id}>
                            <td>
                                {item.label}
                            </td>
                            <td>
                                {item.description}
                            </td>
                            <td>
                                {item.eap}
                            </td>
                            <td>
                                {item.moduleId}
                            </td>
                            <td>
                                <a onClick={() => "localStorage.setItem(subjectId, item.id)"} href="/subjects/Edit/">Edit</a> |
                                <a onClick={() => "localStorage.setItem(subjectId, item.id)"} href="/subjects/Details/">Details</a> |
                                <a onClick={() => "localStorage.setItem(subjectId, item.id)"} href="/subjects/Delete/">Delete</a>
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
        </>
    );
}