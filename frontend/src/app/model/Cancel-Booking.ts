export interface cancelBooking{
    bookingId:number;
    cancellationReason:string;
}

export interface cancelBookingInterface{
    data:cancelBooking[];
}