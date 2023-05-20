import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, map, observable } from 'rxjs';
import { Question } from '../app/app.component';

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

  public searchQuestions(): Promise<Question[]>
  {
    const body = { "Ids": [1, 2, 3, 45, 67, 9, 11, 25, 44, 99, 101, 6, 74, 33] };
    return new Promise((resolve, reject) => {
      this._http_Client.post('http://localhost:5004/api/Question/Search', body)
        .subscribe(
          results => {
            const result = JSON.stringify(results);
            const obj:Question[] = JSON.parse(result);
            resolve(obj);
          },
          error => {
            console.error(error);
            reject(error);
          }
        );
    });
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
