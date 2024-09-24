﻿import {HttpClient} from '@angular/common/http';
import {Tokens} from "../Models/Tokens";
import {AppCookieService} from "./appCookie.service";
import {CookiesName} from "../Consts/CookiesName";
import {ApiConfig} from "../Consts/ApiConfig";
import {Observable, tap, throwError} from "rxjs";

export class AuthService{
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
    return this._httpClient.post<Tokens>(`${ApiConfig.BaseUrl}/login`, payload).pipe(
      tap(tokens => {
        this.SaveTokens(tokens)
      })
    )
  }

  // public RefreshAuthToken():Observable<Tokens>{
  //   if (this.Tokens === null) {
  //     return throwError(new Error('Tokens are null'));
  //   }
  //   const refreshTokenRequest: RefreshTokenRequest = {
  //     refresh_token: this.RefreshToken
  //   };
  //
  //   return this._httpClient.post<TokenResponse>(`${this._baseUrl}/refresh`, refreshTokenRequest)
  //     .pipe(
  //       tap(tokenResponse => { this.SaveTokens(tokenResponse) }),
  //       catchError(error => {
  //         this.Logout()
  //         return throwError(error)
  //       })
  //     )
  //}

  private SaveTokens(tokens: Tokens){
    this.Tokens = tokens
    this._appCookieService.Save<Tokens>(CookiesName.Tokens, this.Tokens)
  }
}
