import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { SearchflightsComponent } from './searchflights/searchflights.component';
import { BookFlightComponent } from './book-flight/book-flight.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    SearchflightsComponent,
    BookFlightComponent

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: SearchflightsComponent, pathMatch: 'full' },
      { path: 'search-flights', component: SearchflightsComponent },
      { path: 'book-flight/:flightId', component: BookFlightComponent, pathMatch: 'full' },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
