import {Reaction} from "../Reaction/Reaction";

export interface Message{
  id: string
  senderId: string
  date: Date
  text: string
  reactions: Reaction[]
}
