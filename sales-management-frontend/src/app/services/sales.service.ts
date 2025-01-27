import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class SalesService {

  constructor(private http: HttpClient) {}

  getSales(): Observable<any> {
    return this.http.get(environment.apiUrl);
  }

  getSaleById(saleId: string): Observable<any> {
    return this.http.get(`${environment.apiUrl}/${saleId}`);
  }

  createSale(sale: any): Observable<any> {
    return this.http.post(environment.apiUrl, sale);
  }

  updateSale(saleId: string, sale: any): Observable<any> {
    return this.http.put(`${environment.apiUrl}/${saleId}`, sale);
  }

  deleteSale(saleId: string): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/${saleId}`);
  }
}
