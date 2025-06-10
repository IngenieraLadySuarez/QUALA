import { Pipe, PipeTransform } from '@angular/core';
import { Sucursal } from '../models/sucursal.model';

@Pipe({
  name: 'orderByCodigo'
})
export class OrderByCodigoPipe implements PipeTransform {
  transform(value: Sucursal[]): Sucursal[] {
    return value.sort((a, b) => a.codigo - b.codigo);
  }
}
