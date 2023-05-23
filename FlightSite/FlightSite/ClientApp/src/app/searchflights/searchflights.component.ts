import { Component, OnInit } from '@angular/core';
import { FlightService } from './../api/services/flight.service'
import { FlightRm } from './../api/models/flight-rm'
import { FormBuilder } from '@angular/forms'

@Component({
  selector: 'app-searchflights',
  templateUrl: './searchflights.component.html',
  styleUrls: ['./searchflights.component.css']
})
export class SearchflightsComponent implements OnInit {

  searchResult: FlightRm[] = []
  constructor(private flightService: FlightService, private fb: FormBuilder) { }

  searchForm = this.fb.group({
    fromDate: [''],
    toDate: [''],
    source: [''],
    destination: [''],
    numberOfPassenger: [1]
  })

  ngOnInit(): void {
    this.search();
  }

  search() {
    this.flightService.searchFlight(this.searchForm.value)
      .subscribe(response => this.searchResult = response,
      this.handleError)
  }

  private handleError(err: any) {
    console.log("Response Error Status: ", err.status);
    console.log("Response Text: ", err.statusText);
    console.log(err);
  }
}


