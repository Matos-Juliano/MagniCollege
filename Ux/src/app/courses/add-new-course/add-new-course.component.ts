import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CoursesService } from 'src/app/Services/courses.service';
import { TeachersService } from 'src/app/Services/teachers.service';
import { ICourses } from 'src/app/shared/Models/ICourses';
import { ITeacher } from 'src/app/shared/Models/Iteacher';

@Component({
  selector: 'app-add-new-course',
  templateUrl: './add-new-course.component.html',
  styleUrls: ['./add-new-course.component.css']
})
export class AddNewCourseComponent implements OnInit {
  success:boolean;

  @Output() createdEvent:EventEmitter<number> = new EventEmitter<number>();

  name:string;

  constructor(private service:CoursesService, private router:Router, private route:ActivatedRoute, private modalService:NgbModal) { }

  ngOnInit(): void {  }

  createCourse(){
    this.service.AddNewCourse(this.name).subscribe({
      next: response => {
        this.modalService.dismissAll();
        this.router.navigate([response.id], {relativeTo:this.route});
      },
      error: err => {
        this.success = false;
      }
    })
  }

}
