import * as Raven from 'raven-js'; 
import { ToastrService } from "ngx-toastr";
import { ErrorHandler, Inject, NgZone } from "@angular/core";

export class AppErrorHandler implements ErrorHandler {
    constructor(
        private ngZone: NgZone,
        @Inject(ToastrService) private toastrService: ToastrService) {
    }

    handleError(error: any): void {
        Raven.captureException(error.originalError || error);
        
        this.ngZone.run(() => {
            this.toastrService.error("Unexpected error", "Error " + error.status, {
                timeOut: 5000
            });
        });
    }
}