import { ChangeDetectorRef, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { Api } from '../../services/api';
import { Searches, searchResult } from '../../model/Searches';
import { Schedule } from '../../model/schedule-info';
import { Vehicle } from '../../model/Vehicles-Info';
import { VehicleRoutes } from '../../model/vehicle-route';
import { Fare } from '../../model/Fare-Info';
import { Booking } from '../../model/Booking-info';
import { BookForm } from '../../Forms/book-form/book-form';
import { ToastrService } from 'ngx-toastr';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-user-page',
  standalone: true,
  imports: [CommonModule, FormsModule, BookForm,RouterLink],
  templateUrl: './user-page.html',
  styleUrl: './user-page.css',
})
export class UserPage {

  // ---------- Master Data ----------
  vehicles: Vehicle[] = [];
  routes: VehicleRoutes[] = [];
  schedules: Schedule[] = [];
  fares: Fare[] = [];
  bookings: Booking[] = [];

  // ---------- Search ----------
  newSearch: Searches = {
    from: '',
    to: '',
    travelDate: '',
    startTime: '',
    endTime: '',
    maxPrice: 0
  };

  searchResults: searchResult[] = [];
  searchCompleted = false;
  error = '';

  // ---------- Modal ----------
  isModalOpen = false;

  // showForm = false;

openForm() {
  this.isModalOpen  = true;
}

  selectedRide = {
    routeName: '',
    fare: 0,
    departureTime: '',
    arrivalTime: '',
    fareId: 0,
    scheduleId: 0
  };

  constructor(
    private apiService: Api,
    private cdr: ChangeDetectorRef,
      private toastr: ToastrService


  ) {}

  // ---------- Lifecycle ----------
  ngOnInit(): void {
    this.loadSchedule();
    this.loadRides();
    this.loadFare();
    this.loadRoutes();
    this.loadVehicle();
  }

  // ---------- Search ----------
  performSearch(): void {
    this.searchCompleted = false;

    this.apiService.search(this.newSearch).subscribe({
      next: (data: searchResult[]) => {
        this.searchResults = data;
        this.searchCompleted = true;
        this.cdr.detectChanges();
      },
      error: err => console.error('Search error', err)
    });
  }

  loadRides(): void {
    this.apiService.getAllSearch().subscribe({
      next: (res: searchResult[]) => {
        this.searchResults = res;
      },
      error: err => console.error('Error loading rides', err)
    });
  }

  // ---------- Load Master Data ----------
  loadSchedule(): void {
    this.apiService.getSchedule().subscribe({
      next: res => {
        this.schedules = [...res];
        this.cdr.detectChanges();
      },
      error: err => console.error(err)
    });
  }

  loadRoutes(): void {
    this.apiService.getRoutes().subscribe({
      next: res => {
        this.routes = [...res.data];
        this.cdr.detectChanges();
      },
      error: err => console.error(err)
    });
  }

  loadVehicle(): void {
    this.apiService.getVehicle().subscribe({
      next: res => {
        this.vehicles = [...res.data];
        this.cdr.detectChanges();
      },
      error: err => console.error(err)
    });
  }

  loadFare(): void {
    this.apiService.getFare().subscribe({
      next: res => {
        this.fares = res.data;
        this.cdr.detectChanges();
      },
      error: err => {
        this.error = 'Failed to load fares';
        console.error(err);
      }
    });
  }

  loadBooking(): void {
    this.apiService.getBooking().subscribe({
      next: res => {
        this.bookings = [...res.data];
        this.cdr.detectChanges();
      },
      error: err => console.error(err)
    });
  }

  // ---------- Book Now ----------
  bookNow(result: searchResult): void {
    this.selectedRide = {
      routeName: result.routeName,
      fare: result.price,
      departureTime: result.departureTime,
      arrivalTime: result.arrivalTime,
      fareId: result.fareId,
      scheduleId: result.scheduleId
    };

    this.isModalOpen = true;
  }

  closeForm(): void {
    this.isModalOpen = false;
  }

  // ---------- Receive booking from form ----------
  handleBooking(bookingPayload: any): void {
    console.log('Booking payload:', bookingPayload);


    this.apiService.newBookings(bookingPayload).subscribe({
      next: () => {
              console.log('Booking successful');
               this.toastr.success('Booking Successful!', 'Success ');
         this.cdr.detectChanges();
        this.isModalOpen = false;
        this.loadBooking();
      },
 error: err => {
      this.toastr.error('Booking Failed!', 'Error ');
      console.error(err);
    }
    });
  }
}