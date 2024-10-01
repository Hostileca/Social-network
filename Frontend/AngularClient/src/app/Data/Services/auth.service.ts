import { Injectable } from '@angular/core';
import { Tokens } from '../Models/Tokens/Tokens';
import {CookiesName} from "../Consts/CookiesName";
import {HttpClient} from "@angular/common/http";
import {AppCookieService} from "./app-cookie.service";
import {catchError, Observable, tap, throwError} from "rxjs";
import {ApiConfig} from "../Consts/ApiConfig";
import {User} from "../Models/User/User";
import {TokenRefresh} from "../Models/Tokens/Token-refresh";
import {UserLogin} from "../Models/User/User-login";
import {UserRegister} from "../Models/User/User-register";

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

  public Login(userLogin: UserLogin): Observable<any>{
    return this._httpClient.post<Tokens>(`${ApiConfig.BaseHttpsUrl}/users/login`, userLogin).pipe(
      tap(tokens => {
        this.SaveTokens(tokens)
      })
    )
  }

  public Register(userRegister: UserRegister){
    return this._httpClient.post<User>(`${ApiConfig.BaseHttpsUrl}/users/register`, userRegister)
  }

  public RefreshAuthToken():Observable<Tokens>{
    if (this.Tokens === null) {
      return throwError(new Error('Tokens are null'));
    }
    const tokenRefreshRequest: TokenRefresh = {
      refreshToken: this.Tokens.refreshToken.value
    };

    return this._httpClient.post<Tokens>(`${ApiConfig.BaseHttpsUrl}/tokens/refresh`, tokenRefreshRequest)
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
