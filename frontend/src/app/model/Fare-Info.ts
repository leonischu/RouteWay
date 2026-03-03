export interface Fare{
  fareId: number;
  routeName: string;
  price: number;
  routeId:number;
  vehicleId:number
  vehicleNumber:string;
  vehicleType: string;
}
export interface FareInterface{
    data:Fare[];
}
