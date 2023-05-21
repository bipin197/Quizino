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
  isNew: boolean;
}

export interface QuestionSearchResult {
  questions: Question[];
  TotalQuestions: number;
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
        { headerName: 'Question', field: 'text', cellClass: 'scrollable-cell', flex: 1, editable:true },
        { headerName: 'Option A', field: 'optionA', editable:true },
        { headerName: 'Option B', field: 'optionB', editable:true },
        { headerName: 'Option C', field: 'optionC', editable:true },
        { headerName: 'Option D', field: 'optionD', editable:true },
        {
          headerName: 'Answer',
          editable:true,
          field: 'answer',
          cellEditorPopup:true,
          cellEditorPopupPosition: 'over',
          cellEditor: 'agSelectCellEditor',
          cellEditorParams: {
            values: [0, 1, 2, 3],
          },
        },
        {
          headerName: 'Category',
          field: 'caterogry',
          editable: false,
          cellEditorPopup: true,
          cellEditorPopupPosition: 'over',
          cellEditor: 'agSelectCellEditor',
          cellEditorParams: {
            values: [0, 1, 2, 3],
          },
        } 
      ],
      pagination: true,
      paginationPageSize: 15,
      rowSelection:'multiple'
    };
  }

  ngOnInit(): void {
    this.dataService.searchQuestions().then(result => {
      this.rowData = result.questions;
      console.log('data is ready!');
    });
  }

  onGridReady(params: any) {
    this.gridApi = params.api;
    console.log('Grid is ready!');
  }

  saveGridData(): void {
    if (this.gridApi) {
      const rowData = this.gridApi?.getSelectedNodes();
      // Perform save operation using rowData
      const newArray: string[] = [];
      for(let i = 0 ; i < rowData.length ; i++)
      {
        newArray.push(rowData[i].data)
        console.log('Grid data saved:', rowData[i].data);
      }
      
      console.log('Grid data update:', newArray);
      this.dataService.SaveQuestions(newArray)
    }
  }

  addNewRow(): void {
    const question: Question = {} as Question;
    if (question) {
      question.isNew = true;
  
      const transaction = {
        add: [question], // Add the new row to the transaction
        addIndex: 0 // Insert the new row at the beginning of the grid
      };
  
      this.gridApi?.applyTransaction(transaction);
    }
  }
} 
