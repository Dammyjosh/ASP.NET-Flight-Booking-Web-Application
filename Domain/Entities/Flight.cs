using Flights.Domain.Entities;
using Flights.Domain.Error;
using Flights.Domain.OverBookError;
using Flights.Dtos;
using Flights.ReadModels;
using Microsoft.AspNetCore.Http.Features;
using System.Collections.Generic;
using System.Numerics;

namespace Flights.Domain.Entities
{
    public class Plane
    {
        public Guid Id { get; set; }
        public string Airline { get; set; }
        public string Price { get; set; }
        public TimePlace Departure { get; set; }
        public TimePlace Arrival { get; set; }
        public int RemainingNumberOfSeats { get; set; }

        public IList<Booking> Bookings = new List<Booking>();

        public Plane()
        {


        }

        public Plane(
        Guid id,
        string airline,
        string price,
        TimePlace departure,
        TimePlace arrival,
        int remainingNumberOfSeats
        )
        {
            Id = id;
            Airline = airline;
            Price = price;
            Departure = departure;
            Arrival = arrival;
            RemainingNumberOfSeats = remainingNumberOfSeats;

        }


        public object ? MakeBooking(string passengerEmail, int numberOfSeats)
        {
            var flight = this;
            if (flight.RemainingNumberOfSeats < numberOfSeats)
            {
                return new OverbookError();
            }


            flight.Bookings.Add(
                new Booking(
                    passengerEmail,
                    numberOfSeats)
                );

            flight.RemainingNumberOfSeats -= numberOfSeats;
            return null;
        }

        public object ? CancelBooking(string passengerEmail, int numberOfSeats)
        {

            var booking = Bookings.FirstOrDefault(b => numberOfSeats == b.NumberOfSeats
            && passengerEmail.ToLower() == b.PassengerEmail.ToLower());

            if (booking == null)
                return new NotFoundError();

            Bookings.Remove(booking);
            RemainingNumberOfSeats += booking.NumberOfSeats;

            return null;
        }
    }
}

