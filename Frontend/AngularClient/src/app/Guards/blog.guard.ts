import {CanActivateFn, Router} from '@angular/router';
import {inject} from "@angular/core";
import {CurrentBlogService} from "../Data/Services/current-blog.service";

export const blogGuard: CanActivateFn = (route, state) => {
  const isBlogSelected = inject(CurrentBlogService).IsBlogSelected()

  if(isBlogSelected){
    return true
  }

  return inject(Router).createUrlTree(["/my-blogs"])
};
