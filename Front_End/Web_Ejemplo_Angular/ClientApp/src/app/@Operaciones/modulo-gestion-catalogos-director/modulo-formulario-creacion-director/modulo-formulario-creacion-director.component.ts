import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CatalogoDirectorInsertRequest } from '../../../model/Catalogos/CatalogosDirector';

@Component({
    selector: 'app-modulo-formulario-creacion-director',
    templateUrl: './modulo-formulario-creacion-director.component.html',
    styleUrls: ['./modulo-formulario-creacion-director.component.css']
})

export class ModuloFormularioCreacionDirectorComponent implements OnInit {
  @Output()
  registrarCatalogo: EventEmitter<CatalogoDirectorInsertRequest[]> =
    new EventEmitter<CatalogoDirectorInsertRequest[]>();

  form = this.fb.group({
    director_titulo: ["", Validators.required],
    director_nombre: ["", Validators.required],
    director_apaterno: ["", Validators.required],
    director_amaterno: [""],
    director_cargo: ["", Validators.required],
    catalogos: this.fb.array([]),
  });

  constructor(public activeModal: NgbActiveModal,private fb: FormBuilder) { }

  ngOnInit(): void {
    this.agregar();
  }

  get catalogos() {
    return this.form.controls["catalogos"] as FormArray;
  }

  public remover = (index: number): void  => this.catalogos.removeAt(index);
  

  public agregar(): void {
    const catalogo = this.fb.group({
      director_titulo: ["", Validators.required],
      director_nombre: ["", Validators.required],
      director_apaterno: ["", Validators.required],
      director_amaterno: [""],
      director_cargo: ["", Validators.required],
    });

    this.catalogos.push(catalogo);
  }

  OnSubmit = (): void => this.registrarCatalogo.emit(this.catalogos.value);
  
}
