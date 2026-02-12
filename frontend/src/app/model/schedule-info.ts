export interface Schedule {
  scheduleId: number;
  vehicleId: number;
  
  routeId: number;
  travelDate: string | null;  
  departureTime: string;      
  arrivalTime: string;        
}