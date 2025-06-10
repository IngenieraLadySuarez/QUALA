import { Component, OnInit } from '@angular/core';
import { Sucursal } from './models/sucursal.model';
import { MonedaDto } from './models/moneda-dto.model';
import { SucursalService } from './services/sucursal.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  sucursales: Sucursal[] = [];
  monedas: MonedaDto[] = [];
  nuevaSucursal: Sucursal = this.getSucursalVacia();
  modoEdicion: boolean = false;
  hoy: string = new Date().toISOString().split('T')[0]; // YYYY-MM-DD

  constructor(private sucursalService: SucursalService) {}

  ngOnInit(): void {
    this.cargarSucursales();
    this.cargarMonedas();
  }

  getSucursalVacia(): Sucursal {
    return {
      codigo: 0,
      descripcion: '',
      direccion: '',
      identificacion: '',
      fechaCreacion: '',
      monedaId: 0,
    };
  }

  cargarSucursales(): void {
    this.sucursalService.getAll().subscribe((data: Sucursal[]) => {
      this.sucursales = data.sort((a, b) => a.codigo - b.codigo);
    });
  }

  cargarMonedas(): void {
    this.sucursalService.getAllMonedas().subscribe((data: MonedaDto[]) => {
      this.monedas = data;
    });
  }

  crearSucursal(): void {
    this.sucursalService.create(this.nuevaSucursal).subscribe(() => {
      this.cargarSucursales();
      this.limpiarFormulario();
    });
  }

  actualizarSucursal(): void {
    this.sucursalService.update(this.nuevaSucursal).subscribe(() => {
      this.cargarSucursales();
      this.limpiarFormulario();
    });
  }

  editarSucursal(sucursal: Sucursal): void {
    this.nuevaSucursal = { ...sucursal };
    this.modoEdicion = true;
  }

  eliminarSucursal(codigo: number): void {
    this.sucursalService.delete(codigo).subscribe(() => {
      this.cargarSucursales();
    });
  }

  limpiarFormulario(): void {
    this.nuevaSucursal = this.getSucursalVacia();
    this.modoEdicion = false;
  }

  getMonedaNombre(monedaId: number): string {
    const moneda = this.monedas.find(m => m.id === monedaId);
    return moneda ? moneda.nombre : 'Desconocida';
  }
}
