import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()

export class MakeService {
  public makes: Make[];

  constructor(private http: HttpClient) { }

  getMakes() {
    return this.http.get<Make[]>('make');
  }
}

interface Make {
  name: string;
  models: Model[];
}

interface Model {
  name: string;
}
