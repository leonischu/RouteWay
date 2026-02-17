import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Schedule } from '../../model/schedule-info';
import { Fare } from '../../model/Fare-Info';
import { VehicleRoutes } from '../../model/vehicle-route';
import { Vehicle } from '../../model/Vehicles-Info';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-booking-form',
  standalone:true,
  imports: [CommonModule,FormsModule],
  templateUrl: './booking-form.html',
  styleUrl: './booking-form.css',
})
export class BookingForm {

  isModalOpen = false;
  

  newBooking = {
    vehicleId:0,
 routeId: 0,
  scheduleId: 0,
  fareId: 0,
  passengerName: '',
  passengerPhone: '',
  seats: 0

  }

  @Input() schedules :Schedule[] = [];
  @Input() fares : Fare[] = [];
  @Input() routes: VehicleRoutes[] = [];
  @Input() vehicles: Vehicle[] = [];

  @Output() newBookings = new EventEmitter<any>();


  openForm(){
    this.isModalOpen = true;
  }

  closeForm(){
    this.isModalOpen = false;
  }

  resetForm(){  
   this.newBooking = {
    vehicleId: 0,
         routeId: 0,
         scheduleId: 0,
         fareId: 0,
         passengerName: '',
         passengerPhone: '',
         seats: 1
      }
  }
    submitForm(){
  if(this.newBooking.routeId === 0 ||   !this.newBooking.passengerName ||
    !this.newBooking.passengerPhone ||
    this.newBooking.seats <= 0 ||
    this.newBooking.scheduleId === 0 ||
    this.newBooking.fareId === 0){
    return;
   }
   this.newBookings.emit(this.newBooking);
 

  this.closeForm();
  this.resetForm();
   }
  onRouteChange() {
    
  console.log("Selected Route:", this.newBooking.routeId);
  console.log("Available Fares:", this.fares);

  this.newBooking.vehicleId = 0;
    this.newBooking.scheduleId = 0;
    this.newBooking.fareId = 0;
 if (!this.newBooking.routeId) return;

  const fare = this.fares.find(
    f => f.routeId === Number(this.newBooking.routeId)
  );

  if (fare) {
    this.newBooking.fareId = fare.fareId;
  } else {
    this.newBooking.fareId = 0;
  }

  console.log("Selected RouteId:", this.newBooking.routeId);
  console.log("Matched Fare:", fare);


  }
   onVehicleChange() {
    this.newBooking.scheduleId = 0;

  }
  onScheduleChange() {
    const schedule = this.schedules.find(
      s => s.scheduleId === this.newBooking.scheduleId
    );

    if (!schedule) return;

    // const fare = this.fares.find(
    //   f => f.routeId === schedule.routeId
    // );

    // this.newBooking.fareId = fare ? fare.fareId : 0;
  }

  getFarePrice(fareId: number): number | '' {
 if (!fareId) return '';
  const fare = this.fares.find(f => f.fareId === fareId);
  return fare ? fare.price : '';
}


get filteredVehicles(): Vehicle[] {
  const rid = Number(this.newBooking.routeId);
  return this.vehicles.filter(v => Number((v as any).routeId) === rid);
}

}


