import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Lead } from '../../Models/Lead';  // Assuming you have a Lead model
import { StoreService } from '../store/store.service';
import {environment} from "../../../../environmenets/environment";

@Injectable({
  providedIn: 'root'
})
export class LeadService {
  private url: string = `${environment.API_BASE_URL_LEADS}api/leads`;
  private token: string | null = localStorage.getItem('token');

  constructor(private http: HttpClient, private store: StoreService) {}

  private getHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.token}`
    });
  }

  getAllLeads(): Observable<Lead[]> {
    return this.http.get<Lead[]>(this.url, { headers: this.getHeaders() })
      .pipe(catchError(this.handleError));
  }

  getLeadById(id: string): Observable<Lead> {
    return this.http.get<Lead>(`${this.url}/${id}`, { headers: this.getHeaders() })
      .pipe(catchError(this.handleError));
  }

  createLead(lead: Lead): Observable<Lead> {
    return this.http.post<Lead>(this.url, lead, { headers: this.getHeaders() })
      .pipe(catchError(this.handleError));
  }

  updateLead(id: string, lead: Lead): Observable<Lead> {
    return this.http.put<Lead>(`${this.url}/${id}`, lead, { headers: this.getHeaders() })
      .pipe(catchError(this.handleError));
  }

  deleteLead(id: string): Observable<any> {
    return this.http.delete<any>(`${this.url}/${id}`, { headers: this.getHeaders() })
      .pipe(catchError(this.handleError));
  }

  assignLead(id: string, collaboratorId: number): Observable<any> {
    return this.http.post<any>(`${this.url}/${id}/assign`, { collaboratorId }, { headers: this.getHeaders() })
      .pipe(catchError(this.handleError));
  }

  startLead(id: string): Observable<any> {
    return this.http.post<any>(`${this.url}/${id}/start`, {}, { headers: this.getHeaders() })
      .pipe(catchError(this.handleError));
  }

  convertLeadToWon(id: string): Observable<any> {
    return this.http.post<any>(`${this.url}/${id}/convert/won`, {}, { headers: this.getHeaders() })
      .pipe(catchError(this.handleError));
  }

  markLeadAsLost(id: string): Observable<any> {
    return this.http.post<any>(`${this.url}/${id}/convert/lost`, {}, { headers: this.getHeaders() })
      .pipe(catchError(this.handleError));
  }

  createEventFromLead(id: string, eventDetails: string): Observable<any> {
    return this.http.post<any>(`${this.url}/${id}/event`, { eventDetails }, { headers: this.getHeaders() })
      .pipe(catchError(this.handleError));
  }

  private handleError(error: any) {
    console.error('An error occurred:', error);
    return throwError(() => new Error('Something went wrong; please try again later.'));
  }
}
