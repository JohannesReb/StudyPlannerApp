export interface ICurriculum {
    id: string,
    code: string,
    label: string,
    manager: string,
    from: Date,
    until: Date,
    semesters: number
} 