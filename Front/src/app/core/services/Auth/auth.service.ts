import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import { Injectable } from '@angular/core';
import {catchError, Observable, tap, throwError} from 'rxjs';
import { LoginResponseDto } from '../../Models/login-response-dto';
import { StoreService } from '../store/store.service';
import {environment} from "../../../../environmenets/environment";

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  url: string = environment.API_BASE_URL;
  urlser: string = environment.API_BASE_URL_LEADS;
  token = localStorage.getItem('token');

  constructor(private http: HttpClient, private store: StoreService) {}

  login(userData: any): Observable<any> {
    console.log(userData);
    const url = 'https://localhost:7002/api/auth/Login';
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
      // Add any other headers required by your API
    });

    return this.http.post(url, userData, { headers });
  }
  logout() {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`
    });
    return this.http.post(this.url, { headers }).subscribe({
      next:()=>{
        localStorage.clear();
        console.log(this.store.isLogged());
      }
    });

  }
  register(userData: any): Observable<any> {
    console.log(userData);
    const registerUrl = `${this.url}signup`;
    return this.http.post(registerUrl, userData);
  }
  getUserDto(email: any): Observable<any> {
    const registerUrl = `https://localhost:7002/api/auth/get-user-info/${email}`;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.token}`
      // Add any other headers required by your API
    });
    return this.http.get(registerUrl,{headers});
  }
  init(): Observable<any> {
    const registerUrl = `${this.urlser}initdb`;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.token}`
      // Add any other headers required by your API
    });
    return this.http.get(registerUrl,{headers});
  }



}
