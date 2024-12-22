import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EMPTY, Observable, catchError, map } from 'rxjs';
import {
  LoginRequest,
  RefreshRequest,
} from '../../core/services/ElectronicsAppClient';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  apiBaseUrl: string = 'https://localhost:7195';

  constructor(private httpClient: HttpClient) {}

  //Logs in the user and saves tokens to localStorage
  login(credential: LoginRequest): Observable<any> {
    return this.httpClient
      .post<any>(`${this.apiBaseUrl}/login`, credential)
      .pipe(
        map((response) => {
          this.storeAuthData(response);
          return response;
        }),
        catchError((err) => {
          let error = err as HttpErrorResponse;
          return EMPTY;
        })
      );
  }

  // Refreshes the access token using the refresh token
  refreshToken(): Observable<any> {
    const refreshToken = this.getRefreshToken();
    const refreshRequest = new RefreshRequest();
    refreshRequest.refreshToken = refreshToken as string;

    return this.httpClient
      .post<any>(`${this.apiBaseUrl}/refresh`, refreshRequest)
      .pipe(
        map((response) => {
          this.storeAuthData(response);
          return response;
        }),
        catchError((err) => {
          console.error('Token refresh error:', err);
          return EMPTY;
        })
      );
  }

  // Checks if the access token has expired
  isAccessTokenExpired(): boolean {
    const expirationTime = localStorage.getItem('EXPIRATION_TIME');
    if (!expirationTime) return true;

    const now = new Date().getTime();
    return now > parseInt(expirationTime, 10);
  }

  // Logs out the user by clearing localStorage
  logout(): void {
    this.ClearUserDataFromLocalStorage();
  }

  // Checks if the user is logged in
  isLoggedIn(): boolean {
    const accessToken = this.getAccessToken();
    const refreshToken = this.getRefreshToken();
    return !!accessToken && !!refreshToken;
  }

  // Retrieves the access token
  getAccessToken(): string | null {
    var localToken = localStorage.getItem('ACCESS_TOKEN');
    return localToken;
  }

  getRefreshToken(): string | null {
    var sessionToken = sessionStorage.getItem('REFRESH_TOKEN');
    var localToken = localStorage.getItem('REFRESH_TOKEN');
    return sessionToken || localToken;
  }

  // Stores tokens and expiration time in localStorage
  private storeAuthData(response: any): void {
    localStorage.setItem('ACCESS_TOKEN', response.accessToken);
    localStorage.setItem('REFRESH_TOKEN', response.refreshToken);

    const expirationTime = new Date().getTime() + response.expiresIn * 1000;
    localStorage.setItem('EXPIRATION_TIME', expirationTime.toString());
  }
  private ClearUserDataFromLocalStorage(): void {
    localStorage.removeItem('ACCESS_TOKEN');
    localStorage.removeItem('REFRESH_TOKEN');
    localStorage.removeItem('EXPIRATION_TIME');
  }
}
