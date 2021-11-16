import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  isLoggedIn: boolean;
  role: string;

  ngOnInit() {
    this.role = this.readLocalStorageValue('role');
    this.isLoggedIn = this.readLocalStorageValue('token') != null;
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("role");
  }

  readLocalStorageValue(key) {
    return localStorage.getItem(key);
  }
}
