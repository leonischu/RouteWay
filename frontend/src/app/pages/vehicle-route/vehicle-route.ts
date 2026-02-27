import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { Api } from '../../services/api';
import { CommonModule } from '@angular/common';
import { VehicleRoutes, VehicleRoutesInterface } from '../../model/vehicle-route';
import { AddRoutes } from '../../Forms/add-routes/add-routes';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-vehicle-route',
  standalone: true,
  imports: [CommonModule,AddRoutes,RouterLink],
  templateUrl: './vehicle-route.html',
  styleUrl: './vehicle-route.css',
})
export class VehicleRoute implements OnInit {
  @ViewChild(AddRoutes) addRouteForm!: AddRoutes;

  vehicleRoutes: VehicleRoutes[] = [];
  isEditMode = false;

  constructor(private apiService: Api,
      private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadRoutes();
  }

 loadRoutes(): void {
  this.apiService.getRoutes().subscribe({
    next: (res) => {
      this.vehicleRoutes = [...res.data]; 
      this.cdr.detectChanges();     
      console.log(this.vehicleRoutes)      
    },
    error: (err) => console.error(err),
  });
}

  openAddRouteForm() {
    this.isEditMode = false;
    this.addRouteForm.openForm();
  }

  addNewRoute(route: VehicleRoutes) {
    this.apiService.addRoute(route).subscribe({
      next: () => this.loadRoutes(),
      error: (err) => console.error(err),
    });
  }

  editRoute(route: VehicleRoutes) {
    this.isEditMode = true;

    this.addRouteForm.newRoute = { ...route };
    this.addRouteForm.openForm();
  }

  updateRoute(route: VehicleRoutes) {
    this.apiService.editRoute(route.routeId, route).subscribe({
      next: () => {
        this.isEditMode = false;
        this.loadRoutes();
      },
      error: (err) => console.error(err),
    });
  }

  deleteRoute(routeId: number) {
    this.apiService.deleteRoutes(routeId).subscribe({
      next: () => this.loadRoutes(),
      error: (err) => console.error(err),
    });
  }
}




