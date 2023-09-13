import { Component, OnInit } from '@angular/core';
import { faXmark, faClock, faLocationDot, faPlaneDeparture, faCircle, faUsd, faTicket, faPlaneArrival, faPlane } from '@fortawesome/free-solid-svg-icons';
import { BookingRm, BookDto } from '../api/models';
import { BookingService } from '../api/services/booking.service';
import { AuthService } from '../auth/auth.service';



@Component({
  selector: 'app-my-bookings',
  templateUrl: './my-bookings.component.html',
  styleUrls: ['./my-bookings.component.css']
})
export class MyBookingsComponent implements OnInit {


  bookings!: BookingRm[];


  faClock = faClock;
  faLocationDot = faLocationDot;
  faPlaneDeparture = faPlaneDeparture;
  faCircle = faCircle;
  faUsd = faUsd;
  faTicket = faTicket;
  faPlaneArrival = faPlaneArrival;
  faPlane = faPlane;
  faXmark = faXmark

  constructor(private bookingService: BookingService,
    private authService: AuthService,
    ) { }

  ngOnInit(): void {

   
    this.bookingService.listBooking({ email: this.authService.currentUser?.email?? '' })

      .subscribe({
        next: (response) => {
          this.bookings = response
        },
        error: (error) => {
          console.log("Response Error. Status: ", error.status)
          console.log("Response Error. Status Text: ", error.statusText)
          console.log(error)
        }

      })

  }

  cancel(booking: BookingRm) {
    const dto: BookDto = {
      flightId: booking.flightId,
      numberOfSeats: booking.numberOfBookedSeats,
      passengerEmail: booking.passengerEmail
    };

    this.bookingService.cancelBooking({ body: dto })
      .subscribe({
        next: (_) => {
          this.bookings = this.bookings.filter(b => b != booking)
        },
        error: (error) => {

          console.log("Response Error. Status: ", error.status)
          console.log("Response Error. Status Text: ", error.statusText)
          console.log(error)
        }

      })
  }

};





