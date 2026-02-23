export interface Booking {
  bookingId: number;
  scheduleId: number;
  fareId: number;
  passengerName: string;
  passengerPhone: string;
  seats: number;
  totalAmount: number;
  bookingStatus: string;
  createdAt: string;
  // id:number;
}
export interface BookingInterface{
    data:Booking[];
}
   