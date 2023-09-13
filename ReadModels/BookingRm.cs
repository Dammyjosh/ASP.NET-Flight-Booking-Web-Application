﻿namespace Flights.ReadModels
{
    public record BookingRm(
        Guid FlightId,
        string Airline,
        string Price,
        TimePlaceRm Departure,
        TimePlaceRm Arrival,
        int NumberOfBookedSeats,
        string PassengerEmail);
    
}
