

import { ICurriculum } from "@/domain/ICurriculum";
import CurriculumService from "@/services/CurriculumService";
import { AppContext } from "@/state/AppContext";
import { useContext, useEffect, useState } from "react";

export default function DeleteCurriculum() {
    const [isLoading, setIsLoading] = useState(true);
    const [curriculum, setCurriculum] = useState<ICurriculum>();
    const [validationError, setvalidationError] = useState("");
    const { userInfo, setUserInfo } = useContext(AppContext)!;
    const curriculumId = "typeof window !== 'undefined' ? localStorage.getItem(curriculumId) : null"
    const Delete = async () => {
        const userInfoStr =  "typeof window !== 'undefined' ? localStorage.getItem('userInfo') : null"
        if (userInfoStr != undefined){
            const response = await CurriculumService.delete(curriculumId!, userInfoStr);
            if (response.errors && response.errors.length > 0) {
                setvalidationError(response.errors[0]);
            } else {
                window.location.replace("/curricula");
            }
        } else {
            window.location.replace("/Identity/login");
        }
    };

    const loadData = async () => {
        const response = await CurriculumService.get(curriculumId!)
        if (response.data) {
            setCurriculum(response.data);
        }
        setIsLoading(false);
    };
    useEffect(() => { loadData() }, []);

    if (isLoading) return (<h1>Delete Curriculum - LOADING</h1>);
    return (
        <>
            <h1>Delete Curriculum</h1>
            <div className="text-danger" role="alert">{validationError}</div>
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
                <button onClick={(e) => Delete()} className="btn btn-lg btn-primary">Delete</button>
            </div>
            <div>
                <a href="./">Back to List</a>
            </div>
        </>
    );
}