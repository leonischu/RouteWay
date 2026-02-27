import { ChangeDetectorRef, Component } from '@angular/core';
import { Api } from '../../services/api';
import { Fare } from '../../model/Fare-Info';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { VehicleRoutes } from '../../model/vehicle-route';
import { RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-fares',
  imports: [CommonModule,FormsModule,RouterLink],
  templateUrl: './fares.html',
  styleUrl: './fares.css',
})
export class Fares {
  fares:Fare[]=[];
    routes: VehicleRoutes[] = [];
   error: string = '';

   newFare: Fare ={
    fareId:0,
    routeName:'',
    price:0,
    routeId:0
   } ;
constructor(
  private apiService :Api,
  private cdr: ChangeDetectorRef,
  private toastr:ToastrService

){}
ngOnInit():void{
  this.getFare();
  this.loadRoutes();
}

getFare():void{
  this.apiService.getFare().subscribe({
    next: (response) =>{
      this.fares = response.data;
            this.cdr.detectChanges();        
    console.log(this.fares)
    },
      error: (err) => {
        this.error = 'Failed to load fares'; 
        console.error(err); 
      }

    }
  )
}


addFare():void {
  this.apiService.addFare(this.newFare).subscribe({
    next:(response:Fare)=>{
       this.fares.push(response);
          this.cdr.detectChanges(); 
        this.newFare = { fareId: 0, routeName: '', price: 0, routeId: 0 }; // Reset the form after successful addition
        console.log(this.newFare);
                  
        // alert('Fare added successfully!');
          this.toastr.success('Fare added sucessfully!');
    }, error: (err) => {
        this.toastr.error('Failed to add fare'); 
        console.error(err);}
  });
}

loadRoutes(): void {
  this.apiService.getRoutes().subscribe({
    next: (res) => {
      this.routes = [...res.data];     
      this.cdr.detectChanges();      
      console.log(this.routes) 
    },
    error: (err) => console.error(err),
  });
}


}

