import { HttpInterceptorFn } from '@angular/common/http';
import { finalize } from 'rxjs/operators';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { inject } from '@angular/core';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
    let totalRequests = 0;
    const loaderService = inject(NgxUiLoaderService);

    if(totalRequests == 0) {
        loaderService.start();
      }
  
      totalRequests++;
  
      return next(req).pipe(
        finalize(() => {
          totalRequests--;
          if (totalRequests === 0) {
            loaderService.stop();
          }
        })
      );
}