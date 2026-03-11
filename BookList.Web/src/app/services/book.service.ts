import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BookResponse } from '../models/book-response';
import { BookRequest } from '../models/book-request';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})

export class BookService {
  private apiUrl = environment.apiUrl + '/api/book';
  private http = inject(HttpClient);

  getBook(id: number): Observable<BookResponse> {
    return this.http.get<BookResponse>(this.apiUrl + '/' + id, { withCredentials: true });
  }

  getAll(): Observable<BookResponse[]> {
    return this.http.get<BookResponse[]>(this.apiUrl + '/', { withCredentials: true });
  }

  addBook(payload: BookRequest): Observable<BookResponse> {
    return this.http.post<BookResponse>(this.apiUrl + '/add', payload, { withCredentials: true });
  }

  updateBook(id: number, payload: BookRequest): Observable<BookResponse> {
    return this.http.put<BookResponse>(this.apiUrl + '/update/' + id, payload, { withCredentials: true });
  }

  deleteBook(id: number): Observable<BookResponse[]> {
    return this.http.delete<BookResponse[]>(this.apiUrl + '/delete/' + id, { withCredentials: true });
  }
}
