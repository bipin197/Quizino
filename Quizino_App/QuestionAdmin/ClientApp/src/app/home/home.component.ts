import { Component, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public questions: Question[];

  constructor(http: HttpClient) {
    http.get<Question[]>('https://localhost:44332/api/question/firstfive').subscribe(result => {
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
