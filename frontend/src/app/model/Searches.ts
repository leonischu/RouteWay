export interface Searches {
    from:string;
    to:string;
    travelDate:string;
    startTime:string;
    endTime:string;
    maxPrice:number;
      
}

export interface searchResult{
      scheduleId: number;
  routeName: string;
  departureTime: string;  
  arrivalTime: string;    
  price: number;
    fareId: number;
}