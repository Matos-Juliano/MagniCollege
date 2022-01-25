import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICourses } from '../shared/Models/ICourses';
import { ICoursesView } from '../shared/Models/ICoursesView';
import { ISubject } from '../shared/Models/ISubject';
import { IStudent } from '../shared/Models/Student';

@Injectable({
  providedIn: 'root'
})
export class CoursesService {

  constructor(private http:HttpClient) { }

  getAllCourses():Observable<ICourses[]>{
    return this.http.get<ICourses[]>('http://localhost:5000/api/courses/getall');
  }

  GetCourseById(id:number):Observable<ICourses>{
    return this.http.get<ICourses>('http://localhost:5000/api/courses/' + id);
  }

  GetStudentsInCourse(id:number):Observable<IStudent[]>{
    return this.http.get<IStudent[]>('http://localhost:5000/api/courses/' + id + '/students');
  }

  GetAverageGrade(id:number):Observable<number>{
    return this.http.get<number>('http://localhost:5000/api/courses/' + id + '/gradeAverage');
  }

  GetNumberOfTeachersInCourse(id:number):Observable<number>{
    return this.http.get<number>('http://localhost:5000/api/courses/' + id + '/teachercount');
  }

  GetSubjectsInCourse(id:number):Observable<ISubject[]>{
    return this.http.get<[]>('http://localhost:5000/api/courses/' + id + '/subjects');
  }

  AddNewCourse(courseName:string):Observable<ICourses>{
    return this.http.post<ICourses>('http://localhost:5000/api/courses/add', {name:courseName});
  }

  AddSubjectToCourse(courseId:number, subjectid:number):Observable<boolean>{
    return this.http.post<boolean>('http://localhost:5000/api/courses/' + courseId + '/addsubject', {subjectId:subjectid});
  }

  GetStudentCount(courseId:number):Observable<number>{
    return this.http.get<number>('http://localhost:5000/api/courses/' + courseId + '/studentcount');
  }

  RemoveSubjectFromCourse(courseId:number, subjectid:number):Observable<boolean>{
    return this.http.delete<boolean>('http://localhost:5000/api/courses/' + courseId + '/subject', {body: {subjectId:subjectid}})
  }

  GetCourseViewFromV2(courseId:number):Observable<ICoursesView>{
    return this.http.get<ICoursesView>('http://localhost:5000/v2/api/courses/' + courseId);
  }

}
