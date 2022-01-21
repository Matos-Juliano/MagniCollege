import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TeachersService } from 'src/app/Services/teachers.service';
import { ITeacher } from 'src/app/shared/Models/Iteacher';

@Component({
  selector: 'app-fire-teacher',
  templateUrl: './fire-teacher.component.html',
  styleUrls: ['./fire-teacher.component.css']
})
export class FireTeacherComponent implements OnInit {

  @Input() teacher:ITeacher;
  @Output() firedEvent:EventEmitter<void> = new EventEmitter<void>();

  success:boolean;
  constructor(private teacherService:TeachersService) { }

  ngOnInit(): void {
  }

  fireTeacher(){
    this.teacherService.DeActivateTeacher(this.teacher.id).subscribe({
      next: response => {
        this.success = true;
        this.firedEvent.emit();
      },
      error: err => {
        this.success = false;
      }
    })
  }

}
