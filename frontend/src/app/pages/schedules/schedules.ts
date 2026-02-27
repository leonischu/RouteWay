import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Api } from '../../services/api';
import { Schedule } from '../../model/schedule-info';
import { CommonModule } from '@angular/common';
import { VehicleRoutes } from '../../model/vehicle-route';
import { Vehicle } from '../../model/Vehicles-Info';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-schedules',
  standalone:true,
  imports: [CommonModule,FormsModule,RouterLink],
  templateUrl: './schedules.html',
  styleUrl: './schedules.css',
})
export class Schedules implements OnInit {
schedules: Schedule[] = [];
routes: VehicleRoutes [] = [];
vehicles : Vehicle [] =[];
  
 newSchedule: Schedule = this.getEmptySchedule();

  private getEmptySchedule(): Schedule {
    return {
      scheduleId: 0,
      vehicleId: 0,
      routeId: 0,
      travelDate: '',
      departureTime: '',
      arrivalTime: '',
      availableSeats: 0
    };
  }

 error: string = '';
constructor(
  private apiService:Api,
   private cdr: ChangeDetectorRef
){ }
 

  ngOnInit(): void {
    this.getSchedule();
    this.loadRoutes();
    this.loadVehicle();
      }



      getSchedule(){
        this.apiService.getSchedule().subscribe({
          next:(response) =>{
            this.schedules=response;
            console.log(this.schedules);
            this.cdr.detectChanges();       
          }
        })
      }

       addSchedule(): void {

    // Ensure numbers are numbers
    this.newSchedule.vehicleId = Number(this.newSchedule.vehicleId);
    this.newSchedule.routeId = Number(this.newSchedule.routeId);
    this.newSchedule.availableSeats = Number(this.newSchedule.availableSeats);

    console.log(this.newSchedule);

    this.apiService.addSchedule(this.newSchedule).subscribe({
      next: (response: any) => {

        // Fix travelDate format before pushing
        const formattedResponse = {
          ...response,
          travelDate: response.travelDate
            ? response.travelDate.split('T')[0]
            : ''
        };

        this.schedules.push(formattedResponse);
        this.cdr.detectChanges();

        // Reset cleanly
        this.newSchedule = this.getEmptySchedule();
      },

      error: (err) => {
        this.error = 'Failed to add Schedule';
        console.error(err);
      }
    });
  }

      loadRoutes() : void {
        this.apiService.getRoutes().subscribe({
          next:(res) =>{
            this.routes = [...res.data];
            console.log(this.routes);
            this.cdr.detectChanges();                   
          },
          error:(err) => console.error(err)
        });
      }

      loadVehicle() : void {
        this.apiService.getVehicle().subscribe({
          next:(res) =>{
          this.vehicles = [...res.data];
          console.log(this.vehicles);
          this.cdr.detectChanges();       
          },
          error:(err) => console.error(err)
        });
      }

    deleteSchedule(scheduleId:number ) : void {
      this.apiService.deleteSchedule(scheduleId).subscribe(() =>{
        this.getSchedule();
      })
    }
      

}
