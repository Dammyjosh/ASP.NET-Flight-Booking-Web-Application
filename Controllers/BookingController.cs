﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Flights.Data;
using Flights.ReadModels;
using Flights.Dtos;
using Flights.Domain.Error;

namespace Flights.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly Entities _entities;

        public BookingController(Entities entities)
        {

            _entities = entities;
        }

        [HttpGet("{email}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<BookingRm>), 200)]

        public ActionResult<IEnumerable<BookingRm>> List(string email)
        {

            var bookings = _entities.Flights.ToArray()
                .SelectMany(p => p.Bookings
                     .Where(b => b.PassengerEmail == email)
                     .Select(b => new BookingRm(
                        p.Id,
                        p.Airline,
                        p.Price.ToString(),
                        new TimePlaceRm(p.Arrival.Place, p.Arrival.Time),
                        new TimePlaceRm(p.Departure.Place, p.Departure.Time),
                        b.NumberOfSeats,
                        email
                        )));

            return Ok(bookings);
        }


        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(500)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Cancel(BookDto dto)
        {
            var flight = _entities.Flights.Find(dto.FlightId);

            var error = flight?.CancelBooking(dto.PassengerEmail, dto.NumberOfSeats);

            if (error == null)
            {
                _entities.SaveChanges();
                return NoContent();
            }

            if (error is NotFoundError)
                return NotFound();

            throw new Exception($"The error of type: {error.GetType().Name} occurred while canceling the booking made by {dto.PassengerEmail}");

        }
    }
}
