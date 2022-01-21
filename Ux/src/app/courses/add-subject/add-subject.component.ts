import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CoursesService } from 'src/app/Services/courses.service';
import { SubjectsService } from 'src/app/Services/subjects.service';
import { ISubject } from 'src/app/shared/Models/ISubject';

@Component({
  selector: 'app-add-subject',
  templateUrl: './add-subject.component.html',
  styleUrls: ['./add-subject.component.css']
})
export class AddSubjectComponent implements OnInit {
  @Input() courseId:number;
  @Output() addedEvent:EventEmitter<void> = new EventEmitter<void>();

  subjectForm:FormGroup;
  subjectId:number;
  success:boolean;
  subjects: ISubject[];  

  constructor(private service:SubjectsService, private courseService:CoursesService) { }

  ngOnInit(): void {
    this.service.GetAllSubjects().subscribe(response=> {
      this.subjects = response;
    });
  }


  addSubject(){
    console.log(this.courseId + ' and ' + this.subjectId)
    this.courseService.AddSubjectToCourse(this.courseId, this.subjectId).subscribe({
      next: response => {
        this.success = true;
        setTimeout(()=> {
          this.addedEvent.emit();
        }, 800)
      },
      error: err => {
        this.success = false;
      }
    })
  }

}
