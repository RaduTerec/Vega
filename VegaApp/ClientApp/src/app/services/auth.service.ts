import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from './../models/user';
import { Login } from '../models/login';
import { AuthResponse } from '../models/authResponse';
import { Observable } from 'rxjs';

@Injectable()

export class AuthenticationService {
    private readonly userEndpoint = 'api/user';

    constructor(private http: HttpClient) { }

    register(userData: User) {
        return this.http.post(this.userEndpoint, userData);
    }

    login(loginData: Login) : Observable<AuthResponse> {
        return this.http.put(this.userEndpoint, loginData) as Observable<AuthResponse>;
    }
}
