import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { VehicleRoutes, VehicleRoutesInterface } from '../model/vehicle-route';
import { VehicleRoute } from '../pages/vehicle-route/vehicle-route';
import {  Vehicle, VehicleInterface } from '../model/Vehicles-Info';
import { Schedule } from '../model/schedule-info';
import { Fare, FareInterface } from '../model/Fare-Info';
import { Searches, searchResult } from '../model/Searches';
import { Booking, BookingInterface } from '../model/Booking-info';
import { cancelBooking } from '../model/Cancel-Booking';
import { UserInterface } from '../model/User-detail';


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
      



        
      //Services for Fares 
        
        getFare():Observable<FareInterface>{
          return this.http.get<FareInterface>(`${this.apiUrl}api/Fare`);
        }
        addFare(newFare:any):Observable<Fare>{
          return this.http.post<Fare>(`${this.apiUrl}api/Fare`,newFare)
        }



      //Service for search 
      getAllSearch():Observable<searchResult[]>{
        return this.http.get<searchResult[]>(`${this.apiUrl}api/Search`)
      } 

      search(newSearch:any):Observable<searchResult[]>{
        return this.http.post<searchResult[]>(`${this.apiUrl}api/Search`,newSearch);
      }

     //Services for Schedule
     getSchedule():Observable<Schedule[]> {
     return this.http.get<Schedule[]>(`${this.apiUrl}api/Schedule`);
        }

      addSchedule(newSchedule:any):Observable<Schedule[]>{
        return this.http.post<Schedule[]>(`${this.apiUrl}api/Schedule`,newSchedule)
      }


      //Services for Bookings
      getBooking():Observable<BookingInterface>{
        return this.http.get<BookingInterface>( `${this.apiUrl}api/Booking`);
      }

     
      cancelBooking(bookingId:number,cancel:cancelBooking):Observable<any>{
        return this.http.put<any>(`${this.apiUrl}api/BookingCancel/cancel`,cancel);
      }


      newBookings(newBooking:any):Observable<Booking[]>{
        return this.http.post<Booking[]>(`${this.apiUrl}api/Booking`,newBooking);
      }


      //Services for Users

      getUser():Observable<UserInterface>{
        return this.http.get<UserInterface>(`${this.apiUrl}api/User`)
      }

}
