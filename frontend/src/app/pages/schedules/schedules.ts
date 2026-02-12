import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Api } from '../../services/api';
import { Schedule } from '../../model/schedule-info';
import { CommonModule } from '@angular/common';
import { VehicleRoutes } from '../../model/vehicle-route';
import { Vehicle } from '../../model/Vehicles-Info';

@Component({
  selector: 'app-schedules',
  standalone:true,
  imports: [CommonModule],
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
  vehicleName: '',
  routeId: 0,
  travelDate: '',
  departureTime: '',
  arrivalTime: ''
};
 
 error: string = '';
constructor(
  private apiService:Api,
  //  private cdr: ChangeDetectorRef
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
            console.log(this.schedules)
          }
        })
      }

      addSchedule():void {
        this.apiService.addSchedule(this.newSchedule).subscribe(
          {
       next:(response:Schedule[])=>{
       this.schedules.push(...response);
         this.newSchedule = {
               scheduleId: 0,
               vehicleId: 0,
               vehicleName: '',
               routeId: 0,
               travelDate: '',
               departureTime: '',
               arrivalTime: ''
                }
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
          },
          error:(err) => console.error(err)
        });
      }

      loadVehicle() : void {
        this.apiService.getVehicle().subscribe({
          next:(res) =>{
          this.vehicles = [...res.data];
          console.log(this.vehicles);
          },
          error:(err) => console.error(err)
        });
      }

}
