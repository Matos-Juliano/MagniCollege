import { AfterViewInit, Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { IStudent } from 'src/app/shared/Models/Student';
import { StudentService } from 'src/app/Services/student.service';
import { StudentDataTableDataSource} from './student-data-table-datasource';
import { CoursesService } from 'src/app/Services/courses.service';

@Component({
  selector: 'app-student-data-table',
  templateUrl: './student-data-table.component.html',
  styleUrls: ['./student-data-table.component.css']
})
export class StudentDataTableComponent implements AfterViewInit, OnInit {
  @Input() courseId:number;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<IStudent>;
  dataSource: StudentDataTableDataSource;

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['id', 'name', 'birthday', "edit"];

  constructor(private service: StudentService, private courseService:CoursesService) {
    this.dataSource = new StudentDataTableDataSource();     
  }

  ngOnInit(): void {
    if(this.courseId){
      this.getStudentsByCourse()
    }else{
      this.getStudents();
    }  
  }

  getStudentsByCourse(){
    this.courseService.GetStudentsInCourse(this.courseId).subscribe({
      next: response => {
        this.dataSource.data = response;
        this.paginator._changePageSize(10);
      }
    })
  }

  getStudents(){
    this.service.getAllStudents().subscribe(response => {
      this.dataSource.data = response;
      this.paginator._changePageSize(10);
    });
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.table.dataSource = this.dataSource;
  }
}
