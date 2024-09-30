import {ChatMember} from "../ChatMember/Chat-member";

export interface Chat{
  id: string;
  name: string;
  chatMembers: ChatMember[]
}
