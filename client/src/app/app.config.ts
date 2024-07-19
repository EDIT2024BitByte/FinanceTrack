import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { routes } from './shared/router/router.config';
import { loadingInterceptor } from './shared/services/loading.interceptor';
import { provideNativeDateAdapter } from '@angular/material/core';
import { provideAnimations } from '@angular/platform-browser/animations';
import { SidebarNavHelper } from '@coreui/angular';
import { ngxUiLoaderConfig } from './shared/config/ngx-loader-config';
import { NgxUiLoaderModule } from 'ngx-ui-loader';
import { provideToastr } from 'ngx-toastr';
import { MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';

export const appConfig: ApplicationConfig = {
  providers: [
    provideClientHydration(), 
    provideHttpClient(withFetch(), withInterceptors([loadingInterceptor])),
    importProvidersFrom(BrowserModule, NgxUiLoaderModule.forRoot(ngxUiLoaderConfig)),
    provideRouter(routes),
    provideAnimations(),
    SidebarNavHelper,
    provideNativeDateAdapter(),
    provideToastr({
      closeButton: true,
      positionClass: 'toast-bottom-right',
      timeOut: 3000,
      preventDuplicates: true
    }),
   { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true } }
  ]
};