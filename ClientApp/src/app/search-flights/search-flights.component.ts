import { Component, OnInit } from '@angular/core';
import { faSearch, faClock, faLocationDot, faPlaneDeparture, faCircle, faUsd, faTicket, faPlaneArrival, faPlane } from '@fortawesome/free-solid-svg-icons';
import { FlightService } from '../api/services/flight.service';
import { FlightRm } from '../api/models';
import { FormBuilder } from '@angular/forms';


@Component({
  selector: 'app-search-flights',
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css']
})
export class SearchFlightsComponent implements OnInit {

  faSearch = faSearch;
  faClock = faClock;
  faLocationDot = faLocationDot;
  faPlaneDeparture = faPlaneDeparture;
  faCircle = faCircle;
  faUsd = faUsd;
  faTicket = faTicket;
  faPlaneArrival = faPlaneArrival;
  faPlane = faPlane;



  searchResult: FlightRm[] = []

  constructor(private flightService: FlightService,
    private fb: FormBuilder ) { }

  searchForm = this.fb.nonNullable.group({
    from: [''],
    destination: [''],
    fromDate: [''],
    toDate: [''],
    numberOfPassengers: [1]
  })


  ngOnInit(): void {
    this.search
  }

  search() {
    this.flightService.searchFlight(this.searchForm.value)
      .subscribe({
        next: (response) => {
          this.searchResult = response;
        },
        error: (error) => {
          console.log("Response Error. Status: ", error.status)
          console.log("Response Error. Status Text: ", error.statusText)
          console.log(error)
        }

      })
    }
  }

