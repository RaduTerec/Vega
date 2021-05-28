import { Component, OnInit } from '@angular/core';
import { MakeService } from './../services/make.service';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {

  makes;
  models;
  vehicle : any = {};

  constructor(private makeservice: MakeService) { }

  ngOnInit(): void {
    this.makeservice.getMakes()
      .subscribe(makes => this.makes = makes);    
  }

  onMakeChange(): void{
    var selectedMake = this.makes.find(m => m.id == this.vehicle.make);
    this.models = selectedMake ? selectedMake.models : [];
  }
}
