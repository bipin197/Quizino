import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

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
  _token!: string;
  tokenResponse!: Observable<any>;

  constructor(httpClient: HttpClient) {
    this._http_Client = httpClient;
  }

  public async searchQuestions(): Promise<QuestionSearchResult>
  {
    const body = { "Ids": [] };
    let token:string = await this.getReadAccessToken();
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    });


    return new Promise((resolve, reject) => {
      this._http_Client.post('http://localhost:5001/api/Question/Search', body, {headers})
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

  public async SaveQuestions(body : string[]): Promise<void>
  {
    let token:string = await this.getWriteAccessToken();
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    });
    return new Promise((resolve, reject) => {
    this._http_Client.post('http://localhost:5001/api/Question/Update', JSON.stringify(body), {headers})        .subscribe(
      function (results) {
        console.log(results);
        resolve();
      },
      error => {
        console.error(error);
        reject();
      }
    )});
  }

  async getReadAccessToken() : Promise<string> {

    let token:string = "";
    return new Promise((resolve, reject) => {
    this._http_Client.post(
      this._token_url,
      '{"client_id": "7SnodGk5EgwbIzTmqqZd8u1H3STPqaY0", "client_secret": "eByGWFS6Xo4n9DuuWGvsrshtsv1uZqMzzVhhTT9LqfCABOuE4AwaHQAVSSr4CcDO","audience": "quizion-test-2", "grant_type": "client_credentials", "scope": "read:questions"}',
      {
        headers: {
          'content-type': 'application/json'} })
      .subscribe(data => {
        let tokenObj: Token = JSON.parse(JSON.stringify(data));
        token = tokenObj.access_token;
        resolve(token);
      },
      error => {
        console.error(error);
        reject(error);
      })});
  };

  async getWriteAccessToken() : Promise<string> {

    let token:string = "";
    return new Promise((resolve, reject) => {
    this._http_Client.post(
      this._token_url,
      '{"client_id": "7SnodGk5EgwbIzTmqqZd8u1H3STPqaY0", "client_secret": "eByGWFS6Xo4n9DuuWGvsrshtsv1uZqMzzVhhTT9LqfCABOuE4AwaHQAVSSr4CcDO","audience": "quizion-test-2", "grant_type": "client_credentials", "scope": "write:questions"}',
      {
        headers: {
          'content-type': 'application/json'} })
      .subscribe(data => {
        let tokenObj: Token = JSON.parse(JSON.stringify(data));
        token = tokenObj.access_token;
        resolve(token);
      },
      error => {
        console.error(error);
        reject(error);
      })});
  };
}
