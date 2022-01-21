import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { CoursesService } from 'src/app/Services/courses.service';
import { StudentService } from 'src/app/Services/student.service';
import { ICourses } from 'src/app/shared/Models/ICourses';

@Component({
  selector: 'app-enroll',
  templateUrl: './enroll.component.html',
  styleUrls: ['./enroll.component.css']
})
export class EnrollComponent implements OnInit {

  @Input() studentId:number;
  @Input() student:string;
  @Output() enrolled:EventEmitter<void> = new EventEmitter<void>()

  enrollForm:FormGroup;
  courses:ICourses[];
  success:boolean;
  constructor(private courseService:CoursesService, private studentService:StudentService) { }

  ngOnInit(): void {
    this.courseService.getAllCourses().subscribe(response=>{
      this.courses = response;
    })

    this.createForm();
  }

  enrollStudent(){
    this.studentService.Enroll(this.studentId, this.enrollForm.value['courseId']).subscribe({
      next: response=>{
        this.success = response;
        this.enrolled.emit();
      },
      error: err => {
        this.success = false;
      }
    })
  }

  createForm(){
    this.enrollForm = new FormGroup({
      studentId: new FormControl(this.studentId),
      courseId: new FormControl()
    })
  }

}
