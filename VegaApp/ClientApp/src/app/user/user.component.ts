import { Login } from './../models/login';
import { User } from './../models/user';
import { AuthenticationService } from './../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  userData: User = { name: "", username: "", email: "", password: "" };
  loginData: Login = { email: "", password: "" };

  constructor(private router: Router,
    private authService: AuthenticationService,
    private toastrService: ToastrService) { }

  ngOnInit() {
  }

  register() {
    var result = this.authService.register(this.userData);
    console.log(result);
  }

  login() {
    var result = this.authService.login(this.loginData);

    result.subscribe(success => {
      this.router.navigate(["/"]);
    }, err => {
      this.toastrService.error("Login failed. Please try again", "Login", {
        timeOut: 5000
      });
    });
  }

}
