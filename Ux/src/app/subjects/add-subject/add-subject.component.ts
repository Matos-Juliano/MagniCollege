import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SubjectsService } from 'src/app/Services/subjects.service';
import { TeachersService } from 'src/app/Services/teachers.service';
import { ISubject } from 'src/app/shared/Models/ISubject';
import { ITeacher } from 'src/app/shared/Models/Iteacher';

@Component({
  selector: 'app-create-subject',
  templateUrl: './add-subject.component.html',
  styleUrls: ['./add-subject.component.css']
})
export class CreateSubjectComponent implements OnInit {
  subject:ISubject = {
    id: null,
    name : '',
    teacherId : null
  }
  teacherId:number;
  teachers:ITeacher[];
  success:boolean;
  constructor(private subjectService:SubjectsService, private router:Router, private route:ActivatedRoute, private teacherService:TeachersService, private modalService:NgbModal) { }

  ngOnInit(): void {
    this.getTeachers();
  }

  getTeachers(){
    this.teacherService.GetActiveTeachers().subscribe({
      next: response => {
        this.teachers = response;
      }
    })
  }

  sendSubject(){
    this.subjectService.AddNewSubject(this.subject.name, this.teacherId).subscribe({
      next: response => {
        this.success = true;
        setTimeout(()=> {
          this.modalService.dismissAll();
          this.router.navigate([response.id], {relativeTo:this.route})
        }, 800)
      }
    })
  }

}
