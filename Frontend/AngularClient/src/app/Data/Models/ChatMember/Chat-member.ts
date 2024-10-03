import {Blog} from "../Blog/Blog";

export interface ChatMember{
  id: string;
  blog: Blog
  role: number
  joinDate: Date
}
