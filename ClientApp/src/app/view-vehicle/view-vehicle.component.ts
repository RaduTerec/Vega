import { HttpEventType } from '@angular/common/http';
import { Component, ElementRef, NgZone, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { PhotoService } from '../services/photo.service';
import { VehicleService } from '../services/vehicle.service';

@Component({
  selector: 'app-view-vehicle',
  templateUrl: './view-vehicle.component.html',
  styleUrls: ['./view-vehicle.component.css']
})
export class ViewVehicleComponent implements OnInit {
  @ViewChild('fileInput', { static: false }) fileInput: ElementRef;
  vehicle: any;
  vehicleId: number;
  photos: any;
  uploadProgress: number;
  active = 1;
  uploadSub: Subscription;

  constructor(
    private zone: NgZone,
    private route: ActivatedRoute,
    private router: Router,
    private toastrService: ToastrService,
    private vehicleService: VehicleService,
    private photoService: PhotoService) {
    route.params.subscribe(p => {
      this.vehicleId = +p['id'];
      if (isNaN(this.vehicleId) || this.vehicleId <= 0) {
        router.navigate(['/']);
        return;
      }
    });
  }

  ngOnInit() {
    this.vehicleService.getVehicle(this.vehicleId)
      .subscribe(
        v => this.vehicle = v,
        err => {
          if (err.status == 404) {
            this.router.navigate(['/']);
            return;
          }
        });

    this.photoService.getPhotos(this.vehicleId)
      .subscribe(photos => this.photos = photos);
  }

  delete() {
    if (confirm("Are you sure?")) {
      this.vehicleService.delete(this.vehicle.id)
        .subscribe(x => {
          this.router.navigate(['/']);
        });
    }
  }

  uploadPhoto() {
    var nativeElement: HTMLInputElement = this.fileInput.nativeElement;

    var upload = this.photoService.upload(this.vehicleId, nativeElement.files[0]);

    this.uploadSub = upload.subscribe(resp => {
      this.zone.run(() => {
        if (resp.type === HttpEventType.UploadProgress) {
          var percentDone = Math.round(100 * resp.loaded / resp.total);
          this.uploadProgress = percentDone;
        }
      });
    })
  }

  cancelUpload() {
    this.uploadSub.unsubscribe();
    this.uploadProgress = null;
    this.uploadSub = null;
  }
}
