import {Message} from "./Message";

export interface DelayedMessage extends Message{
  jobId: string
  dateTimeOffset: Date
}
