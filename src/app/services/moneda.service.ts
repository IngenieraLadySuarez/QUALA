import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MonedaDto } from '../models/moneda-dto.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MonedaService {
  private apiUrl = 'https://localhost:44392/api/Moneda';

  constructor(private http: HttpClient) {}

  getMonedas(): Observable<MonedaDto[]> {
    return this.http.get<MonedaDto[]>(this.apiUrl);
  }
}
