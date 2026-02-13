import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Api } from '../../services/api';
import { Booking } from '../../model/Booking-info';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-bookings',
  imports: [CommonModule,RouterLink],
  templateUrl: './bookings.html',
  styleUrl: './bookings.css',
})
export class Bookings implements OnInit{

  bookings:Booking[]=[];



// bookings = [];  // Array to store booking details (if needed for display)
//   bookingId: number;  // The ID of the booking to cancel
//   cancellationReason: string = '';  // Reason for cancellation


// cancelBooking(): void {
//     // Create the cancelBooking data object
//     const cancelBookingData: cancelBooking = {
//       bookingId: this.bookingId,
//       cancellationReason: this.cancellationReason,
//     };
// this.bookingService.cancelBooking(this.bookingId, cancelBookingData).subscribe({
//       next: (response) => {
//         console.log('Booking cancelled successfully:', response);
//         // Handle successful cancellation, e.g., show a success message, refresh bookings list, etc.
//       },
//       error: (err) => {
//         console.error('Error cancelling booking:', err);
//         // Handle error, e.g., show an error message
//       },
//     });
//   }

//   // Optional: You can add a method to load bookings into the bookings array if needed
//   loadBooking(): void {
//     // Call a service to load bookings (if needed for your UI)
//     // Example: this.bookingService.getBooking().subscribe(res => this.bookings = res.data);
//   }
// }








  constructor(
    private apiService:Api,
    private cdr:ChangeDetectorRef
  ){}
  
  ngOnInit(): void {
    this.loadBooking();  
  }

  loadBooking(): void {
    this.apiService.getBooking().subscribe({
      next: (res) => {
        this.bookings = [...res.data];
        console.log(this.bookings);
        this.cdr.detectChanges();
      },
      error: (err) => console.error(err)
    });
  }

  cancelBooking():void {
    this.apiService.cancelBooking().subscribe({

    })
  }


}
