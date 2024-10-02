import { Routes } from '@angular/router';
import {SignInComponent} from "./components/pages/sign-in/sign-in.component";
import {SignUpComponent} from "./components/pages/sign-up/sign-up.component";
import {MyBlogsComponent} from "./components/pages/my-blogs/my-blogs.component";
import {authGuard} from "./Guards/auth.guard";
import {CreateBlogComponent} from "./components/pages/create-blog/create-blog.component";
import {MyChatsComponent} from "./components/pages/my-chats/my-chats.component";
import {blogGuard} from "./Guards/blog.guard";
import {ChatComponent} from "./components/pages/chat/chat.component";
import {BlogsComponent} from "./components/pages/blogs/blogs.component";
import {BlogComponent} from "./components/pages/blog/blog.component";

export const routes: Routes = [
    { path: "sign-in", component: SignInComponent },
    { path: "sign-up", component: SignUpComponent },
    { path: "my-blogs", component: MyBlogsComponent, canActivate: [authGuard] },
    { path: "create-blog", component: CreateBlogComponent, canActivate: [authGuard] },
    { path: "my-chats", component: MyChatsComponent, canActivate: [authGuard, blogGuard] },
    { path: "my-chats/:id", component: ChatComponent, canActivate: [authGuard, blogGuard] },
    { path: "blogs", component: BlogsComponent, canActivate: [authGuard, blogGuard] },
    { path: "blogs/:id", component: BlogComponent, canActivate: [authGuard, blogGuard] },
    { path: "**", redirectTo: "my-chats" }
];
