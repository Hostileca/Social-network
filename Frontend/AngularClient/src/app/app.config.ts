import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import {CookieService} from "ngx-cookie-service";
import {provideHttpClient} from "@angular/common/http";
import {AuthService} from "./Data/Services/auth.service";

export const appConfig: ApplicationConfig = {
  providers: [
      provideZoneChangeDetection({ eventCoalescing: true }),
      provideRouter(routes),
      provideHttpClient(),
      [CookieService]
  ]
};
