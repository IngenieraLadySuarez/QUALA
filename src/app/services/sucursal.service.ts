import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Sucursal } from '../models/sucursal.model';
import { MonedaDto } from '../models/moneda-dto.model';

@Injectable({ providedIn: 'root' })
export class SucursalService {
  private apiUrl = 'https://localhost:44392/api';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Sucursal[]> {
    return this.http.get<Sucursal[]>(`${this.apiUrl}/Sucursal`);
  }

  create(sucursal: Sucursal): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/Sucursal`, sucursal);
  }

  update(sucursal: Sucursal): Observable<void> {
  return this.http.put<void>(`${this.apiUrl}/Sucursal/${sucursal.codigo}`, sucursal);
  }

  delete(codigo: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Sucursal/${codigo}`);
  }

  getAllMonedas(): Observable<MonedaDto[]> {
    return this.http.get<MonedaDto[]>(`${this.apiUrl}/Moneda`);
  }
}