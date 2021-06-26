import { SaveVehicle } from '../models/save-vehicle';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Vehicle } from '../models/vehicle';

@Injectable()

export class VehicleService {

  constructor(private http: HttpClient) { }

  getMakes() {
    return this.http.get<any>('api/make');
  }

  getFeatures() {
    return this.http.get<any>('api/feature');
  }

  create(vehicle: SaveVehicle) {
    return this.http.post('/api/vehicle', vehicle);
  }

  getVehicle(id: number) {
    return this.http.get('/api/vehicle/' + id);
  }

  getVehicles():Observable<Vehicle[]> {
    return this.http.get('/api/vehicle/') as Observable<Vehicle[]>;
  }

  update(vehicle: SaveVehicle) {
    return this.http.put('/api/vehicle/' + vehicle.id, vehicle);
  }

  delete(id: number) {
    return this.http.delete('/api/vehicle/' + id);
  }
}
