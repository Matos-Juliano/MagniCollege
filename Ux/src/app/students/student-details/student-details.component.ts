import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ICourses } from 'src/app/shared/Models/ICourses';
import { IStudent } from 'src/app/shared/Models/Student';
import { StudentService } from 'src/app/Services/student.service';
import { isEmpty, Subject } from 'rxjs';

@Component({
  selector: 'app-student-details',
  templateUrl: './student-details.component.html',
  styleUrls: ['./student-details.component.css']
})
export class StudentDetailsComponent implements OnInit {
  student?:IStudent;
  course?:ICourses;
  eventsSubject: Subject<void> = new Subject<void>();
  studentId:number;

  constructor(private service:StudentService, private route:ActivatedRoute, private modalService:NgbModal) { }

  ngOnInit(): void {
    this.studentId = Number(this.route.snapshot.paramMap.get('id'));
    this.getStudent();
    this.getCourse();
  }

  getStudent(){
    this.service.getStudentById(this.studentId).subscribe({
      next: response=>{
        this.student = response;
      }
    })  
  }

  getCourse(){
    this.service.GetEnrolledCourse(this.studentId).subscribe({
      next: response => {
        this.course = response;
      },
      error: err=> {
        this.course = null;
      }
    })
  }

  openModal(content:any, size$:string){
    this.modalService.open(content, {ariaLabelledBy:'modal-basic-title', size: size$});
  }

  updateGrades(){
    this.eventsSubject.next();
    this.modalService.dismissAll();
  }

  updateEnrollment(){
    this.getCourse();
  }

  updateStudentInfo(){
    this.getStudent();
  }

  closeModals(){
    this.modalService.dismissAll();
  }

}
