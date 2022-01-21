import { AfterViewInit, Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { Observable } from 'rxjs';
import { IGrade } from 'src/app/shared/Models/IGrade';
import { GradesTableDataSource } from './grades-table-datasource';

@Component({
  selector: 'app-grades-table',
  templateUrl: './grades-table.component.html',
  styleUrls: ['./grades-table.component.css']
})
export class GradesTableComponent implements OnInit, AfterViewInit {
  @Input() isIndividual:boolean;
  @Input() grades:IGrade[];
  @Input() event:Observable<IGrade[]>

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<IGrade>;
  dataSource: GradesTableDataSource;

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['student', 'value'];

  constructor() {
    this.dataSource = new GradesTableDataSource();
  }

  ngOnInit(): void {
    if(this.isIndividual){
      this.displayedColumns = ['subject', 'value']
    }
    this.dataSource.data = this.grades;
    this.event.subscribe(x=>{
      this.dataSource.data = x;
      this.paginator._changePageSize(10);
    })
  }
  
  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.table.dataSource = this.dataSource;    
  }
}
