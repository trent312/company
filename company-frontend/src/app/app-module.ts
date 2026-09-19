import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
// Using ApiCompanyRepository (providedIn: 'root') via DI in services
import { CompanyListComponent } from './presentation/pages/company-list/company-list.component';

@NgModule({
  declarations: [
    App
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CompanyListComponent,
    HttpClientModule
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),

  ],
  bootstrap: [App]
})
export class AppModule { }
