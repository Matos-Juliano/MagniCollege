import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CoursesService } from 'src/app/Services/courses.service';
import { ICoursesView } from 'src/app/shared/Models/ICoursesView';
import { SubjectsTableV2Component } from 'src/app/subjects/subjects-table/subjects-table-v2.component';

@Component({
  selector: 'app-course-details-v2',
  templateUrl: './course-details-v2.component.html',
  styleUrls: ['./course-details-v2.component.css']
})
export class CourseDetailsV2Component implements OnInit {
  courseId : number;
  coursesView : ICoursesView;
  showStudents:boolean;
  @ViewChild('subjectTable') subjectTable:SubjectsTableV2Component;

  constructor(private coursesService: CoursesService, private route:ActivatedRoute, private modalService:NgbModal) { }

  ngOnInit(): void {
    this.courseId = Number(this.route.snapshot.paramMap.get('id'));
    
    this.getCourseView();
  }

  getCourseView(){
    this.coursesService.GetCourseViewFromV2(this.courseId).subscribe({
      next : response => {
        this.coursesView = response;
        this.showStudents = true;
      },
      error : err => {
        console.log(err);
      }
    })
  }

  openModal(content:any, size$:string){
    this.modalService.open(content, {size:size$})
  }

  closeModals(){
    this.modalService.dismissAll();
  }

  updateCourseInfo(){
    this.closeModals();
    this.ngOnInit();
  }

  removeSubject(subId:number){
    this.coursesService.RemoveSubjectFromCourse(this.courseId, subId).subscribe({
      next: response => {
        this.coursesService.GetSubjectsInCourse(this.courseId).subscribe({
          next: response => {
            this.coursesView.subjects = response;
          }
        })
        
      }
    })
  }

}
