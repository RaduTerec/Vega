import { AuthService } from './auth.service';
import { Injectable } from '@angular/core';
import { CanActivate } from "@angular/router";
import { Router } from '@angular/router';

@Injectable()
export class EditVehicleGuard implements CanActivate {

    constructor(private router: Router,
        private auth: AuthService) { }

    canActivate() {
        if (this.auth.authenticated() && 
        (this.auth.isInRole("Moderator") || this.auth.isInRole("Admin") )) {
            return true;
        }

        this.router.navigate(["/user"]);
        return false;
    }
}