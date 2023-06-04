import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { QuestionSearchResult } from '../models/modelService.component';

export interface Token {
  access_token: string;
  scope: string;
  expires_in: number;
  token_type: string;
}

@Injectable({
  providedIn: 'root'
})

export class DataService {
  _http_Client!: HttpClient;
  _token_url: string = 'https://dev-duimink2n4isdefw.us.auth0.com/oauth/token';
  _token!: Token;
  tokenResponse!: Observable<any>;

  constructor(httpClient: HttpClient) {
    this._http_Client = httpClient;
  }

  public searchQuestions(): Promise<QuestionSearchResult>
  {
    const body = { "Ids": [] };
    return new Promise((resolve, reject) => {
      this._http_Client.post('http://localhost:5004/api/Question/Search', body)
        .subscribe(
          function (results) {
            const result = JSON.stringify(results);
            const obj: QuestionSearchResult = JSON.parse(result);
            resolve(obj);
          },
          error => {
            console.error(error);
            reject(error);
          }
        );
    });
  }

  public SaveQuestions(body : string[]): void
  {
    this._http_Client.post('http://localhost:5004/api/Question/Update', JSON.stringify(body), {headers :{ 'Content-Type' : 'application/json'}})        .subscribe(
      function (results) {
        console.log(results);

      },
      error => {
        console.error(error);
      }
    );
  }

  setAccessToken() {
    this._http_Client.post(
      this._token_url,
      '[{"client_id": "7SnodGk5EgwbIzTmqqZd8u1H3STPqaY0", "client_secret": "eByGWFS6Xo4n9DuuWGvsrshtsv1uZqMzzVhhTT9LqfCABOuE4AwaHQAVSSr4CcDO","audience": "quizion-test-2", "grant_type": "client_credentials", "scope": "read:questions"}]',
      {
        headers: {
          'content-type': 'application/x-www-form-urlencoded', 'Access-Control-Allow-Origin': '*', 'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE', 'Access-Control-Allow-Headers': 'Content-Type, Access-Control-Allow-Methods'} })
      .subscribe(data => {
        let tokenObj: Token = JSON.parse(data.toString());
        console.log(tokenObj);
        this._token = tokenObj;
      });
  }
}
