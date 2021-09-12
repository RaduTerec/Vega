import { SaveVehicle } from '../models/save-vehicle';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Vehicle } from '../models/vehicle';

@Injectable()

export class VehicleService {
  private readonly vehicleEndpoint = 'api/vehicle';

  constructor(private http: HttpClient) { }

  getMakes() {
    return this.http.get<any>('api/make');
  }

  getFeatures() {
    return this.http.get<any>('api/feature');
  }

  create(vehicle: SaveVehicle) {
    return this.http.post(this.vehicleEndpoint, vehicle);
  }

  getVehicle(id: number) {
    return this.http.get(this.vehicleEndpoint + '/' + id);
  }

  getVehicles(filter):Observable<Vehicle[]> {
    return this.http.get(this.vehicleEndpoint + '?' + this.toFilterQuery(filter)) as Observable<Vehicle[]>;
  }

  toFilterQuery(obj) {
    var parts = [];

    for (var property in obj) {
      var value = obj[property];
      
      if (value != null && value != undefined) 
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
    }

    return parts.join('&');
  }

  update(vehicle: SaveVehicle) {
    return this.http.put(this.vehicleEndpoint + '/' + vehicle.id, vehicle);
  }

  delete(id: number) {
    return this.http.delete(this.vehicleEndpoint + '/' + id);
  }
}
