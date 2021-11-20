import { AuthenticationService } from './auth.service';
import { Injectable } from '@angular/core';
import { CanActivate } from "@angular/router";
import { Router } from '@angular/router';

@Injectable()
export class NewVehicleGuard implements CanActivate {

    constructor(private router: Router,
        private auth: AuthenticationService) { }

    canActivate() {
        if (this.auth.authenticated() && this.auth.isInRole("Admin")) {
            return true;
        }

        this.router.navigate(["/"]);
        return false;
    }
}