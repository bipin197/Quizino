import { Component, Injectable, ViewEncapsulation } from '@angular/core';
import { GridOptions } from 'ag-grid-community';


@Injectable({
    providedIn: 'root'
  })

  @Component({
    selector: 'app-root',
    templateUrl: './dataGridService.component.html',
    styleUrls: ['./dataGridService.component.css']
  })
  
  

export class dataGridService{
    constructor() {}
    
    public getGridOptionsForQuestions(): GridOptions{
    return {
        columnDefs: [
        { headerName: 'Question', field: 'text', cellClass: 'scrollable-cell', flex: 1, editable:true, cellClassRules: { 'null-cell': 'typeof value !== "string" || value.length === 0', } },
        { headerName: 'Option A', field: 'optionA', editable:true, cellClassRules: { 'null-cell': 'typeof value !== "string" || value.length === 0', } },
        { headerName: 'Option B', field: 'optionB', editable:true, cellClassRules: { 'null-cell': 'typeof value !== "string" || value.length === 0', } },
        { headerName: 'Option C', field: 'optionC', editable:true, cellClassRules: { 'null-cell': 'typeof value !== "string" || value.length === 0', } },
        { headerName: 'Option D', field: 'optionD', editable:true, cellClassRules: { 'null-cell': 'typeof value !== "string" || value.length === 0', } },
        {
            headerName: 'Answer',
            editable:true,
            field: 'answer',
            cellEditorPopup:true,
            cellEditorPopupPosition: 'over',
            cellEditor: 'agSelectCellEditor',
            cellClassRules: { 'null-cell': 'typeof value !== "string" || value.length === 0', },
            cellEditorParams: {
            values: ["Option A", "Option B", "Option C", "Option D"],
            },
            valueGetter: (params)=> this.getReadableOption(params.data),
            valueSetter:this.setStatusValue
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
        rowSelection:'multiple',
    };
}

getReadableOption(params:any): string
{
  const answer = params.answer;
  // Logic to convert the answer value to a readable string
  let readableAnswer = '';
  switch (answer) {
    case 0:
      readableAnswer = 'Option A';
      break;
    case 1:
      readableAnswer = 'Option B';
      break;
    case 2:
      readableAnswer = 'Option C';
      break;
    case 3:
      readableAnswer = 'Option D';
      break;
    default:
      readableAnswer = '';
  }
  return readableAnswer;
}

setStatusValue(params: any): boolean {
        const readableAnswer = params.newValue;
        let numericAnswer = 0;
        switch (readableAnswer) {
        case 'Option A':
            numericAnswer = 0;
            break;
        case 'Option B':
            numericAnswer = 1;
            break;
        case 'Option C':
            numericAnswer = 2;
            break;
        case 'Option D':
            numericAnswer = 3;
            break;
        default:
            return false; // Return false if the readable string is invalid
        }
        params.data.answer = numericAnswer;
        return true;
    }
}