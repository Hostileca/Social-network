import { Routes } from '@angular/router';
import {SignInComponent} from "./components/pages/sign-in/sign-in.component";
import {SignUpComponent} from "./components/pages/sign-up/sign-up.component";
import {MyBlogsComponent} from "./components/pages/my-blogs/my-blogs.component";
import {authGuard} from "./Guards/auth.guard";
import {CreateBlogComponent} from "./components/pages/create-blog/create-blog.component";

export const routes: Routes = [
    { path: "sign-in", component: SignInComponent },
    { path: "sign-up", component: SignUpComponent },
    { path: "my-blogs", component: MyBlogsComponent, canActivate: [authGuard] },
    { path: "blog-create", component: CreateBlogComponent, canActivate: [authGuard] },
];
