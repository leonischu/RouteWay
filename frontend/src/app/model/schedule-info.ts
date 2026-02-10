export interface Schedule {
  scheduleId: number;
  vehicleId: number;
  vehicleName: string;
  routeId: number;
  travelDate: string | null;  
  departureTime: string;      
  arrivalTime: string;        
}