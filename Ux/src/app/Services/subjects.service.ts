import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IGrade } from '../shared/Models/IGrade';
import { ISubject } from '../shared/Models/ISubject';
import { ITeacher } from '../shared/Models/Iteacher';

@Injectable({
  providedIn: 'root'
})
export class SubjectsService {

  constructor(private http:HttpClient) { }

  GetAllSubjects():Observable<ISubject[]>{
    return this.http.get<ISubject[]>('http://localhost:5000/api/subjects/getall');
  }

  GetByID(id:number):Observable<ISubject>{
    return this.http.get<ISubject>('http://localhost:5000/api/subjects/' + id);
  }

  GetAllTeachersInSubject(id:number):Observable<ITeacher[]>{
    return this.http.get<ITeacher[]>('http://localhost:5000/api/subjects/' + id + '/teacher');
  }

  GetGradeByStudentAndSubject(id:number, stuId:number):Observable<ISubject[]>{
    return this.http.get<ISubject[]>('http://localhost:5000/api/subjects/' + id + '/grade?studentId=' + stuId);
  }

  GetGradeBySubject(id:number):Observable<IGrade[]>{
    return this.http.get<IGrade[]>('http://localhost:5000/api/subjects/'+ id + '/grades');
  }

  AddNewSubject(name$:string, teacherId$:number):Observable<ISubject>{
    return this.http.post<ISubject>('http://localhost:5000/api/subjects/add', {name:name$, teacherId:teacherId$});
  }

  AddGrade(subject:number, student:number, gradeValue:number):Observable<ISubject>{
    const grade:any = {subject:null, value:gradeValue, studentId:student}
    return this.http.post<ISubject>('http://localhost:5000/api/subjects/' + subject + '/grade', grade);
  }

  UpdateTeacherFromSubject(subject:number, teacher:number):Observable<boolean>{
    return this.http.post<boolean>('http://localhost:5000/api/subjects/' + subject + '/teacher', {teacherId:teacher});
  }
}
