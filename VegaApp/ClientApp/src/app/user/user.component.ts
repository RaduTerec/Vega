import { Login } from './../models/login';
import { User } from './../models/user';
import { AuthService } from './../services/auth.service';
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
    private authService: AuthService,
    private toastrService: ToastrService) { }

  ngOnInit() {
  }

  register() {
    var result = this.authService.register(this.userData);
    this.handleAuthResponse(result, "Register");
  }

  login() {
    var result = this.authService.login(this.loginData);
    this.handleAuthResponse(result, "Login");
  }

  private handleAuthResponse(result, dialogName: string) {
    result.subscribe(() => {
      this.router.navigate(["/"]);
    }, error => {
      if (!error.error) {
        error.error = dialogName + "failed";
      }

      this.toastrService.error(error.error + " Please try again.", dialogName, {
        timeOut: 5000
      });
    });
  }

}
