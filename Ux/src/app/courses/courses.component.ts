import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css']
})
export class CoursesComponent implements OnInit {


  
  constructor(private modal:NgbModal, private router:Router, private route:ActivatedRoute) { }

  ngOnInit(): void {
  }

  openModal(content:any, size$:string){
    this.modal.open(content, {ariaLabelledBy:'modal-basic-title', size: size$});
  }

  closeModals(){
    this.modal.dismissAll();
  }

}
