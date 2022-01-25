import { AfterViewInit, Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { IStudent } from 'src/app/shared/Models/Student';
import { StudentService } from 'src/app/Services/student.service';
import { StudentDataTableDataSource} from './student-data-table-datasource';
import { CoursesService } from 'src/app/Services/courses.service';

@Component({
  selector: 'app-student-data-table-v2',
  templateUrl: './student-data-table.component.html',
  styleUrls: ['./student-data-table.component.css']
})
export class StudentDataTableV2Component implements AfterViewInit, OnInit {
  @Input() students:IStudent[];
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<IStudent>;
  dataSource: StudentDataTableDataSource;

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['id', 'name', 'birthday', 'edit'];

  constructor() {
    this.dataSource = new StudentDataTableDataSource();     
  }

  ngOnInit(): void {
    this.dataSource.data = this.students;
    this.paginator._changePageSize(10);
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.table.dataSource = this.dataSource;
  }
}
