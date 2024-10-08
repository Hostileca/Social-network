﻿import {PageSettings} from "../Data/Requests/PageSettings";
import {HttpParams} from "@angular/common/http";

export class HttpHelper{
  static AddPageSettingsToQuery(httpParams: HttpParams, pageSettings: PageSettings) : HttpParams{
    httpParams = httpParams.append('pageNumber', pageSettings.pageNumber.toString())
    httpParams = httpParams.append('pageSize', pageSettings.pageSize.toString())
    return httpParams
  }

  static AddBlogIdToQuery(httpParams: HttpParams, blogId: string){
    httpParams = httpParams.append('blogId', blogId)
    return httpParams
  }
}
