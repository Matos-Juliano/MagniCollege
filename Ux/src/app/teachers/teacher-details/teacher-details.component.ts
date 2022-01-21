import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TeachersService } from 'src/app/Services/teachers.service';
import { ITeacher } from 'src/app/shared/Models/Iteacher';

@Component({
  selector: 'app-teacher-details',
  templateUrl: './teacher-details.component.html',
  styleUrls: ['./teacher-details.component.css']
})
export class TeacherDetailsComponent implements OnInit {

  teacherId:number;
  teacher?:ITeacher;
  constructor(private route:ActivatedRoute, private service:TeachersService, private modalService:NgbModal, private router:Router) { }

  ngOnInit(): void {
    this.teacherId = Number(this.route.snapshot.paramMap.get('id'));

    this.service.GetTeacherById(this.teacherId).subscribe({
      next: response => {
        this.teacher = response;
      }
    })

  }

  openModal(content:any, size$:string){
    this.modalService.open(content, {size:size$});
  }

  closeModal(){
    this.modalService.dismissAll();
  }

  refreshTeacher(){
    this.closeModal();
    this.ngOnInit();
  }

}
