import {Reaction} from "../Reaction/Reaction";

export interface Message{
  id: string
  senderId: string
  chatId: string
  date: Date
  text: string
  reactions: Reaction[]
}
