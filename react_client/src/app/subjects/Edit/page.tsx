"use client"
import { IModule } from "@/domain/IModule";
import { ISubject } from "@/domain/ISubject";
import CurriculumService from "@/services/CurriculumService";
import ModuleService from "@/services/ModuleService";
import SubjectService from "@/services/SubjectService";
import { useEffect, useState } from "react";

export default function Edit() {
    const [modules, setModules] = useState<IModule[]>();
    const [subject, setSubject] = useState<ISubject>();
    const [isLoading, setIsLoading] = useState(true);
    const subjectId = "typeof window !== 'undefined' ? localStorage.getItem(subjectId) : null"
    const userInfo = "typeof window !== 'undefined' ? localStorage.getItem('userInfo') : null"
    //const { userInfo, setUserInfo } = useContext(AppContext)!;

    const [validationError, setvalidationError] = useState("");

    const [label, setLabel] = useState("");
    const [description, setDescription] = useState("");
    const [eap, setEap] = useState(0);
    const [moduleId, setModuleId] = useState("");


    const ValidateAndSave = async () => {

        if (label == ""){
            setvalidationError("Label field is required!");
            return;
        }
        if (userInfo != undefined){
            const response = await SubjectService.put(userInfo, subjectId!, label, description, eap, moduleId)
            if (response.errors && response.errors.length > 0) {
                setvalidationError(response.errors[0]);
            } else {
                window.location.replace("/subjects");
            }
        } else{
            window.location.replace("/Identity/login");
        }
    };

    const loadData = async () => {
        const response = await SubjectService.get(subjectId!, userInfo!)
        if (response.data) {
            setSubject(response.data);
            setLabel(response.data.label)
            setDescription(response.data.description)
            setEap(response.data.eap)
            setModuleId(response.data.moduleId)
        } else {
            if (response.errors && response.errors.length > 0) {
                return (
                    <>
                        Server error: {response.errors[0]}
                    </>
                )
            }
        }
        const curricula = await CurriculumService.getAll();
        if (curricula.data){
            const response = await ModuleService.getAll(curricula.data[0].id)
            if (response.data) {
                setModules(response.data);
            }
        }
        setIsLoading(false);
    };
    useEffect(() => { loadData() }, []);

    if (isLoading){
        return (<h1>Details - LOADING</h1>);
        
    }
    return (
        <>
        <div className="row">
            <div className="col-md-5">
                <h2>Edit Subject</h2>
                <hr />
                <div className="text-danger" role="alert">{validationError}</div>
                <div className="form-floating mb-3">
                    <input
                        value={label}
                        onChange={(e) => { setLabel(e.target.value); setvalidationError(""); }}
                        id="label" type="text" className="form-control" autoComplete="label" placeholder="Label" />
                    <label htmlFor="label" className="form-label">Label</label>
                </div>
                <div className="form-floating mb-3">
                    <input
                        value={description}
                        onChange={(e) => { setDescription(e.target.value); setvalidationError(""); }}
                        id="description" type="text" className="form-control" autoComplete="description" placeholder="Võsa Pets" />
                    <label htmlFor="description" className="form-label">Description</label>
                </div>
                <div className="form-floating mb-3">
                    <input
                        value={eap}
                        onChange={(e) => { setEap(Number.parseInt(e.target.value)); setvalidationError(""); }}
                        id="eap" type="number" className="form-control" autoComplete="eap" placeholder="0" />
                    <label htmlFor="eap" className="form-label">EAP</label>
                </div>

                <div className="form-group">
                    <label className="form-label" htmlFor="moduleId">Module</label>
                    <select
                    value={moduleId}
                    onChange={(e) => { setModuleId(e.target.value) }}
                    className="form-control" id="moduleId">
                        <option></option>
                        {modules!.map((module) => 
                            <option value={module.id} key={module.id}>{module.label}</option>
                        )}
                    </select>
                </div>
                <div>
                    <button onClick={(e) => ValidateAndSave()} className="w-100 btn btn-lg btn-primary">Save</button>
                </div>
            </div>
            <div>
                <a href="./">Back to List</a>
            </div>
        </div>
        </>
    );
  }