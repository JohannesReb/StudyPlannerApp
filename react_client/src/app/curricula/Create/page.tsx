
import { ICurriculum } from "@/domain/ICurriculum";
import CurriculumService from "@/services/CurriculumService";
import { AppContext } from "@/state/AppContext";
import { useContext, useEffect, useState } from "react";

export default function Create() {
    const [curriculum, setCurriculum] = useState<ICurriculum>();
    //const { userInfo, setUserInfo } = useContext(AppContext)!;

    const [validationError, setvalidationError] = useState("");

    const [code, setCode] = useState("a");
    const [label, setLabel] = useState("a");
    const [manager, setManager] = useState("a");
    const [from, setFrom] = useState("2024-05-22T20:02");
    const [until, setUntil] = useState("2024-05-25T20:02");
    const [semesters, setSemesters] = useState(4);


    const ValidateAndSave = async () => {

        if (code == "" || label == "" || manager == ""){
            setvalidationError("Code, Label and Manager fields are required!");
            return;
        }
        const userInfo = "typeof window !== 'undefined' ? localStorage.getItem('userInfo') : null"
        if (userInfo != undefined){
            const response = await CurriculumService.post(userInfo, code, label, manager, from, until, semesters)
            if (response.data) {
                setCurriculum(response.data);
                window.location.replace("/curricula");
            }

            if (response.errors && response.errors.length > 0) {
                setvalidationError(response.errors[0]);
            }
        } else{
            window.location.replace("/Identity/login");
        }
    };

    return (
        <>
        <div className="row">
            <div className="col-md-5">
                <h2>Create Curriculum</h2>
                <hr />
                <div className="text-danger" role="alert">{validationError}</div>
                <div className="form-floating mb-3">
                    <input
                        value={code}
                        onChange={(e) => { setCode(e.target.value); setvalidationError(""); }}
                        id="code" type="text" className="form-control" autoComplete="code" placeholder="XYZ9999" />
                    <label htmlFor="code" className="form-label">Code</label>
                </div>
                <div className="form-floating mb-3">
                    <input
                        value={label}
                        onChange={(e) => { setLabel(e.target.value); setvalidationError(""); }}
                        id="label" type="text" className="form-control" autoComplete="label" placeholder="Label" />
                    <label htmlFor="label" className="form-label">Label</label>
                </div>
                <div className="form-floating mb-3">
                    <input
                        value={manager}
                        onChange={(e) => { setManager(e.target.value); setvalidationError(""); }}
                        id="manager" type="text" className="form-control" autoComplete="manager" placeholder="Võsa Pets" />
                    <label htmlFor="manager" className="form-label">Manager</label>
                </div>

                <div className="form-group">
                    <label className="form-label" htmlFor="from">From</label>
                    <input 
                    value={from}
                    onChange={(e) => { setFrom(e.target.value); setvalidationError(""); }}
                    className="form-control" type="datetime-local" id="from"/>
                </div>
                <div className="form-group">
                    <label className="form-label" htmlFor="until">Until</label>
                    <input 
                    value={until}
                    onChange={(e) => { setUntil(e.target.value); setvalidationError(""); }}
                    className="form-control" type="datetime-local" id="until"/>

                </div>
                <div className="form-floating mb-3">
                    <input
                        value={semesters}
                        onChange={(e) => { setSemesters(Number.parseInt(e.target.value)); setvalidationError(""); }}
                        id="semesters" type="text" className="form-control" autoComplete="semesters" placeholder="Semesters" />
                    <label htmlFor="semesters" className="form-label">Semesters</label>
                </div>
                <div>
                    <button onClick={(e) => ValidateAndSave()} className="w-100 btn btn-lg btn-primary">Create</button>
                </div>
            </div>
            <div>
                <a href="./">Back to List</a>
            </div>
        </div>
        </>
    );
}