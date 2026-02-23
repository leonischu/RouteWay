import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { searchResult } from '../../model/Searches';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-book-form',
  imports: [FormsModule,CommonModule],
  templateUrl: './book-form.html',
  styleUrl: './book-form.css',
})
export class BookForm {
  
  @Input() routeName: string = '';
  @Input() fare!: number ;
    @Input() fareId!: number;
  @Input() departureTime: string = '';
  @Input() arrivalTime: string = '';
  @Input() vehicle: string = '';
  @Input() routeId: number = 0;
  @Input() vehicleId: number = 0;
  @Input() scheduleId: number = 0;

  @Output() newBookings = new EventEmitter<any>();
  @Output() close = new EventEmitter<void>();

  // isModalOpen = true;
  newBooking = {
    passengerName: '',
    passengerPhone: '',
    seats: 1,
    bookingStatus: 'CONFIRMED'
  };
  
//   closeForm(){
// this.isModalOpen=false;
//   }


  
  submitForm() {
    if (
      !this.newBooking.passengerName ||
      !this.newBooking.passengerPhone ||
      this.newBooking.seats <= 0
    ) {
      alert('Please fill in all fields!');
      return;
    }

    const totalAmount = this.fare * this.newBooking.seats;

    const bookingDetails = {
      passengerName: this.newBooking.passengerName,
      passengerPhone: this.newBooking.passengerPhone,
      seats: this.newBooking.seats,
      totalAmount,
      bookingStatus: this.newBooking.bookingStatus,

      // IDs
      routeId: this.routeId,
      vehicleId: this.vehicleId,
      scheduleId: this.scheduleId,
      fareId: this.fareId,

      // Display-only info
      routeName: this.routeName,
      departureTime: this.departureTime,
      arrivalTime: this.arrivalTime,
      vehicle: this.vehicle,
    };

    this.newBookings.emit(bookingDetails);
    // this.closeForm();
  }
}
