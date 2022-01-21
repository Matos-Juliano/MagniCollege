import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-teachers',
  templateUrl: './teachers.component.html',
  styleUrls: ['./teachers.component.css']
})
export class TeachersComponent implements OnInit {

  constructor(private modalService:NgbModal, private router:Router, private route:ActivatedRoute) { }

  ngOnInit(): void {
  }

  OpenModal(content:any, size$:string){
    this.modalService.open(content, {size:size$})
  }

  closeModals(){
    this.modalService.dismissAll();
  }

  goToTeacher(id:number){
    this.modalService.dismissAll();
    this.router.navigate([id], {relativeTo:this.route})
  }

}
