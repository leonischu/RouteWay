import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Api } from '../../services/api';
import { ToastrService } from 'ngx-toastr';
import { Booking, BookingInterface } from '../../model/Booking-info';
import { Vehicle } from '../../model/Vehicles-Info';
import { VehicleRoutes } from '../../model/vehicle-route';
import { Schedule } from '../../model/schedule-info';
import { Fare } from '../../model/Fare-Info';
import { Auth } from '../../services/auth';
import { Bookings } from '../../pages/bookings/bookings';
import { CommonModule } from '@angular/common';
import { cancelBooking } from '../../model/Cancel-Booking';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-my-bookings',
  standalone: true,
  imports: [CommonModule,RouterLink],
  templateUrl: './my-bookings.html',
  styleUrl: './my-bookings.css',
})
export class MyBookings implements OnInit {

bookingId: number = 0;
  cancellationReason: string = '';
  loading: boolean = false;


  bookings:Booking[]=[];
  // bookings: Bookings[] = [];
  vehicles: Vehicle[] = [];
  routes: VehicleRoutes[] = [];
  schedules :Schedule[] = [];
  fares : Fare[] = [];
  error: string = '';


  constructor(
    private apiService:Api,
    private cdr:ChangeDetectorRef,
    private toastr:ToastrService, 
       private authService: Auth,


  ){}
  ngOnInit(): void {
    this.loadBooking();
      

    this.loadFare();
    this.loadRoutes();
    this.loadSchdeule();
    this.loadVehicle();


  }

  loadBooking(): void {
  const userId =this.authService.getUserId(); 

  this.apiService.getBookingId(userId).subscribe({
    next: (res) => {
      this.bookings = res.data;
      console.log(this.bookings);
      this.cdr.detectChanges();
    },
    error: (err) => console.error(err)
  });
}




    loadRoutes():void
    {
      this.apiService.getRoutes().subscribe({
        next:(res) => {
          this.routes = [...res.data];
          console.log(this.routes);
          this.cdr.detectChanges();
        },
        error:(err) => console.error(err)

      });
    }

    loadVehicle() : void {
      this.apiService.getVehicle().subscribe({
        next : (res) => {
          this.vehicles = [...res.data];
          console.log(this.vehicles);
          this.cdr.detectChanges();
        },
        error:(err) => console.error(err)
      });
    }


    loadSchdeule() : void {
      this.apiService.getSchedule().subscribe({
        next: (res) =>{
          this.schedules = [...res];
          console.log(this.schedules);
          this.cdr.detectChanges();
        } ,
        error:(err) => console.error(err)
      });
    }

    loadFare():void {
      this.apiService.getFare().subscribe({
        next:(response) =>{
          this.fares = response.data;
          this.cdr.detectChanges();
          console.log(this.fares)
        },
        error:(err) => {
          this.error= 'Failed to load fares';
          console.error(err);
        }
      })
    }


cancelBooking(id: number) { 
  const reason = prompt("Enter cancellation reason:"); 
  if (!id || !reason) {
    this.toastr.error('Booking ID and reason are required.');
    return;
  }
  
  this.loading = true;
  const cancelData: cancelBooking = {
    bookingId: id, 
    cancellationReason: reason
  };
   
  this.apiService.cancelBooking(id, cancelData).subscribe(
    (response) => {
      this.toastr.success('Booking cancelled successfully!');
      this.loading = false;
      this.loadBooking(); 
      this.cdr.detectChanges();
    },
    (error) => {
      this.toastr.error('Failed to cancel booking.');
      this.loading = false;
    }
  );
}  





// getRouteName(routeId: number): string {
//   const route = this.routes.find(r => r.routeId === routeId);
//   return route ? route.source || route.destination : 'N/A';
// }

// getVehicleName(vehicleId: number): string {
//   const vehicle = this.vehicles.find(v => v.vehicleId === vehicleId);
//   return vehicle ? vehicle.vehicleNumber : 'N/A';
// }

// getRouteNameFromFare(fareId: number): string {
//   const fare = this.fares.find(f => f.fareId === fareId);
//   if (!fare) return 'N/A';
//   return this.getRouteName(fare.routeId);
// }

// getVehicleNameFromFare(fareId: number): string {
//   const fare = this.fares.find(f => f.fareId === fareId);
//   if (!fare) return 'N/A';
//   return this.getVehicleName(fare.vehicleId);
// }


}
