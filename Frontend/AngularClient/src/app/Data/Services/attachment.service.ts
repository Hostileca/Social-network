import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {map, Observable} from "rxjs";
import {ApiConfig} from "../Consts/ApiConfig";
import {HttpHelper} from "../../Helpers/http-helper";
import {Attachment} from "../Models/Attachment/Attachment";

@Injectable({
  providedIn: 'root'
})
export class AttachmentService {

  constructor(private readonly _httpClient: HttpClient) { }

  public GetPostAttachment(attachmentId: string): Observable<Blob> {
    return this._httpClient.get(`${ApiConfig.BaseUrl}/attachments/${attachmentId}`,
      {responseType: 'blob'})
  }

  public GetMessageAttachment(chatId: string, messageId: string, attachmentId: string,
                              userBlogId: string): Observable<Attachment>{
    let params = new HttpParams()
    params = HttpHelper.AddUserBlogIdToQuery(params, userBlogId)
    return this._httpClient.get(`${ApiConfig.BaseUrl}/chats/${chatId}/messages/${messageId}/attachments/${attachmentId}`,
      {responseType: 'blob', observe: 'response', params}).pipe(
          map(response => {
            const blob = response.body;
            console.log(response.headers)
            const contentDisposition = response.headers.get('content-disposition');
            console.log(contentDisposition)
            let filename = 'unknown_file';
            if (contentDisposition) {
              const matches = /filename="([^"]+)"/.exec(contentDisposition);
              if (matches != null && matches[1]) {
                filename = matches[1];
              }
            }

            const fileType = response.headers.get('Content-Type') || 'application/octet-stream';

            if(!blob){
              throw Error('file is null')
            }

            const blobUrl = window.URL.createObjectURL(blob)
            const attachment = {
              blobUrl: blobUrl,
              filename: filename,
              fileType: fileType
            }
            return attachment;
          })
    )
  }
}
