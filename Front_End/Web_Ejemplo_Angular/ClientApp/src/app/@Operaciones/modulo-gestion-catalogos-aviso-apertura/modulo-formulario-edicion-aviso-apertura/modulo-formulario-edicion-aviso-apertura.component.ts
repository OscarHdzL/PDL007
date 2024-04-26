import { formatDate } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CatalogoAvisoAperturaInsertRequest, EditarCatalogoAvisoAperturaRequest } from 'src/app/model/Catalogos/CatalogosAvisoApertura';

@Component({
  selector: 'app-modulo-formulario-edicion-aviso-apertura',
  templateUrl: './modulo-formulario-edicion-aviso-apertura.component.html',
  styleUrls: ['./modulo-formulario-edicion-aviso-apertura.component.css']
})
export class ModuloFormularioEdicionAvisoAperturaComponent implements OnInit {
    @Output()
    ActualizarCatalogo: EventEmitter<CatalogoAvisoAperturaInsertRequest[]> =
      new EventEmitter<CatalogoAvisoAperturaInsertRequest[]>();
    @Input()
    catalogoEditar: EditarCatalogoAvisoAperturaRequest;
  
    formGroup: FormGroup;
    constructor(public activeModal: NgbActiveModal, private fb: FormBuilder) {}
  
    ngOnInit(): void {
      this.formGroup = this.fb.group({
        c_id: [
          "",
          {
            validators: [
              Validators.required,
            ],
            // asyncValidators: [courseTitleValidator(this.catalogos)],
          },
        ],
        
        c_nombre_n: [
          "",
          {
            validators: [
              Validators.required,
              Validators.minLength(1),
              Validators.maxLength(60),
            ],
            // asyncValidators: [courseTitleValidator(this.catalogos)],
          },
        ],
        // c_descripcion_n: [Validators.required, Validators.maxLength(250)],
        // c_f_inic_vig: [Validators.required],
        // c_f_fin_vig: [Validators.required],
      });
      
      if (this.catalogoEditar != undefined){
        this.formGroup.patchValue(this.catalogoEditar);
        this.formGroup.get('c_f_inic_vig').patchValue(formatDate(this.catalogoEditar.c_f_inic_vig, 'yyyy-MM-dd', 'en'));
        this.formGroup.get('c_f_fin_vig').patchValue(formatDate(this.catalogoEditar.c_f_inic_vig, 'yyyy-MM-dd', 'en'));
      }
    }
  
    
    obtenerErroresNombre() {
      var campo = this.formGroup.get("c_nombre_n");
      if (campo.hasError("required")) return "El campo es requerido";
    }
    // obtenerErroresDescripcion() {
    //   var campo = this.formGroup.get("c_descripcion_n");
    //   if (campo.hasError("required")) return "El campo es requerido";
    // }
    obtenerErroresFechaInicio() {
      var campo = this.formGroup.get("c_f_inic_vig");
      if (campo.hasError("required")) return "El campo es requerido";
    }
    obtenerErroresFechaFin() {
      var campo = this.formGroup.get("c_f_fin_vig");
      if (campo.hasError("required")) return "El campo es requerido";
    }
    OnSubmit() {
  
      this.ActualizarCatalogo.emit(this.formGroup.value);
    }
  }
  