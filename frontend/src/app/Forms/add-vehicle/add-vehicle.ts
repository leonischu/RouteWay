import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { VehicleRoutes } from '../../model/vehicle-route';

@Component({
  selector: 'app-add-vehicle',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-vehicle.html',
  styleUrl: './add-vehicle.css',
})
export class AddVehicle {

  isModalOpen = false;

  @Input() isEditMode = false;
  @Input() routes: VehicleRoutes[] = [];

  @Output() vehicleAdded = new EventEmitter<any>();
  @Output() vehicleUpdated = new EventEmitter<any>();

  newVehicle = {
    vehicleId: 0,
    routeId: 0,
    vehicleNumber: '',
    vehicleType: '',
    capacity: 0
  };

  openForm() {
    this.isModalOpen = true;
  }

  closeForm() {
    this.isModalOpen = false;
  }

  resetForm() {
    this.newVehicle = {
      vehicleId: 0,
      routeId: 0,
      vehicleNumber: '',
      vehicleType: '',
      capacity: 0
    };
  }

  submitForm() {
    if (
      this.newVehicle.vehicleNumber &&
      this.newVehicle.vehicleType &&
      this.newVehicle.capacity > 0
    ) {
      if (this.isEditMode) {
        this.vehicleUpdated.emit(this.newVehicle);
      } else {
        this.vehicleAdded.emit(this.newVehicle);
      }

      this.closeForm();
      this.resetForm();
    }
  }
}
