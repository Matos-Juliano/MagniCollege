import { Component, OnInit } from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css']
})
export class StudentsComponent implements OnInit {

  constructor(private modalService:NgbModal, private router:Router, private route:ActivatedRoute) { }

  ngOnInit(): void {
  }

  openModal(content:any, size$:string){
    this.modalService.open(content, {ariaLabelledBy:'modal-basic-title', size: size$});
  }

  updateStudentInfo(id:number){
    this.router.navigate([id], {relativeTo: this.route})
  }

  closeModals(){
    this.modalService.dismissAll();
  }

}
