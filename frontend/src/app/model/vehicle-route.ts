
export interface VehicleRoutes {
  routeId: number;
  source: string;
  destination: string;
  distance: number;
  IsDeleted:number;
}
export interface VehicleRoutesInterface {
  data: VehicleRoutes[];
}