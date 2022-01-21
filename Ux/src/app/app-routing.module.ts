import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudentsComponent } from './students/students.component';
import { CoursesComponent } from './courses/courses.component';
import { TeachersComponent } from './teachers/teachers.component';
import { HomeComponent } from './home/home.component';
import { StudentDetailsComponent } from './students/student-details/student-details.component';
import { CourseDetailsComponent } from './courses/course-details/course-details.component';
import { SubjectsComponent } from './subjects/subjects.component';
import { SubjectDetailsComponent } from './subjects/subject-details/subject-details.component';
import { TeacherDetailsComponent } from './teachers/teacher-details/teacher-details.component';

const routes: Routes = [
  {path: "", component: HomeComponent},  
  {path: "students", component: StudentsComponent},
  {path: "courses", component: CoursesComponent},
  {path: "teachers", component: TeachersComponent},
  {path: "teachers/:id", component: TeacherDetailsComponent},
  {path: "students/:id", component: StudentDetailsComponent},
  {path: "courses/:id", component:CourseDetailsComponent},
  {path: "subjects", component:SubjectsComponent},
  {path: "subjects/:id", component:SubjectDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
