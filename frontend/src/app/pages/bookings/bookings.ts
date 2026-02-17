import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { Api } from '../../services/api';
import { Booking } from '../../model/Booking-info';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { cancelBooking } from '../../model/Cancel-Booking';
import { FormsModule } from '@angular/forms';
import { BookingForm } from '../../Forms/booking-form/booking-form';
import { Vehicle } from '../../model/Vehicles-Info';
import { VehicleRoutes } from '../../model/vehicle-route';
import { Schedule } from '../../model/schedule-info';
import { Fare } from '../../model/Fare-Info';

@Component({
  selector: 'app-bookings',
    standalone:true,
  imports: [CommonModule,RouterLink,FormsModule,BookingForm],
  templateUrl: './bookings.html',
  styleUrl: './bookings.css',
})
export class Bookings implements OnInit{
   @ViewChild(BookingForm) addBookingForm!:BookingForm ;
  
  error: string = '';

  vehicles: Vehicle[] = [];
  routes: VehicleRoutes[] = [];
  schedules :Schedule[] = [];
  fares : Fare[] = [];
  
  filteredVehicles: Vehicle[] = [];
  filteredSchedules: Schedule[] = [];
  selectedFare: Fare | null = null;
  


  isEditMode = false;
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
    this.loadFare();
    this.loadRoutes();
    this.loadSchdeule();
    this.loadVehicle();
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
    );}

    openBookingModal() : void {
      this.isEditMode = false;
      this.addBookingForm.resetForm();
      this.addBookingForm.openForm();
    }
    addBooking(Booking:any):void {
      this.apiService.newBookings(Booking).subscribe(() => {
        this.loadBooking();
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
 // ----------------- Cascading Dropdown Logic -----------------
  onRouteChange(routeId: number) {
    this.filteredVehicles = this.vehicles.filter(v => v.routeId === routeId);
    this.filteredSchedules = [];
    this.selectedFare = null;
  }

  onVehicleChange(vehicleId: number) {
    this.filteredSchedules = this.schedules.filter(s => s.vehicleId === vehicleId);
    this.selectedFare = null;
  }

  onScheduleChange(scheduleId: number) {
    const schedule = this.schedules.find(s => s.scheduleId === scheduleId);
    if (!schedule) return;

    this.selectedFare = this.fares.find(f => f.routeId === schedule.routeId) || null;
  }


  }




