import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {ApiConfig} from "../Consts/ApiConfig";

@Injectable({
  providedIn: 'root'
})
export class AttachmentService {

  constructor(private readonly _httpClient: HttpClient) { }

  public GetPostAttachment(attachmentId: string): Observable<Blob> {
    return this._httpClient.get(`${ApiConfig.BaseUrl}/attachments/${attachmentId}`, {responseType: 'blob'})
  }
}
