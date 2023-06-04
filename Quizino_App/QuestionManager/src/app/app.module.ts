import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AgGridModule } from 'ag-grid-angular';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AuthModule } from '@auth0/auth0-angular';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AgGridModule,
    HttpClientModule,
    AuthModule.forRoot({
      domain: 'dev-duimink2n4isdefw.us.auth0.com',
      clientId: 'L1fBvOx21IJdplZXkvTEXIswKKc3fLBC',
      authorizationParams: {
        redirect_uri: window.location.origin
      }
    })
  ],
  exports: [
    AgGridModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
export class SharedModule { }
