import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { HomeComponent } from './home/home.component';
import { StudentDataTableComponent } from './students/student-data-table/student-data-table.component';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { StudentsComponent } from './students/students.component';
import { CoursesComponent } from './courses/courses.component';
import { TeachersComponent } from './teachers/teachers.component';
import { CoursesTableComponent } from './courses/courses-table/courses-table.component';
import { StudentDetailsComponent } from './students/student-details/student-details.component';
import { LoadingOverlayComponent } from './shared/loading-overlay/loading-overlay.component';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { LoadingInterceptor } from './Services/loading.interceptor';
import { GradesComponent } from './grades/grades.component';
import { GradesTableComponent } from './grades/grades-table/grades-table.component';
import { AddGradeModalComponent } from './grades/add-grade-modal/add-grade-modal.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DisenrollModalComponent } from './students/student-details/disenroll-modal/disenroll-modal.component';
import { EnrollComponent } from './students/student-details/enroll/enroll.component';
import { EditStudentComponent } from './students/student-details/edit-student/edit-student.component';
import { AddNewCourseComponent } from './courses/add-new-course/add-new-course.component';
import { CourseDetailsComponent } from './courses/course-details/course-details.component';
import { TeachersTableComponent } from './teachers/teachers-table/teachers-table.component';
import { SubjectsTableComponent } from './subjects/subjects-table/subjects-table.component';
import { SubjectsComponent } from './subjects/subjects.component';
import { SubjectDetailsComponent } from './subjects/subject-details/subject-details.component';
import { TeacherDetailsComponent } from './teachers/teacher-details/teacher-details.component';
import { AddUpdateTeacherComponent } from './teachers/add-update-teacher/add-update-teacher.component';
import { FireTeacherComponent } from './teachers/fire-teacher/fire-teacher.component';
import { AddSubjectComponent } from './courses/add-subject/add-subject.component';
import { UpdateTeacherComponent } from './subjects/update-teacher/update-teacher.component'
import { CreateSubjectComponent } from './subjects/add-subject/add-subject.component';
import { CourseDetailsV2Component } from './courses/course-details-v2/course-details-v2.component';
import { StudentDataTableV2Component } from './students/student-data-table/student-data-table-v2.component';
import { SubjectsTableV2Component } from './subjects/subjects-table/subjects-table-v2.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    StudentDataTableComponent,
    StudentsComponent,
    CoursesComponent,
    TeachersComponent,
    CoursesTableComponent,
    StudentDetailsComponent,
    LoadingOverlayComponent,
    GradesComponent,
    GradesTableComponent,
    AddGradeModalComponent,
    DisenrollModalComponent,
    EnrollComponent,
    EditStudentComponent,
    AddNewCourseComponent,
    CourseDetailsComponent,
    TeachersTableComponent,
    SubjectsTableComponent,
    SubjectsComponent,
    SubjectDetailsComponent,
    TeacherDetailsComponent,
    AddUpdateTeacherComponent,
    FireTeacherComponent,
    AddSubjectComponent,
    UpdateTeacherComponent,
    CreateSubjectComponent,
    CourseDetailsV2Component,
    StudentDataTableV2Component,
    SubjectsTableV2Component
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NoopAnimationsModule,
    MatToolbarModule,
    MatButtonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatFormFieldModule,
    MatSelectModule,
    HttpClientModule,
    MatProgressSpinnerModule,
    NgbModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi:true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
