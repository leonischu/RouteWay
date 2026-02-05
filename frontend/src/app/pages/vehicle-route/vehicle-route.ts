import { Component, OnInit } from '@angular/core';
import { Api } from '../../services/api';
import { CommonModule } from '@angular/common';
import { VehicleRoutes, VehicleRoutesInterface } from '../../model/vehicle-route';

@Component({
  selector: 'app-vehicle-route',
  imports: [CommonModule],
  templateUrl: './vehicle-route.html',
  styleUrl: './vehicle-route.css',
})
export class VehicleRoute implements OnInit {
  
  vehicleRoutes:VehicleRoutes[]=[];
  constructor(private apiService:Api){}
  
  
 ngOnInit(): void {
    this.apiService.getRoutes().subscribe(
      (data) => { 
        this.vehicleRoutes = data.data;
        console.log(this.vehicleRoutes);
      },
      (error: any) => {
        console.error('Error fetching vehicle routes', error);
      }
    );
  }

  viewRouteDetails(route:VehicleRoute){
    console.log(route);
  }
  deleteRoute(routeId:number):void{
    this.apiService.deleteRoutes(routeId).subscribe((rresponse)=>{
      console.log('Route Deleted');
      
    },
    error => {
      console.error('Error deleting routes',error);
    });
  }


}
