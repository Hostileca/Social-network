import {SendMessage} from "./Send-message";

export interface SendDelayedMessage extends SendMessage {
  Date: Date
}
