import { ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { Api } from '../../services/api';
import { Searches, searchResult } from '../../model/Searches';
import { errorContext } from 'rxjs/internal/util/errorContext';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Schedule } from '../../model/schedule-info';
import { Vehicle } from '../../model/Vehicles-Info';
import { VehicleRoutes } from '../../model/vehicle-route';
import { Fare } from '../../model/Fare-Info';
import { Booking } from '../../model/Booking-info';
import { BookingForm } from '../../Forms/booking-form/booking-form';
import { BookForm } from '../../Forms/book-form/book-form';

@Component({
  selector: 'app-user-page',
  imports: [CommonModule,FormsModule,BookForm],
  templateUrl: './user-page.html',
  styleUrl: './user-page.css',
})
export class UserPage {
  // @ViewChild(BookForm) addBookForm!:BookForm ;
   vehicles: Vehicle[] = [];
   routes: VehicleRoutes[] = [];
   schedules:Schedule[] = [];
   fares : Fare[] = [];
  
  filteredVehicles: Vehicle[] = [];
  filteredSchedules: Schedule[] = [];
  selectedFare: Fare | null = null;
  isEditMode = false;
  bookings:Booking[]=[];



   isModalOpen = false;

  selectedRide = {

    routeName: '',
    fare: 0,
    departureTime: '',
    arrivalTime: '',
    fareId:0
    // vehicle: '',
    // routeId:0,
    // vehicleId:0,
    // scheduleId:0
  };

//For Search

  newSearch: Searches = {
    from: '',
    to: '',
    travelDate: '',
    startTime: '',
    endTime: '',
    maxPrice: 0
  };

  searchResults:searchResult[] = [];
  searchCompleted: boolean = false;
  error: string = '';
  

  constructor(
     private apiService:Api,
     private cdr: ChangeDetectorRef

  ){}
  ngOnInit():void{
//  this.performSearch();
 this.loadSchedule();
 this.loadRides();
     this.loadFare();
    this.loadRoutes();
    this.loadVehicle();
  }
  performSearch(){
    this.searchCompleted = false; 
        console.log(this.newSearch)
    this.apiService.search(this.newSearch).subscribe(
      (data)=>{
        this.searchResults = data;
         this.searchCompleted = true;
              this.cdr.detectChanges();     
    
      },
      (error)=> {
        console.error('Search error', error);
      }
    );
  }

  loadSchedule():void{
    this.apiService.getSchedule().subscribe({
      next: (res:Schedule[]) => {
        this.schedules=[...res];
        console.log(this.schedules);
        this.cdr.detectChanges();       
      },
     error:(err) => console.error(err)
    })
  }


loadRides(): void {
    this.apiService.getAllSearch().subscribe(
      (searchResults: searchResult[]) => {
        this.searchResults = searchResults;
        console.log( this.searchResults);
      },
       (err) => {
        console.error('Error loading rides', err);
        this.cdr.detectChanges();
      }
    );
  }


  // For Bookings

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
  // onRouteChange(routeId: number) {
  //   this.filteredVehicles = this.vehicles.filter(v => v.routeId === routeId);
  //   this.filteredSchedules = [];
  //   this.selectedFare = null;
  // }

  // onVehicleChange(vehicleId: number) {
  //   this.filteredSchedules = this.schedules.filter(s => s.vehicleId === vehicleId);
  //   this.selectedFare = null;
  // }

  // onScheduleChange(scheduleId: number) {
  //   const schedule = this.schedules.find(s => s.scheduleId === scheduleId);
  //   if (!schedule) return;

  //   this.selectedFare = this.fares.find(f => f.routeId === schedule.routeId) || null;
  // }


 openForm(routeName: string, fare: number, departureTime: string, arrivalTime: string,fareId:number): void {
  this.selectedRide = {
    routeName,
    fareId,
    fare,
    departureTime,
    arrivalTime
  };

  this.isModalOpen = true;
}

  closeForm():void {
    this.isModalOpen = false;
  }
 handleBooking(Booking:any):void {
      this.apiService.newBookings(Booking).subscribe(() => {
        this.loadBooking();
      });
    }

}
