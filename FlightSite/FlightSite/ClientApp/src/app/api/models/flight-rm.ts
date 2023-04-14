/* tslint:disable */
/* eslint-disable */
import { TimePlaceRm } from './time-place-rm';
export interface FlightRm {
  airline?: null | string;
  arrival?: TimePlaceRm;
  departue?: TimePlaceRm;
  id?: string;
  price?: null | string;
  remainingNumberOfSeats?: number;
}
