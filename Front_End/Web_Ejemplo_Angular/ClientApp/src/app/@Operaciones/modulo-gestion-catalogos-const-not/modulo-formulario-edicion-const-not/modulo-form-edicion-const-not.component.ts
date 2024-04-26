import { formatDate } from "@angular/common";
import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import {
  CatalogoConstanciaNotarioArraigoInsertRequest,
  EditarCatalogoConstanciaNotarioArraigoRequest,
} from "src/app/model/Catalogos/CatalogosConstanciaNotarioArraigo";
import { ConsultaListaCatalogoTSolRegResponse } from "src/app/model/Catalogos/CatalogosTSolReg";
import { ServiciosRutas } from "src/app/model/Operaciones/generales/ServiciosRutas";
import { ServiceGenerico } from "src/app/services/service-generico.service";

@Component({
  selector: "app-modulo-formulario-edicion-constancia-notario",
  templateUrl: "./modulo-form-edicion-const-not.component.html",
  styleUrls: ["./modulo-form-edicion-const-not.component.css"],
  providers: [ServiceGenerico],
})
export class ModuloFormularioEdicionConstanciaNotarioComponent
  implements OnInit
{
  @Output()
  ActualizarCatalogo: EventEmitter<CatalogoConstanciaNotarioArraigoInsertRequest[]> =
                  new EventEmitter<CatalogoConstanciaNotarioArraigoInsertRequest[]>();
  @Input()
  catalogoEditar: EditarCatalogoConstanciaNotarioArraigoRequest;

  formGroup: FormGroup; 
  listaSolicitudEscrito = [];

  constructor(public activeModal: NgbActiveModal, 
              private fb: FormBuilder,
              public services: ServiceGenerico) {}

  ngOnInit(): void {
    this.obtenerSolReg();
    this.formGroup = this.fb.group({
      c_id: [
        "",
        {
          validators: [Validators.required]
        },
      ],

      c_nombre_n: [
        "",
        {
          validators: [
            Validators.required,
            Validators.minLength(1),
            Validators.maxLength(60),
          ]
        },
      ],

      tipo_escrito: ["", { validators: [Validators.required] } ],
    });
    
    if (this.catalogoEditar != undefined) {
        this.formGroup.patchValue(this.catalogoEditar);
    }
  }

  obtenerErroresNombre() {
    var campo = this.formGroup.get("c_nombre_n");
    if (campo.hasError("required")) return "El campo es requerido";
  }
  obtenerErroresDescripcion() {
    var campo = this.formGroup.get("c_descripcion");
    if (campo.hasError("required")) return "El campo es requerido";
  }
  obtenerErroresFechaInicio() {
    var campo = this.formGroup.get("c_f_inic_vig");
    if (campo.hasError("required")) return "El campo es requerido";
  }
  obtenerErroresFechaFin() {
    var campo = this.formGroup.get("c_f_fin_vig");
    if (campo.hasError("required")) return "El campo es requerido";
  }
  obtenerErroresTipo() {
    var campo = this.formGroup.get("tipo_escrito");
    if (campo.hasError("required")) return "El campo es requerido";
  }
  OnSubmit() {
    this.ActualizarCatalogo.emit(this.formGroup.value);
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
