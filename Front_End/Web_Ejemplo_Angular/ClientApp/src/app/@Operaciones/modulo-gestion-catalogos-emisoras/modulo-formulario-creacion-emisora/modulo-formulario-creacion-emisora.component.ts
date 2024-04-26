import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormArray, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CatalogoEmisoraInsertRequest } from '../../../model/Catalogos/CatalogosEmisora';
import { ServiceGenerico } from '../../../services/service-generico.service';
import { ServiciosRutas } from '../../../model/Operaciones/generales/ServiciosRutas';
import { CatalogoEstados } from '../../../model/Catalogos/CatalogoEstados';

@Component({
    selector: 'app-modulo-formulario-creacion-emisora',
    templateUrl: './modulo-formulario-creacion-emisora.component.html',
    styleUrls: ['./modulo-formulario-creacion-emisora.component.css'],
    providers: [ServiceGenerico]
})
/** modulo-formulario-creacion-credo component*/
export class ModuloFormularioCreacionEmisoraComponent implements OnInit {
  @Output()
  registrarCatalogo: EventEmitter<CatalogoEmisoraInsertRequest[]> =
    new EventEmitter<CatalogoEmisoraInsertRequest[]>();

    private modelo_configuracion: ServiciosRutas = new ServiciosRutas();
    public states:CatalogoEstados[] = [] as CatalogoEstados[];

  formGroup: FormGroup = this.fb.group({
    frecuencia_canal: ["", Validators.required],
    proveedor: [""],
    televisora_radiodifusora: ["", Validators.required],
    lugar_transmision: ["", Validators.required],
    televisora: ["", Validators.required],
  });

  constructor(public activeModal: NgbActiveModal, private fb: FormBuilder,private services: ServiceGenerico) { }

  ngOnInit(): void {
    this.services.HttpGet( this.modelo_configuracion.serviciosOperaciones + "/ConsultaEstados/Get" ).subscribe(tempdate =>  this.states = [] = tempdate.response as CatalogoEstados[] );
  }

  OnSubmit = (): void => this.registrarCatalogo.emit(this.formGroup.value);
}
