import { Component } from '@angular/core';
import { BookDto, BookingRm } from '../api/models'
import { BookingService } from '../api/services'
import { AuthService } from './../auth/auth.service'
import { Router } from '@angular/router'

@Component({
  selector: 'app-my-bookings',
  templateUrl: './my-bookings.component.html',
  styleUrls: ['./my-bookings.component.css']
})
export class MyBookingsComponent {
  bookings!: BookingRm[];

  constructor(private bookingService: BookingService, private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.bookingService.listBooking({ email: this.authService.currentUser?.email ?? '' })
      .subscribe(r => this.bookings = r, this.handleError);
  }

  private handleError(err: any) {
    console.log("Response error status: ", err.status);
    console.log("Response error status text ", err.statusText);
    console.log(err);
  }

  cancel(booking: BookingRm) {
    const dto: BookDto = {
      flightId: booking.flightId,
      numberOfSeats: booking.numberOfBookedSeats,
      passengerEmail: booking.passengerEmail,
    };

    this.bookingService.cancelBooking({ body: dto })
      .subscribe(_ => this.bookings = this.bookings.filter(b => b != booking), this.handleError)

  }
}
