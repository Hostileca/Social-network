import { Routes } from '@angular/router';
import {SignInComponent} from "./components/pages/sign-in/sign-in.component";
import {SignUpComponent} from "./components/pages/sign-up/sign-up.component";

export const routes: Routes = [
    { path: "sign-in", component: SignInComponent },
    { path: "sign-up", component: SignUpComponent }
];
