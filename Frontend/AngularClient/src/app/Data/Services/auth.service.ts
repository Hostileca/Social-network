import { Injectable } from '@angular/core';
import { Tokens } from '../Models/Responses/Tokens';
import {CookiesName} from "../Consts/CookiesName";
import {HttpClient} from "@angular/common/http";
import {AppCookieService} from "./app-cookie.service";
import {catchError, Observable, tap, throwError} from "rxjs";
import {ApiConfig} from "../Consts/ApiConfig";
import {User} from "../Models/Responses/User";
import {TokenRefreshRequest} from "../Models/Requests/TokenRefreshRequest";
import {Blog} from "../Models/Responses/Blog";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public Tokens: Tokens | null = null

  public IsAuth(): boolean{
    if(!this.Tokens){
      this.Tokens = this._appCookieService.Get<Tokens>(CookiesName.Tokens)
    }
    return !!this.Tokens
  }

  constructor(
    private readonly _httpClient: HttpClient,
    private readonly _appCookieService: AppCookieService) {
  }

  public Login(payload: {email: string, password: string}): Observable<any>{
    return this._httpClient.post<Tokens>(`${ApiConfig.BaseUrl}/users/login`, payload).pipe(
      tap(tokens => {
        this.SaveTokens(tokens)
      })
    )
  }

  public Register(payload: {username: string, email: string, password: string, confirmPassword: string}){
    return this._httpClient.post<User>(`${ApiConfig.BaseUrl}/users/register`, payload)
  }

  public RefreshAuthToken():Observable<Tokens>{
    if (this.Tokens === null) {
      return throwError(new Error('Tokens are null'));
    }
    const tokenRefreshRequest: TokenRefreshRequest = {
      refreshToken: this.Tokens.refreshToken.value
    };

    return this._httpClient.post<Tokens>(`${ApiConfig.BaseUrl}/token/refresh`, tokenRefreshRequest)
      .pipe(
        tap(tokenResponse => { this.SaveTokens(tokenResponse) }),
        catchError(error => {
          this.Logout()
          return throwError(error)
        })
      )
  }

  private SaveTokens(tokens: Tokens){
    this.Tokens = tokens
    this._appCookieService.Save<Tokens>(CookiesName.Tokens, this.Tokens)
  }

  public Logout(){
    this.Tokens = null
    this._appCookieService.Delete(CookiesName.Tokens)
  }
}
