import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class FeatureService {
  public features: Feature[];

  constructor(private http: HttpClient) { }

  getFeatures() {
    return this.http.get<Feature[]>('feature');
  }
}

interface Feature {
  id: number;
  name: string;
}
