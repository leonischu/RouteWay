import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { VehicleRoutesInterface } from '../model/vehicle-route';

@Injectable({
  providedIn: 'root',
})
export class Api {
  private apiUrl = 'http://localhost:5278/'; 

  constructor(private http:HttpClient){}


  getRoutes():Observable<VehicleRoutesInterface>{
    return this.http.get<VehicleRoutesInterface>(`${this.apiUrl}api/VehicleRoute`);
  }
  deleteRoutes(routeId:number): Observable<any> {
    return this.http.delete<any>( `${this.apiUrl}api/VehicleRoute/${routeId}`)
  } 

}
