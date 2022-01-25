import { ICourses } from "./ICourses";
import { ISubject } from "./ISubject";
import { IStudent } from "./Student";

export interface ICoursesView {
    course : ICourses,
    students : IStudent[],
    subjects : ISubject[],
    averageGrade : number,
    teachersTotal : number
}