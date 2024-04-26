import { formatDate } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CatalogoDirectorInsertRequest, EditarCatalogoDirectorRequest } from '../../../model/Catalogos/CatalogosDirector';

@Component({
    selector: 'app-modulo-formulario-edicion-director',
    templateUrl: './modulo-formulario-edicion-director.component.html',
    styleUrls: ['./modulo-formulario-edicion-director.component.css']
})
/** modulo-formulario-edicion-Director component*/
export class ModuloFormularioEdicionDirectorComponent implements OnInit {
    /** modulo-formulario-edicion-Director ctor */
  @Output()
  ActualizarCatalogo: EventEmitter<CatalogoDirectorInsertRequest[]> =
    new EventEmitter<CatalogoDirectorInsertRequest[]>();
  @Input()
  catalogoEditar: EditarCatalogoDirectorRequest;

  formGroup: FormGroup;
  constructor(public activeModal: NgbActiveModal, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.formGroup = this.fb.group({
      director_id: ["", Validators.required],
      director_titulo: ["", Validators.required],
      director_nombre: ["", Validators.required],
      director_apaterno: ["", Validators.required],
      director_amaterno: [""],
      director_cargo: ["", Validators.required],
    });

    if (this.catalogoEditar != undefined) 
      this.formGroup.patchValue(this.catalogoEditar);
  }

  OnSubmit = (): void => this.ActualizarCatalogo.emit(this.formGroup.value);

}
