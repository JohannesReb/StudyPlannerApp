"use client"

import { ICurriculum } from "@/domain/ICurriculum";
import CurriculumService from "@/services/CurriculumService";
import { AppContext } from "@/state/AppContext";
import { useContext, useEffect, useState } from "react";

export default function Curricula() {
    const [isLoading, setIsLoading] = useState(true);
    const [curricula, setCurricula] = useState<ICurriculum[]>([]);
    const { userInfo, setUserInfo } = useContext(AppContext)!;
    const [curriculumId, setCurriculumId] = useState("");


    const loadData = async () => {
        const response = await CurriculumService.getAll()
        if (response.data) {
          setCurricula(response.data);
        }

        setIsLoading(false);
    };
    useEffect(() => { loadData() }, []);

    if (isLoading) return (<h1>Curricula - LOADING</h1>);

    return (
        <>
            <h1>Curricula</h1>

            <p>
                <a href="/curricula/Create">Create New</a>
            </p>
            <table className="table">
                <thead>
                    <tr>
                        <th>
                            Code
                        </th>
                        <th>
                            Title
                        </th>
                        <th>
                            Manager
                        </th>
                        <th>
                            Semesters
                        </th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {curricula.map((item) =>
                        <tr key={item.id}>
                            <td>
                                {item.code}
                            </td>
                            <td>
                                {item.label}
                            </td>
                            <td>
                                {item.manager}
                            </td>
                            <td>
                                {item.semesters}
                            </td>
                            <td>
                                <a onClick={() => "typeof window !== 'undefined' ? localStorage.setItem(curriculumId, item.id) : null"} href="/curricula/Edit/">Edit</a> |
                                <a onClick={() => "typeof window !== 'undefined' ? localStorage.setItem(curriculumId, item.id) : null"} href="/curricula/Details/">Details</a> |
                                <a onClick={() => "typeof window !== 'undefined' ? localStorage.setItem(curriculumId, item.id) : null"} href="/curricula/Delete/">Delete</a>
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
        </>
    );
}