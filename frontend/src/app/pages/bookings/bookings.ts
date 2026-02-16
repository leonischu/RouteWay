import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Api } from '../../services/api';
import { Booking } from '../../model/Booking-info';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { cancelBooking } from '../../model/Cancel-Booking';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-bookings',
  imports: [CommonModule,RouterLink,FormsModule],
  templateUrl: './bookings.html',
  styleUrl: './bookings.css',
})
export class Bookings implements OnInit{

  bookings:Booking[]=[];

 bookingId: number = 0;
  cancellationReason: string = '';
  loading: boolean = false;


  constructor(
    private apiService:Api,
    private cdr:ChangeDetectorRef,
     private toastr: ToastrService
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


  cancelBooking(){
    if(!this.bookingId || !this.cancellationReason) {
      this.toastr.error('Please provide a valid BookingId and cancellation reason');
    return;
    }
    this.loading = true;
    
    const cancelData:cancelBooking = {
      bookingId:this.bookingId,
      cancellationReason:this.cancellationReason
    };
     
    this.apiService.cancelBooking(this.bookingId,cancelData).subscribe(
      (response)=>{
        this.toastr.success('Booking cancelled sucessfully !');
        this.loading=false;
        this.bookingId = 0;
        this.cancellationReason='';
        this.loadBooking();
        this.cdr.detectChanges();
        
      },
      (error) =>{
        this.toastr.error('Failed to cancel booking.Please try again');
        this.loading = false;
      }
    );


    }
  }




