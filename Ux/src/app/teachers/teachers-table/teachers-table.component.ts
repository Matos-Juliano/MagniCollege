import { AfterViewInit, Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { TeachersService } from 'src/app/Services/teachers.service';
import { ITeacher } from 'src/app/shared/Models/Iteacher';
import { TeachersTableDataSource } from './teachers-table-datasource';

@Component({
  selector: 'app-teachers-table',
  templateUrl: './teachers-table.component.html',
  styleUrls: ['./teachers-table.component.css']
})
export class TeachersTableComponent implements AfterViewInit, OnInit {
  @Input() courseId:number;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<ITeacher>;
  dataSource: TeachersTableDataSource;

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['id', 'name', 'birthday', 'view'];

  constructor(private service:TeachersService) {
    this.dataSource = new TeachersTableDataSource();
  }

  ngOnInit(): void {
    if(!this.courseId){
      this.getAllTeachers();
    }  
  }

  getAllTeachers(){
    this.service.GetActiveTeachers().subscribe({
      next: response => {
        this.dataSource.data = response;
        this.paginator._changePageSize(10);
      }
    })
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.table.dataSource = this.dataSource;
  }
}
