import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Api } from '../../services/api';
import { Vehicle } from '../../model/Vehicles-Info';
import { AddVehicle } from '../../Forms/add-vehicle/add-vehicle';
import { VehicleRoutes } from '../../model/vehicle-route';

@Component({
  selector: 'app-vehicles',
  standalone: true,
  imports: [CommonModule, AddVehicle],
  templateUrl: './vehicles.html',
  styleUrl: './vehicles.css',
})
export class Vehicles implements OnInit {

  @ViewChild(AddVehicle) addVehicleForm!: AddVehicle;

  vehicles: Vehicle[] = [];
  routes: VehicleRoutes[] = [];

  isEditMode = false;
  selectedVehicleId = 0;

  constructor(private apiService: Api) {}

  ngOnInit(): void {
    this.loadVehicle();
    this.loadRoutes();
  }

  loadVehicle(): void {
    this.apiService.getVehicle().subscribe(res => {
      this.vehicles = res.data;
    });
  }

  loadRoutes(): void {
    this.apiService.getRoutes().subscribe(res => {
      this.routes = res.data;
    });
  }


  openAddVehicleForm(): void {
    this.isEditMode = false;
    this.addVehicleForm.isEditMode = false;
    this.addVehicleForm.resetForm();
    this.addVehicleForm.openForm();
  }

  addVehicle(vehicle: any): void {
    this.apiService.addVehicle(vehicle).subscribe(() => {
      this.loadVehicle();
    });
  }

 
  editVehicle(vehicle: Vehicle): void {
    this.isEditMode = true;
    this.selectedVehicleId = vehicle.vehicleId;

    this.addVehicleForm.isEditMode = true;
    this.addVehicleForm.newVehicle = { ...vehicle };
    this.addVehicleForm.openForm();
  }

  updateVehicle(vehicle: any): void {
    vehicle.vehicleId = this.selectedVehicleId; 

    this.apiService.updateVehicle(this.selectedVehicleId, vehicle).subscribe(() => {
      this.isEditMode = false;
      this.selectedVehicleId = 0;
      this.loadVehicle();
    });
  }


  deleteVehicle(vehicleId: number): void {
    this.apiService.deleteVehicle(vehicleId).subscribe(() => {
      this.loadVehicle();
    });
  }
}
