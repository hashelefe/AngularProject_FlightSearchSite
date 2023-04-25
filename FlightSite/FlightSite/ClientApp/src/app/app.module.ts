import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { SearchflightsComponent } from './searchflights/searchflights.component';
import { BookFlightComponent } from './book-flight/book-flight.component';
import { RegisterPassengerComponent } from './register-passenger/register-passenger.component';
import { MyBookingsComponent } from './my-bookings/my-bookings.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    SearchflightsComponent,
    BookFlightComponent,
    RegisterPassengerComponent,
    MyBookingsComponent

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: SearchflightsComponent, pathMatch: 'full' },
      { path: 'search-flights', component: SearchflightsComponent },
      { path: 'book-flight/:flightId', component: BookFlightComponent, canActivate:[AuthGuard], pathMatch: 'full' },
      { path: 'register-passenger', component: RegisterPassengerComponent, pathMatch: 'full' },
      { path: 'my-bookings', component: MyBookingsComponent, canActivate: [AuthGuard], pathMatch: 'full' },

    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
