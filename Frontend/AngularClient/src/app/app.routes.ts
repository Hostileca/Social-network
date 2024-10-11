import { Routes } from '@angular/router';
import {SignInComponent} from "./components/pages/sign-in/sign-in.component";
import {SignUpComponent} from "./components/pages/sign-up/sign-up.component";
import {MyBlogsComponent} from "./components/pages/my-blogs/my-blogs.component";
import {authGuard} from "./Guards/auth.guard";
import {CreateBlogComponent} from "./components/pages/create-blog/create-blog.component";
import {MyChatsComponent} from "./components/pages/my-chats/my-chats.component";
import {blogGuard} from "./Guards/blog.guard";
import {BlogsComponent} from "./components/pages/blogs/blogs.component";
import {BlogComponent} from "./components/pages/blog/blog.component";
import {CreateChatComponent} from "./components/pages/create-chat/create-chat.component";
import {CreatePostComponent} from "./components/pages/create-post/create-post.component";
import {WallComponent} from "./components/pages/wall/wall.component";
import {BlogEditComponent} from "./components/pages/blog-edit/blog-edit.component";

export const routes: Routes = [
    { path: "sign-in", component: SignInComponent, canActivate: [] },
    { path: "sign-up", component: SignUpComponent, canActivate: [] },
    { path: "my-blogs", component: MyBlogsComponent, canActivate: [authGuard] },
    { path: "my-blogs/create", component: CreateBlogComponent, canActivate: [authGuard] },
    { path: "wall", component: WallComponent, canActivate: [authGuard, blogGuard] },
    { path: "my-chats", component: MyChatsComponent, canActivate: [authGuard, blogGuard] },
    { path: "my-chats/create", component: CreateChatComponent, canActivate: [authGuard, blogGuard] },
    //{ path: "my-chats/:chatId", component: ChatDetailsComponent, canActivate: [authGuard, blogGuard] },
    { path: "blogs", component: BlogsComponent, canActivate: [authGuard, blogGuard] },
    { path: "blogs/:blogId", component: BlogComponent, canActivate: [authGuard, blogGuard] },
    { path: "blogs/:blogId/edit", component: BlogEditComponent, canActivate: [authGuard, blogGuard] },
    { path: "blogs/:blogId/posts/create", component: CreatePostComponent, canActivate: [authGuard, blogGuard] },
    { path: "**", redirectTo: "wall" }
];
