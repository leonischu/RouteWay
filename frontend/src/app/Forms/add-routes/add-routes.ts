import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-routes',
  imports: [FormsModule,CommonModule],
  templateUrl: './add-routes.html',
  styleUrl: './add-routes.css',
})
export class AddRoutes {
  //Modal ko visibility ko lagi
  isModalOpen = false;

  //New route ko data hold garna 

  newRoute ={
    routeId:0,
    source:'',
    destination:'',
    distance:0
  };
  // use event emitter to notify the parent component when new route is added 
   @Input() isEditMode = false;
   @Output() newRouteAdded = new EventEmitter<any>();
   @Output() routeUpdated = new EventEmitter<any>();
   
  //Opens the Form 

  openForm(){
    this.isModalOpen = true;
  }

  //Closes the form 
  closeForm() {
    this.isModalOpen= false;
  }

  //Reset Form 
  resetForm(){
    this.newRoute = { routeId:0,source:'',destination:'',distance:0}
  }




  //Add the new Route 
  addRoute(){
    if(this.newRoute.source && this.newRoute.destination && this.newRoute.distance)
    {
      this.newRouteAdded.emit(this.newRoute); //Esle new route data parent ma emit garcha
      this.closeForm(); //this closes the form after adding new data 
      this.resetForm(); //Reset the fields of Forms
    }

  }

   submitForm() {
    if (this.isEditMode) {
      this.routeUpdated.emit(this.newRoute);   //  PUT
    } else {
      this.newRouteAdded.emit(this.newRoute);  //  POST
    }

    this.closeForm();
    this.resetForm();
  }

  
  

}
