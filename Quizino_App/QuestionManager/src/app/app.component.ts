import { Component, OnInit } from '@angular/core';
import { GridOptions, GridApi } from 'ag-grid-community';
import { DataService } from '../data/dataService.component';
import { Question } from '../models/modelService.component';
import { dataGridService } from './ui/dataGridService.component';

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

  constructor(private dataService: DataService, appDataGridService: dataGridService) {
    this.gridOptions = appDataGridService.getGridOptionsForQuestions();
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

  onCellValueChanged(params: any)
  {
    params.data.hasChanges = true;
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

      for(let i = 0 ; i < rowData.length ; i++)
      {
        rowData[i].data.isNew = false;
        rowData[i].data.hasChanges = false;
      }
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
