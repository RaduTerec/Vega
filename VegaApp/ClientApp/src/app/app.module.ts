import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ErrorHandler } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { JwtModule } from "@auth0/angular-jwt";

import { AppErrorHandler } from './app.error-handler';
import { VehicleService } from './services/vehicle.service';
import { PhotoService } from './services/photo.service';
import { AuthenticationService } from './services/auth.service';
import { EditVehicleGuard } from './services/editVehicle-guards.service';
import { NewVehicleGuard } from './services/newVehicle-guard.service';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { PaginationComponent } from './shared/pagination.component';
import { VehicleFormComponent } from './vehicle-form/vehicle-form.component';
import { VehicleListComponent } from './vehicle-list/vehicle-list.component';
import { ViewVehicleComponent } from './view-vehicle/view-vehicle.component';
import { UserComponent } from './user/user.component';

export function tokenGetter() {
  return localStorage.getItem("token");
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    PaginationComponent,
    VehicleFormComponent,
    VehicleListComponent,
    ViewVehicleComponent,
    UserComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter        
      },
    }),
    FormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: VehicleListComponent, pathMatch: 'full' },
      { path: 'vehicles/new', component: VehicleFormComponent, canActivate: [NewVehicleGuard] },
      { path: 'vehicles/edit/:id', component: VehicleFormComponent, canActivate: [EditVehicleGuard] },
      { path: 'vehicles/:id', component: ViewVehicleComponent },
      { path: 'user', component: UserComponent},
    ]),
    NgbModule,
  ],
  providers: [
    { provide: ErrorHandler, useClass: AppErrorHandler },
    VehicleService,    
    PhotoService,
    AuthenticationService,
    EditVehicleGuard,
    NewVehicleGuard,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
