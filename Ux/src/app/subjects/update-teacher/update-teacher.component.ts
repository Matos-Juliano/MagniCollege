import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SubjectsService } from 'src/app/Services/subjects.service';
import { TeachersService } from 'src/app/Services/teachers.service';
import { ITeacher } from 'src/app/shared/Models/Iteacher';

@Component({
  selector: 'app-update-teacher',
  templateUrl: './update-teacher.component.html',
  styleUrls: ['./update-teacher.component.css']
})
export class UpdateTeacherComponent implements OnInit {

  @Input() subjectId:number;
  @Output() changedEvent:EventEmitter<void> = new EventEmitter<void>();
  teachers:ITeacher[];
  teacherId:number;
  success:boolean;
  constructor(private modalService:NgbModal, private subjectService:SubjectsService, private teacherService:TeachersService) { }

  ngOnInit(): void {  
    this.teacherService.GetActiveTeachers().subscribe({
      next: response => {
        this.teachers = response;
      }
    })
  }

  updateTeacher(){
    this.subjectService.UpdateTeacherFromSubject(this.subjectId, this.teacherId).subscribe({
      next: response => {
        this.success = true;
        setTimeout(()=>{
          this.changedEvent.emit();
        }, 800)
      },
      error: err => {
        this.success =false;
      }
    })
  }

  openModal(content:any, size$:string){
    this.modalService.open(content, {size:size$});
  }
  closeModal(){
    this.modalService.dismissAll();
  }

}
