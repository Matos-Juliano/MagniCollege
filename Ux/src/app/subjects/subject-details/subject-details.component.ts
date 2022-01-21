import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subject } from 'rxjs';
import { SubjectsService } from 'src/app/Services/subjects.service';
import { IGrade } from 'src/app/shared/Models/IGrade';
import { ISubject } from 'src/app/shared/Models/ISubject';

@Component({
  selector: 'app-subject-details',
  templateUrl: './subject-details.component.html',
  styleUrls: ['./subject-details.component.css']
})
export class SubjectDetailsComponent implements OnInit {
  eventsSubject: Subject<void> = new Subject<void>();
  subjectId:number;
  subject:any;
  grades:IGrade[];
  constructor(private service:SubjectsService, private route:ActivatedRoute, private modalService:NgbModal) { }

  ngOnInit(): void {
    this.subjectId = Number(this.route.snapshot.paramMap.get('id'));
    this.service.GetByID(this.subjectId).subscribe({
      next: response => {        
        this.subject = response;
      }
    })
    this.service.GetGradeBySubject(this.subjectId).subscribe({
      next: response => {
        this.grades = response;
      }
    })
  }

  openModal(content:any, size$:string){
    this.modalService.open(content, {size:size$});
  }
  closeModal(){
    this.modalService.dismissAll();
  }

  teacherChanged(){
    this.closeModal();
    this.ngOnInit();
  }
}
