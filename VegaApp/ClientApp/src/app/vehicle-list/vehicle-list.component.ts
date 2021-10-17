import { KeyValuePair } from './../models/key-value-pair';
import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../services/vehicle.service';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  private readonly PAGE_SIZE = 3;
  queryResult: any = {};
  makes: KeyValuePair[];
  query: any = {
    pageSize: this.PAGE_SIZE
  };
  columns = [
    { title: 'Id' },
    { title: 'Make', key: 'make', isSortable: true },
    { title: 'Model', key: 'model', isSortable: true },
    { title: 'Contact Name', key: 'contactName', isSortable: true },
    { }
  ];
  
  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {
    this.vehicleService.getMakes()
    .subscribe(makes => this.makes = makes);

    this.getFilteredVehicles();
  }

  getFilteredVehicles() {
    this.vehicleService.getVehicles(this.query)
    .subscribe(result => this.queryResult = result);
  }

  onFilterChange() {
    this.query.page = 1;
    this.getFilteredVehicles();
  }

  resetFilter() {
    this.query = {
      page: 1,
      pageSize: this.PAGE_SIZE
    };

    this.getFilteredVehicles();
  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName) {
      this.query.IsAscending = !this.query.IsAscending;
    } else {
      this.query.IsAscending = true;
      this.query.sortBy = columnName;
    }

    this.getFilteredVehicles();
  }

  onPageChange(page) {
    this.query.page = page; 
    this.getFilteredVehicles();
  }
}
