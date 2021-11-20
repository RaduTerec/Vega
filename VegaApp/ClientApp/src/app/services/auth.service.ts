import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from './../models/user';
import { Login } from '../models/login';
import jwtDecode from 'jwt-decode';
import { AuthResponse } from '../models/authResponse';
import { Observable } from 'rxjs';

@Injectable()

export class AuthenticationService {
    private readonly userEndpoint = 'api/user';
    private readonly tokenName = 'token';

    constructor(private http: HttpClient) { }

    register(userData: User) {
        return this.http.post(this.userEndpoint, userData);
    }

    login(loginData: Login): Observable<AuthResponse> {
        var response = this.http.put(this.userEndpoint, loginData) as Observable<AuthResponse>;
        this.SaveTokenInLocalStorage(response);

        return response;
    }

    public authenticated(): boolean {
        var token = localStorage.getItem(this.tokenName);
        if (!token) {
            return false;
        }

        var decoded = jwtDecode(token);
        return decoded['exp'] < Date.now();
    }

    public logout() {
        localStorage.removeItem(this.tokenName);
    }

    public isInRole(roleName: string): boolean {
        var token = localStorage.getItem(this.tokenName);
        if (!token) {
            return false;
        }

        var decoded = jwtDecode(token);
        var roles = decoded['roles'];
        return roles.indexOf(roleName) > -1;
    }

    private SaveTokenInLocalStorage(response: Observable<AuthResponse>) {
        response.subscribe({
            next: loginResponse => {
                localStorage.setItem(this.tokenName, loginResponse.token);
            },
            error: error => {
                throw error;
            }
        });
    }
}
