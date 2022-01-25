import { AfterViewInit, Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { TeachersService } from 'src/app/Services/teachers.service';
import { CoursesService } from '../../Services/courses.service';
import { SubjectsService } from '../../Services/subjects.service';
import { ISubject } from '../../shared/Models/ISubject';
import { SubjectsTableDataSource } from './subjects-table-datasource';

@Component({
  selector: 'app-subjects-table-v2',
  templateUrl: './subjects-table.component.html',
  styleUrls: ['./subjects-table.component.css']
})
export class SubjectsTableV2Component implements AfterViewInit, OnInit, OnChanges {
  
  @Input() subjects:ISubject[];
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
        this.displayedColumns = ['id', 'name', 'teacher', 'view', 'remove']
        this.dataSource.data = this.subjects;
      }else if(this.teacherId){
        this.displayedColumns = ['id', 'name', 'view'];
        this.dataSource.data = this.subjects;
      }else{
        this.dataSource.data = this.subjects;
      }
  }

  ngOnChanges(changes: SimpleChanges): void {
      this.paginator._changePageSize(this.paginator.pageSize);
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
