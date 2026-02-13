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

  bookings:Booking[]=[]
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


}
