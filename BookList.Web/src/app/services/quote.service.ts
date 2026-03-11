import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { QuoteRequest } from '../models/quote-request';
import { QuoteResponse } from '../models/quote-response';
import { environment } from '../../environments/environment';

interface UserQuotesResponse {
  userQuotes: QuoteResponse[]
}

@Injectable({
  providedIn: 'root',
})

export class QuoteService {
  private apiUrl = environment.apiUrl + '/api/quote';
  private http = inject(HttpClient);

  addQuote(payload: QuoteRequest): Observable<QuoteResponse> {
    return this.http.post<QuoteResponse>(this.apiUrl + '/add', payload, { withCredentials: true });
  }

  deleteQuote(id: number): Observable<QuoteResponse> {
    return this.http.delete<QuoteResponse>(this.apiUrl + '/delete/' + id, { withCredentials: true });
  }

  updateQuote(id: number, payload: QuoteRequest): Observable<QuoteResponse> {
    return this.http.put<QuoteResponse>(this.apiUrl + '/update/' + id, payload, { withCredentials: true });
  }

  getQuote(id: number): Observable<QuoteResponse> {
    return this.http.get<QuoteResponse>(this.apiUrl + '/' + id, { withCredentials: true });
  }

  getUserQuotes(): Observable<UserQuotesResponse> {
    return this.http.get<UserQuotesResponse>(this.apiUrl + '/user', { withCredentials: true });
  }
}
