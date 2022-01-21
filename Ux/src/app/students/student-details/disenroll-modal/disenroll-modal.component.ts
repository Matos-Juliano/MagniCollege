import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { StudentService } from 'src/app/Services/student.service';

@Component({
  selector: 'app-disenroll-modal',
  templateUrl: './disenroll-modal.component.html',
  styleUrls: ['./disenroll-modal.component.css']
})
export class DisenrollModalComponent implements OnInit {
  @Input() student:string
  @Input() course:string
  @Input() studentId:number
  success:boolean = false;

  @Output() disenrolled:EventEmitter<boolean> = new EventEmitter<boolean>()

  constructor(private service:StudentService) { }

  ngOnInit(): void {
  }

  disenroll(){
    this.service.Disenroll(this.studentId, null).subscribe(x=>{
      this.success = x;
      this.disenrolled.emit(x);
    })
  }

}
