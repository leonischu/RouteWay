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
  
newSchedule: Schedule = {
  scheduleId: 0,
  vehicleId: 0,

  routeId: 0,
  travelDate: null,
  departureTime: '',
  arrivalTime: '',
  availableSeats: 0
};
 
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

      addSchedule():void {
        console.log(this.newSchedule)
        this.apiService.addSchedule(this.newSchedule).subscribe(
          {
       next:(response:Schedule[] )=>{
       if (Array.isArray(response)) {
                // Ensure response is an array before using spread
                this.schedules.push(...response); // This spreads the array of schedules into the schedules array
            } else {
                // If response is not an array, directly push it (assuming it's a single object)
                this.schedules.push(response);
            }
              this.cdr.detectChanges();
         this.newSchedule = {
               scheduleId: 0,
               vehicleId: 0,
               
               routeId: 0,
               travelDate: '',
               departureTime: '',
               arrivalTime: '',
               availableSeats:0
                }
                console.log(this.newSchedule)
              },
              
         error:(err) =>{
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


      

}
