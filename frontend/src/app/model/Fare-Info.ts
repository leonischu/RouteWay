export interface Fare{
  fareId: number;
  routeName: string;
  price: number;
  routeId:number;
  // vehicleId:number
}
export interface FareInterface{
    data:Fare[];
}
