import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { TeachersService } from 'src/app/Services/teachers.service';
import { CoursesService } from '../../Services/courses.service';
import { SubjectsService } from '../../Services/subjects.service';
import { ISubject } from '../../shared/Models/ISubject';
import { SubjectsTableDataSource } from './subjects-table-datasource';

@Component({
  selector: 'app-subjects-table',
  templateUrl: './subjects-table.component.html',
  styleUrls: ['./subjects-table.component.css']
})
export class SubjectsTableComponent implements AfterViewInit, OnInit {
  
  @Input() courseId:number;
  @Input() teacherId:number;
  @Output() removeEvent:EventEmitter<number> = new EventEmitter<number>()

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<ISubject>;
  dataSource: SubjectsTableDataSource;

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['id', 'name', 'teacher', 'view'];

  constructor(private coursesService:CoursesService, private subjectService:SubjectsService, private teacherService:TeachersService) {
    this.dataSource = new SubjectsTableDataSource();
  }

  ngOnInit(): void {
      if(this.courseId){
        this.coursesService.GetSubjectsInCourse(this.courseId).subscribe({
          next: response => {
            this.displayedColumns = ['id', 'name', 'teacher', 'view', 'remove']
            this.dataSource.data = response;
            this.paginator._changePageSize(10);
          }
        })
      }else if(this.teacherId){
        this.teacherService.GetTeachingSubjects(this.teacherId).subscribe({
          next: response => {
            this.displayedColumns = ['id', 'name', 'view'];
            this.dataSource.data = response;
            this.paginator._changePageSize(10);
          }
        })
      }else{
        this.subjectService.GetAllSubjects().subscribe({
          next: response => {
            console.log(response)
            this.dataSource.data = response;
            this.paginator._changePageSize(10);
          }
        })
      }
  }

  remove(id:number){
    this.removeEvent.emit(id);
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.table.dataSource = this.dataSource;
  }
}
