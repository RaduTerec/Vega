import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class PhotoService {

  constructor(private http: HttpClient) { }

  upload(vehicleId, photo) {
    var formData = new FormData();
    formData.append('file', photo);
    return this.http.post(`/api/vehicle/${vehicleId}/photos`, formData, {
      reportProgress: true,
      observe: 'events',
    });
  }

  getPhotos(vehicleId) {
    return this.http.get(`/api/vehicle/${vehicleId}/photos`);
  }
}