import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import {CookieService} from "ngx-cookie-service";
import {provideHttpClient, withInterceptors} from "@angular/common/http";
import {AuthService} from "./Data/Services/auth.service";
import {authInterceptor} from "./Interceptors/auth.interceptor";

export const appConfig: ApplicationConfig = {
  providers: [
      provideZoneChangeDetection({ eventCoalescing: true }),
      provideRouter(routes),
      provideHttpClient(withInterceptors([authInterceptor])),
      [CookieService]
  ]
};
