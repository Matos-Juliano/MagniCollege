import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { IGrade } from '../shared/Models/IGrade';
import { StudentService } from '../Services/student.service';
import { SubjectsService } from '../Services/subjects.service';
import { Observable, Subject, Subscription } from 'rxjs';

@Component({
  selector: 'app-grades',
  templateUrl: './grades.component.html',
  styleUrls: ['./grades.component.css']
})
export class GradesComponent implements OnInit, OnDestroy {
  
  @Input() subjectId:number;
  @Input() studentId:number;
  private eventSubscription: Subscription;

  @Input() events:Observable<void>;

  constructor(private studentService:StudentService, private subjectService:SubjectsService) { }

  showGrades:boolean = false;
  grades:IGrade[];
  eventsSubject: Subject<IGrade[]> = new Subject<IGrade[]>();

  ngOnInit(): void {
    this.getGrades();

    this.eventSubscription = this.events.subscribe(()=> {
      this.getGrades()
    })
  }

  getGrades(){
    if(this.studentId != null){
      this.studentService.GetGradesFromStudents(this.studentId).subscribe(response => {
        this.grades = response;
        this.showGrades = true;
        this.eventsSubject.next(this.grades);
      })
    }else{
      this.subjectService.GetGradeBySubject(this.subjectId).subscribe(response => {
        this.grades = response;
        this.showGrades = true;
        this.eventsSubject.next(this.grades);
      })
    }
  }

  ngOnDestroy(): void {
      this.eventSubscription.unsubscribe();
  }

}
