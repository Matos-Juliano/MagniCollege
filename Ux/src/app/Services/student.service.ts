import { IsFocusableConfig } from '@angular/cdk/a11y';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Icu } from '@angular/compiler/src/i18n/i18n_ast';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { ICourses } from '../shared/Models/ICourses';
import { IGrade } from '../shared/Models/IGrade';
import { IStudent } from '../shared/Models/Student';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  constructor(private http: HttpClient) { }

  getAllStudents():Observable<IStudent[]>{
    return this.http.get<IStudent[]>('http://localhost:5000/api/students/getall');
  }

  getStudentById(id:number):Observable<IStudent>{
    return this.http.get<IStudent>('http://localhost:5000/api/students/' + id);
  }

  GetGradesFromStudents(id:number):Observable<IGrade[]>{
    return this.http.get<IGrade[]>('http://localhost:5000/api/students/' + id + '/grades');
  }

  GetEnrolledCourse(id:number):Observable<ICourses>{
    return this.http.get<ICourses>('http://localhost:5000/api/students/' + id + '/course')
  }
  AddNewStudent(student:IStudent):Observable<IStudent>{
    return this.http.post<IStudent>('http://localhost:5000/api/students/add', student)
  }

  Update(id:number, student:IStudent):Observable<IStudent>{
    return this.http.patch<IStudent>('http://localhost:5000/api/students/' + id + '/edit', student)
  }

  Enroll(id:number, cId:number):Observable<boolean>{
    return this.http.post<boolean>('http://localhost:5000/api/students/' + id + '/enroll', {courseId:cId})
  }

  Disenroll(id:number, c:string):Observable<boolean>{
    return this.http.post<boolean>('http://localhost:5000/api/students/' + id + '/disenroll', {comment:c})
  }
}
