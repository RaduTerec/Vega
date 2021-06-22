import { SaveVehicle } from '../models/save-vehicle';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()

export class VehicleService {

  constructor(private http: HttpClient) { }

  getMakes() {
    return this.http.get<any>('api/make');
  }

  getFeatures() {
    return this.http.get<any>('api/feature');
  }

  create(vehicle) {
    return this.http.post('/api/vehicle', vehicle);
  }

  getVehicle(id) {
    return this.http.get('/api/vehicle/' + id);
  }

  update(vehicle : SaveVehicle) {
    return this.http.put('/api/vehicle/' + vehicle.id, vehicle);
  }
}
