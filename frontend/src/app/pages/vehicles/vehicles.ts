import { Component } from '@angular/core';
import { Vehicle } from '../../model/Vehicles-Info';
import { Api } from '../../services/api';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-vehicles',
  imports: [CommonModule],
  templateUrl: './vehicles.html',
  styleUrl: './vehicles.css',
})
export class Vehicles {
  vehicle:Vehicle[]=[];

  constructor(private apiService:Api){}
  ngOnInit(): void {
    this.apiService.getVehicle().subscribe(
      (data) => {
        this.vehicle= data.data;
        console.log(this.vehicle);
        
      },
      (error:any) => {
        console.error("Error while fetching vehicle ",error)
      }
    );
   
  }

 deleteVehicle(vehicleId:number):void{
      this.apiService.deleteVehicle(vehicleId).subscribe((response)=>
      {
        console.log("Vehicle Deleted sucessfully");

      },
      error => {
        console.error ('Error deleting vehicle ',error);

      }
      );
    }

}
