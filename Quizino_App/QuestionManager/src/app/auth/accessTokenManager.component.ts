import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

export interface Token {
    access_token: string;
    scope: string;
    expires_in: number;
    token_type: string;
  }

  @Injectable({
    providedIn: 'root'
  })

  export class AccessTokenManagerService{
    _http_Client!: HttpClient;
    _token_url: string = 'https://dev-duimink2n4isdefw.us.auth0.com/oauth/token';
    _token!: string;
    tokenResponse!: Observable<any>;

    constructor(httpClient: HttpClient) {
        this._http_Client = httpClient;
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