import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { VehicleRoutes, VehicleRoutesInterface } from '../model/vehicle-route';
import { VehicleRoute } from '../pages/vehicle-route/vehicle-route';
import {  Vehicle, VehicleInterface } from '../model/Vehicles-Info';

@Injectable({
  providedIn: 'root',
})
export class Api {
  private apiUrl = 'http://localhost:5278/'; 

  constructor(private http:HttpClient){}

//Services For Routes
  getRoutes():Observable<VehicleRoutesInterface>{
    return this.http.get<VehicleRoutesInterface>(`${this.apiUrl}api/VehicleRoute`);
  }
  deleteRoutes(routeId:number): Observable<any> {
    return this.http.delete<any>( `${this.apiUrl}api/VehicleRoute/${routeId}`)
  } 
  addRoute(newRoute:any):Observable<VehicleRoute>{
    return this.http.post<VehicleRoute>(`${this.apiUrl}api/vehicleRoute`,newRoute)
  }
  editRoute(routeId:number, routeData:VehicleRoutes){
    return this.http.put<VehicleRoutes>(`${this.apiUrl}api/VehicleRoute/${routeId}`,routeData)
  }

// Services For Vehicles 
      getVehicle():Observable<VehicleInterface>{
      return this.http.get<VehicleInterface>(`${this.apiUrl}api/Vehicle`);
      }
      
      deleteVehicle(vehicleId:number):Observable<any>{
        return this.http.delete<any>(`${this.apiUrl}api/Vehicle/${vehicleId}`);
      }
      addVehicle(newVehicle:any):Observable<Vehicle>{
        return this.http.post<Vehicle>(`${this.apiUrl}api/Vehicle`,newVehicle);
      }
      updateVehicle(vehicleId:number,vehicleData:Vehicle){
        return this.http.put<Vehicle>(`${this.apiUrl}api/Vehicle`,vehicleData)
      }
      

}
