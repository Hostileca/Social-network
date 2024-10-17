import {SendMessage} from "./Send-message";

export interface SendDelayedMessage extends SendMessage {
  date: Date
}
