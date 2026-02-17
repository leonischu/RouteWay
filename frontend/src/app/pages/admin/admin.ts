import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component } from '@angular/core';
import { Auth } from '../../services/auth';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { Api } from '../../services/api';
import { Booking } from '../../model/Booking-info';
import { Vehicle } from '../../model/Vehicles-Info';
import { User } from '../../model/User-detail';

@Component({
  selector: 'app-admin',
  standalone:true,
  imports: [CommonModule,RouterLink,RouterOutlet],
  templateUrl: './admin.html',
  styleUrl: './admin.css',
})
export class Admin {
 userDetail:any;
 bookings:Booking[]=[];
 vehicles:Vehicle[]=[];
  users:User[]=[]

  constructor(
  public authService:Auth,
  private router:Router,
  private apiService : Api,
  private cdr :ChangeDetectorRef

){}
ngOnInit(){
  this.userDetail = this.authService.getUserName();
  this.loadBooking();
  this.loadVehicle();
  this.loadUsers();
}
isLoggedIn(): boolean {
  return this.authService.isLoggedIn();
}
  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

 loadBooking(): void {
    this.apiService.getBooking().subscribe({
      next: (res) => {
        this.bookings = [...res.data];
        console.log(this.bookings);
        this.cdr.detectChanges();
      },
      error: (err) => console.error(err)
    });
  }




    loadVehicle(): void {
  this.apiService.getVehicle().subscribe({
    next: (res) => {
      this.vehicles = [...res.data]; 
      console.log(this.vehicles); 
      this.cdr.detectChanges();        
    },
    error: (err) => console.error(err),
  });
  }
   
  loadUsers():void {
    this.apiService.getUser().subscribe({
      next :(res)=>{
        this.users = [...res.data];
       
        this.cdr.detectChanges();       

      }
    });
  }




}
