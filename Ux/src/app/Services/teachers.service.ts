import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ISubject } from '../shared/Models/ISubject';
import { ITeacher } from '../shared/Models/Iteacher';

@Injectable({
  providedIn: 'root'
})
export class TeachersService {

  constructor(private http:HttpClient) { }

  GetAllTeachers():Observable<ITeacher[]>{
    return this.http.get<ITeacher[]>('http://localhost:5000/api/teachers/getall');
  }

  GetTeacherById(id:number):Observable<ITeacher>{
    return this.http.get<ITeacher>('http://localhost:5000/api/teachers/' + id);
  }

  GetTeachingSubjects(id:number):Observable<ISubject[]>{
    return this.http.get<ISubject[]>('http://localhost:5000/api/teachers/' + id + '/subjects');
  }

  GetActiveTeachers():Observable<ITeacher[]>{
    return this.http.get<ITeacher[]>('http://localhost:5000/api/teachers/getactive');
  }

  AddNewTeacher(subject:ITeacher):Observable<ITeacher>{
    return this.http.post<ITeacher>('http://localhost:5000/api/teachers/add', subject);
  }

  UpdateTeacher(subject:ITeacher):Observable<ITeacher>{
    return this.http.patch<ITeacher>('http://localhost:5000/api/teachers/' + subject.id + '/update', subject);
  }

  ActivateTeacher(id:number):Observable<ITeacher>{
    return this.http.patch<ITeacher>('http://localhost:5000/api/teachers/' + id + '/activate', {});
  }

  DeActivateTeacher(id:number):Observable<ITeacher>{
    return this.http.patch<ITeacher>('http://localhost:5000/api/teachers/' + id + '/deactivate', {});
  }
}
