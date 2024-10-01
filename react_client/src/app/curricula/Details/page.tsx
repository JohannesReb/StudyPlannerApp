"use client"
import { ICurriculum } from "@/domain/ICurriculum";
import CurriculumService from "@/services/CurriculumService";
import { useEffect, useState } from "react";

export default function Details() {
    const [isLoading, setIsLoading] = useState(true);
    const [curriculum, setCurriculum] = useState<ICurriculum>();
    const curriculumId = "typeof window !== 'undefined' ? localStorage.getItem(curriculumId) : null"

    const loadData = async () => {
        const response = await CurriculumService.get(curriculumId!)
        if (response.data) {
            setCurriculum(response.data);
            console.log(curriculum)
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
                            Code
                        </th>
                        <th>
                            Title
                        </th>
                        <th>
                            Manager
                        </th>
                        <th>
                            From
                        </th>
                        <th>
                            Until
                        </th>
                        <th>
                            Semesters
                        </th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr key={curriculum!.id}>
                        <td>
                            {curriculum!.code}
                        </td>
                        <td>
                            {curriculum!.label}
                        </td>
                        <td>
                            {curriculum!.manager}
                        </td>
                        <td>
                            {new Date(curriculum!.from).toLocaleDateString()}
                        </td>
                        <td>
                            {new Date(curriculum!.until).toLocaleDateString()}
                        </td>
                        <td>
                            {curriculum!.semesters}
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