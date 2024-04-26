import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Validators, FormBuilder, FormArray } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CatalogoAvisoAperturaInsertRequest } from 'src/app/model/Catalogos/CatalogosAvisoApertura';

@Component({
  selector: 'app-modulo-formulario-creacion-aviso-apertura',
  templateUrl: './modulo-formulario-creacion-aviso-apertura.component.html',
  styleUrls: ['./modulo-formulario-creacion-aviso-apertura.component.css']
})
export class ModuloFormularioCreacionAvisoAperturaComponent implements OnInit {

  @Output()
  registrarCatalogo: EventEmitter<CatalogoAvisoAperturaInsertRequest[]> =
    new EventEmitter<CatalogoAvisoAperturaInsertRequest[]>();

    form=this.fb.group({
      nombre: [
        "",
        {
          validators: [
            Validators.required,
            Validators.minLength(5),
            Validators.maxLength(60),
          ],
          // asyncValidators: [courseTitleValidator(this.catalogos)],
          updateOn: "blur",
        },
      ],
      // descripcion: ["", Validators.required],
      catalogos: this.fb.array([]),
    });
  constructor( public activeModal: NgbActiveModal,
    private fb: FormBuilder,) { }

  ngOnInit(): void {
    
  }

  
  get catalogos() {
    return this.form.controls["catalogos"] as FormArray;
  }

  public remover(index: number): void {
    this.catalogos.removeAt(index);
  }

  public agregar(): void {
    const catalogo = this.fb.group({
      nombre: ["", Validators.required],
      // descripcion: ["", Validators.required],
    });

    this.catalogos.push(catalogo);
  }


  OnSubmit() {

    this.registrarCatalogo.emit(this.catalogos.value);
  }


}
