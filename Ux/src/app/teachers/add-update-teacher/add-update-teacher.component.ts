import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TeachersService } from 'src/app/Services/teachers.service';
import { ITeacher } from 'src/app/shared/Models/Iteacher';

@Component({
  selector: 'app-add-update-teacher',
  templateUrl: './add-update-teacher.component.html',
  styleUrls: ['./add-update-teacher.component.css']
})
export class AddUpdateTeacherComponent implements OnInit {

  @Input() teacherId:number
  @Output() createdEvent:EventEmitter<number> = new EventEmitter<number>();
  teacher:ITeacher;
  success:boolean;
  constructor(private teacherService:TeachersService) { }

  ngOnInit(): void {
    if(this.teacherId){
      this.teacherService.GetTeacherById(this.teacherId).subscribe({
        next: response => {
          this.teacher = response;
        }
      })
    }else{
      this.teacher = {
        name: '',
        birthday : new Date(),
        salary : 0,
        id:null
      }
    }
  }

  sendTeacher(){
    if(this.teacherId){
      this.teacherService.UpdateTeacher(this.teacher).subscribe({
        next: response => {
          this.success = true;
          setTimeout(()=> {
            this.createdEvent.emit(0);
          }, 800)
        },
        error: err => {
          this.success = false;
        }
      })
    }else {
      this.teacherService.AddNewTeacher(this.teacher).subscribe({
        next: response => {
          this.success = true;
          setTimeout(()=>{
            this.createdEvent.emit(response.id);
          }, 800)
        },
        error: err => {
          this.success = false;
        }
      })
    }
  }

}
