import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { ISubject } from 'src/app/shared/Models/ISubject';
import { SubjectsService } from 'src/app/Services/subjects.service';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-add-grade-modal',
  templateUrl: './add-grade-modal.component.html',
  styleUrls: ['./add-grade-modal.component.css']
})
export class AddGradeModalComponent implements OnInit {

  @Input() student:number;

  subjects?:ISubject[];
  subjectId:number;

  gradeForm:FormGroup;
  value:number;
  success:boolean = false;

  @Output() closeModalEvent = new EventEmitter<void>();

  constructor(private service:SubjectsService) { }

  ngOnInit(): void {
    this.service.GetAllSubjects().subscribe(response=> {
      this.subjects = response;
    });
    this.createForm();
  }

  createForm(){
    this.gradeForm = new FormGroup({
      studentId: new FormControl(this.student),
      subjectId: new FormControl(''),
      value: new FormControl('')
    })
  }

  addGrade(){
    this.service.AddGrade(this.gradeForm.value['subjectId'], this.gradeForm.value['studentId'], this.gradeForm.value['value']).subscribe(response=> {
      if(response.id > 0){
        this.success = true;
      }
    })
  }

  closeModal(){
    this.closeModalEvent.emit();
  }

}
