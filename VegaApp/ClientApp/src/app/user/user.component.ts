import { Login } from './../models/login';
import { User } from './../models/user';
import { AuthenticationService } from './../services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  userData: User;
  loginData: Login;

  constructor(private authService: AuthenticationService) { }

  ngOnInit() {
  }

  register() {
    var result = this.authService.register(this.userData);
    console.log(result);
  }

  login() {
    var result = this.authService.login(this.loginData);
    result.subscribe(loginResponse => {
      console.log(loginResponse);
    });
    console.log(result);
  }

}
