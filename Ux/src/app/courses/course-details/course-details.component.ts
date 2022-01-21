import { Component, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CoursesService } from 'src/app/Services/courses.service';
import { SubjectsService } from 'src/app/Services/subjects.service';
import { ICourses } from 'src/app/shared/Models/ICourses';
import { SubjectsTableComponent } from 'src/app/subjects/subjects-table/subjects-table.component';

@Component({
  selector: 'app-course-details',
  templateUrl: './course-details.component.html',
  styleUrls: ['./course-details.component.css']
})
export class CourseDetailsComponent implements OnInit {
  showStudents:boolean = false;
  course:ICourses = null;
  courseId: number;
  teachersTotal:number;
  gradeAverage:number;
  studentCount:number;

  @ViewChild('subjectTable') subjectTable:SubjectsTableComponent;

  constructor(private route:ActivatedRoute, private courseService:CoursesService, private modalService:NgbModal) {
      this.courseId = Number(this.route.snapshot.paramMap.get('id'));
   }

  ngOnInit(): void {   

    this.courseService.GetCourseById(this.courseId).subscribe({
      next: response => {
        this.course = response;
        this.showStudents = true;
      }
    })

    this.courseService.GetStudentCount(this.courseId).subscribe({
      next: response => {
        this.studentCount = response;
      }
    })

    this.courseService.GetAverageGrade(this.courseId).subscribe({
      next: response => {
        this.gradeAverage = response;
      }
    })

    this.courseService.GetNumberOfTeachersInCourse(this.courseId).subscribe({
      next: response => {
        this.teachersTotal = response;
      }
    })

  }

  updateCourseInfo(){
    this.subjectTable.ngOnInit()
    this.closeModals();
  }

  closeModals(){
    this.modalService.dismissAll()
  }

  openModal(content:any, size$:string){
    this.modalService.open(content, {size:size$})
  }

  removeSubject(subId:number){
    this.courseService.RemoveSubjectFromCourse(this.courseId, subId).subscribe({
      next: response => {
        this.subjectTable.ngOnInit();
      }
    })
  }
  

}
