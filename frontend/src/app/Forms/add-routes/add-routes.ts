import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-routes',
  imports: [FormsModule],
  templateUrl: './add-routes.html',
  styleUrl: './add-routes.css',
})
export class AddRoutes {
  //Modal ko visibility ko lagi
  isModalOpen = false;

  //New route ko data hold garna 

  newRoute ={
    source:'',
    destination:'',
    distance:0
  };
  // use event emitter to notify the parent component when new route is added 
   @Output() newRouteAdded = new EventEmitter<any>();
   
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
    this.newRoute = { source:'',destination:'',distance:0}
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

  
  

}
