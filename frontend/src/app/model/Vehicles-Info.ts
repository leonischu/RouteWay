export interface Vehicle{
    vehicleId:number,
    vehicleNumber:string,
    vehicleType:string,
    capacity:number,
    routeId:number
}
export interface VehicleInterface{
    data:Vehicle[];
}