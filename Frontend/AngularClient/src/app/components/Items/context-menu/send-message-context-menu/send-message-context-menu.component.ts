import { Component } from '@angular/core';
import {ContextMenuBaseComponent} from "../context-menu-base/context-menu-base.component";
import {NgForOf, NgIf} from "@angular/common";
import {MessageTypes} from "./message-types";

@Component({
  selector: 'app-send-message-context-menu',
  standalone: true,
  imports: [
    NgForOf,
    NgIf,
  ],
  templateUrl: './send-message-context-menu.component.html',
  styleUrl: './send-message-context-menu.component.css'
})
export class SendMessageContextMenuComponent extends ContextMenuBaseComponent{
  private _messageType: MessageTypes = MessageTypes.simple;

  public get MessageType(): MessageTypes {
    return this._messageType;
  }

  public set MessageType(value: MessageTypes) {
    this._messageType = value;
  }

  protected readonly MessageTypes = MessageTypes;
}
