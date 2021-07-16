import { KeyValuePair } from './../models/key-value-pair';
import { Component, OnInit } from '@angular/core';
import { Vehicle } from '../models/vehicle';
import { VehicleService } from '../services/vehicle.service';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  vehicles: Vehicle[];
  makes: KeyValuePair[];
  query: any = {};
  
  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {
    this.vehicleService.getMakes()
    .subscribe(makes => this.makes = makes);

    this.getFilteredVehicles();
  }

  getFilteredVehicles() {
    this.vehicleService.getVehicles(this.query)
    .subscribe(vehicles => this.vehicles = vehicles);
  }

  onFilterChange() {
    this.getFilteredVehicles();
  }

  resetFilter() {
    this.query = {};
    this.onFilterChange();
  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName) {
      this.query.IsAscending = false;
    } else {
      this.query.IsAscending = true;
      this.query.sortBy = columnName;
    }

    this.getFilteredVehicles();
  }
}
