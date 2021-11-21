import { ToastrService } from "ngx-toastr";
import { ErrorHandler, Inject, NgZone, isDevMode } from "@angular/core";

export class AppErrorHandler implements ErrorHandler {
    constructor(
        private ngZone: NgZone,
        @Inject(ToastrService) private toastrService: ToastrService) {
    }

    handleError(error: any): void {
        // this.ngZone.run(() => {
        //     this.toastrService.error("Unexpected error", "Error " + error.status, {
        //         timeOut: 5000
        //     });
        // });

        // if (!isDevMode()) {
        //     Raven.captureException(error.originalError || error);
        // }
        // else {
        //     throw error;
        // }        
    }
}