// export interface VehicleRoutesInterface {data:
// {  routeId: number;
//   source: string;
//   destination: string;
//   distance: number;}[]
// }
export interface VehicleRoutes {
  routeId: number;
  source: string;
  destination: string;
  distance: number;
}
export interface VehicleRoutesInterface {
  data: VehicleRoutes[];
}