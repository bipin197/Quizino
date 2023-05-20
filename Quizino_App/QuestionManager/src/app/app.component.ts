import { Component, OnInit } from '@angular/core';
import { GridOptions, GridApi } from 'ag-grid-community';
import { DataService } from '../data/dataService.component';


export interface Question {
  questionId: number;
  text: string;
  optionA: string;
  optionB: string;
  optionC: string;
  optionD: string;
  answer: number;
  caterogry: number;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  title = 'Question Manager';
  gridOptions: GridOptions;
  rowData: Question[] = [];
  private gridApi!: GridApi;

  constructor(private dataService: DataService) {
    this.gridOptions = {
      columnDefs: [
        { headerName: 'Question', field: 'text' },
        { headerName: 'Option A', field: 'optionA' },
        { headerName: 'Option B', field: 'optionB' },
        { headerName: 'Option C', field: 'optionC' },
        { headerName: 'Option D', field: 'optionD' },
        { headerName: 'Answer', field: 'answer' },
        { headerName: 'Category', field: 'caterogry' }
      ],
      pagination: true,
      paginationPageSize: 15
    };
  }

  ngOnInit(): void {
    this.dataService.searchQuestions().then(result => {
      this.rowData = result;
    });
  }
}
