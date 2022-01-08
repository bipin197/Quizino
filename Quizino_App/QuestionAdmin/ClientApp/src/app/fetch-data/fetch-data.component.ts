import { Component, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public questions: Question[];

  constructor(http: HttpClient) {
    let headers = new HttpHeaders();
    headers.set('Access-Control-Allow-Origin', '*');
    http.get<Question[]>('https://localhost:44332/api/question/firstfive', { headers: headers}).subscribe(result => {
      this.questions = result;
    }, error => console.error(error));
  }
}

interface Question {
  text: string;
  optionA: number;
  optionB: number;
  optionC: string;
  optionD: string;
  answer: string;
}
