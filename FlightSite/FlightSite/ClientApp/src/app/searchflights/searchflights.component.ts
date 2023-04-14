import { Component, OnInit } from '@angular/core';
import { FlightService } from './../api/services/flight.service'
import { FlightRm } from './../api/models/flight-rm'

@Component({
  selector: 'app-searchflights',
  templateUrl: './searchflights.component.html',
  styleUrls: ['./searchflights.component.css']
})
export class SearchflightsComponent implements OnInit {

  searchResult: FlightRm[] = []
  constructor(private flightService: FlightService) { }

  ngOnInit(): void {
  }

  search() {
    this.flightService.searchFlight({})
      .subscribe(response => this.searchResult = response,
      this.handleError)
  }

  private handleError(err: any) {
    console.log(err);
  }
}


