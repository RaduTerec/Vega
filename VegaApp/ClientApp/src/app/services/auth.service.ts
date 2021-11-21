import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from './../models/user';
import { Login } from '../models/login';
import { AuthResponse } from '../models/authResponse';
import { Observable } from 'rxjs';
import { JwtHelperService } from "@auth0/angular-jwt";

@Injectable()

export class AuthService {
    private readonly userEndpoint = 'api/user';
    private readonly tokenName = 'token';
    private jwtService = new JwtHelperService();

    constructor(private http: HttpClient) { }

    register(userData: User) {
        var response = this.http.post(this.userEndpoint, userData) as Observable<AuthResponse>;
        this.SaveTokenInLocalStorage(response);

        return response;
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

        return !this.jwtService.isTokenExpired(token);
    }

    public logout() {
        localStorage.removeItem(this.tokenName);
    }

    public isInRole(roleName: string): boolean {
        var token = localStorage.getItem(this.tokenName);
        if (!token) {
            return false;
        }

        var decoded = this.jwtService.decodeToken(token);
        var roles = decoded['roles'];
        return roles.indexOf(roleName) > -1;
    }

    private SaveTokenInLocalStorage(response: Observable<AuthResponse>) {
        response.subscribe({
            next: authResponse => {
                localStorage.setItem(this.tokenName, authResponse.token);
            },
            error: error => {
                throw error;
            }
        });
    }
}
