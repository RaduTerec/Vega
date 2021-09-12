import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from './../models/user';
import { Login } from '../models/login';

@Injectable()

export class AuthenticationService {
    private readonly userEndpoint = 'api/user';

    constructor(private http: HttpClient) { }

    register(userData: User) {
        return this.http.post(this.userEndpoint, userData);
    }

    login(loginData: Login) {
        return this.http.put(this.userEndpoint, loginData);
    }
}