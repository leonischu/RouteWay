import { Component, OnInit, ViewChild } from '@angular/core';
import { Api } from '../../services/api';
import { CommonModule } from '@angular/common';
import { VehicleRoutes, VehicleRoutesInterface } from '../../model/vehicle-route';
import { AddRoutes } from '../../Forms/add-routes/add-routes';


@Component({
  selector: 'app-vehicle-route',
  imports: [CommonModule,AddRoutes],
  templateUrl: './vehicle-route.html',
  styleUrl: './vehicle-route.css',
})
export class VehicleRoute implements OnInit {
    @ViewChild('addRouteForm') addRouteForm!: AddRoutes;  // Access the child component
   

     selectedRouteId!:number;
     isEditMode = false;
  
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

  // viewRouteDetails(route:VehicleRoute){
  //   console.log(route);
  // }
  deleteRoute(routeId:number):void{
    this.apiService.deleteRoutes(routeId).subscribe((rresponse)=>{
      console.log('Route Deleted');
      
    },
    error => {
      console.error('Error deleting routes',error);
    });
  }

  addNewRoute(newRoute:any):void {
    this.apiService.addRoute(newRoute).subscribe((addedRoute)=>{
      console.log('Route added sucessfully',addedRoute);
      
    }, error =>{
      console.error('Error adding route:',error);
    });
  }


  openAddRouteForm(): void {
    this.addRouteForm.openForm();  // Call openForm() from the parent component
  }

  editRoute(route:VehicleRoutes):void{
    this.selectedRouteId = route.routeId;
    this.isEditMode = true;

  

    //patch existing value into from 
    this.addRouteForm.newRoute={
      routeId:route.routeId,
      source:route.source,
      destination:route.destination,
      distance: route.distance
    };
      //open the form 
    this.addRouteForm.openForm();
 
  }

 updateRoute(route: any): void {
  this.apiService
    .editRoute(route.routeId, route)
    .subscribe({
      next: () => {
        this.isEditMode = false;
        this.addRouteForm.isEditMode = false;
        this.ngOnInit();
      },
      error: err => console.error(err)
    });
}


  }



