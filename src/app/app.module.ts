import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { OrderByCodigoPipe } from './pipes/order-by-codigo.pipe';

import { AppComponent } from './app.component';
import { AuthInterceptor } from './interceptors/auth.interceptor'; 

@NgModule({
  declarations: [AppComponent,OrderByCodigoPipe ],
  imports: [BrowserModule, HttpClientModule,FormsModule],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true 
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
