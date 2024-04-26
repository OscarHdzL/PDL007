import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Validators, FormBuilder, FormArray } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CatalogoConstanciaNotarioArraigoInsertRequest } from 'src/app/model/Catalogos/CatalogosConstanciaNotarioArraigo';
import { ConsultaListaCatalogoTSolRegResponse } from 'src/app/model/Catalogos/CatalogosTSolReg';
import { ServiciosRutas } from 'src/app/model/Operaciones/generales/ServiciosRutas';
import { ServiceGenerico } from '../../../services/service-generico.service';

@Component({
  selector: 'app-modulo-formulario-creacion-constancia-notario',
  templateUrl: './modulo-form-creacion-const-not.component.html',
  styleUrls: ['./modulo-form-creacion-const-not.component.css'],
  providers: [ServiceGenerico],
})
export class ModuloFormularioCreacionConstanciaNotarioComponent implements OnInit {

  listaSolicitudEscrito = [];

  @Output()
  registrarCatalogo: EventEmitter<CatalogoConstanciaNotarioArraigoInsertRequest[]> =
    new EventEmitter<CatalogoConstanciaNotarioArraigoInsertRequest[]>();

    form = this.fb.group({
      nombre: ["", { validators: [Validators.required,
                                  Validators.minLength(5),
                                  Validators.maxLength(60)], 
                      updateOn: "blur",
                    },
      ], 
      i_tipo_escrito : ["", { validators: [Validators.required] } ],
      catalogos: this.fb.array([]),
    });

  constructor( public activeModal: NgbActiveModal,
               private fb: FormBuilder,
               public services: ServiceGenerico) {

  }

  ngOnInit(): void {
    this.agregar();
    this.obtenerSolReg();
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
      i_tipo_escrito : ["", Validators.required ] ,
    });

    this.catalogos.push(catalogo);
  }


  OnSubmit() {
    this.registrarCatalogo.emit(this.catalogos.value);
  }

  obtenerSolReg() {
    const modeloCofiguracion = new ServiciosRutas(); 
    this.services.HttpGet(modeloCofiguracion.serviciosCatalogos +"/ConsultaListaCatalogosSolicitudEscrito/Get?Activos=" + true )
      .subscribe((tempdate) => {
        this.listaSolicitudEscrito = [];
        if (tempdate) {
          this.listaSolicitudEscrito = tempdate.response as ConsultaListaCatalogoTSolRegResponse[];
        }
      });
  }

}
