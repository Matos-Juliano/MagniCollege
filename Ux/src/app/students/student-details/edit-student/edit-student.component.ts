import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { StudentService } from 'src/app/Services/student.service';
import { IStudent } from 'src/app/shared/Models/Student';

@Component({
  selector: 'app-edit-student',
  templateUrl: './edit-student.component.html',
  styleUrls: ['./edit-student.component.css']
})
export class EditStudentComponent implements OnInit {

  success:boolean;
  @Input() student:IStudent;
  @Input() isEdit:boolean;
  @Output() updateEvent:EventEmitter<void> = new EventEmitter<void>();
  @Output() createdEvent:EventEmitter<number> = new EventEmitter<number>();

  constructor(private service:StudentService, private modalService:NgbModal) { }

  ngOnInit(): void {
    if(!this.isEdit){
      this.student = {
        id: null,
        name : '',
        birthday : new Date()
      }
    }
  }

  editStudent(){
    if(this.isEdit){
      this.service.Update(this.student.id, this.student).subscribe({
        next: response => {
          this.success = true;
          setTimeout(()=> {
            this.updateEvent.emit();
            this.closeModals();
          }, 800)          
        },
        error: err => {
          this.success = false;
        }
      })
    }else{
      this.service.AddNewStudent(this.student).subscribe({
        next: response => {
          this.success = true;
          setTimeout(()=> {
            this.createdEvent.emit(response.id);
            this.closeModals();
          }, 800) 
        },
        error: err => {
          this.success = false;
        }
      })
    }
    
  }

  closeModals(){
    this.modalService.dismissAll();
  }

}
