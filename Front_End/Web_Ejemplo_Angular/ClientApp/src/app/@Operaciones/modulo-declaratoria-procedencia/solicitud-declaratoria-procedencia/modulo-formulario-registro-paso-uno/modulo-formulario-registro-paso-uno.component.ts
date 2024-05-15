import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, Routes } from "@angular/router";
import { NgbModal, NgbModalRef } from "@ng-bootstrap/ng-bootstrap";
import { AuthIdentity } from "src/app/guards/AuthIdentity";
import { route } from "src/app/model/Utilities/route";
import { ServiceGenerico } from "src/app/services/service-generico.service";
import { ModuloModalAdvertenciaComponent } from "src/app/shared/modulo-modal-advertencia/modulo-modal-advertencia.component";
import { TabService } from "../../services/tab.service";
import { ServiciosRutas } from "src/app/model/Operaciones/generales/ServiciosRutas";
import { WebRestService } from "../../services/crud.rest.service";
import { GeneralComponent } from "../../components/general/general.component";
import { Subscription } from "rxjs";

@Component({
  selector: "app-modulo-formulario-registro-paso-uno",
  templateUrl: "./modulo-formulario-registro-paso-uno.component.html",
  styleUrls: ["./modulo-formulario-registro-paso-uno.component.css"],
  providers: [ServiceGenerico],
})
export class ModuloFormularioRegistroPasoUnoComponent extends GeneralComponent implements OnInit {

  us_id: number;
  id_tramite: any;
  formGroup: FormGroup = this.fb.group({
    nombreCompleto: [null, { validators: [Validators.required] }],
    denominacionReligiosa: [null, { validators: [Validators.required] }],
    numeroRegistroSGAR: [null, { validators: [Validators.required] }],
    cargo: [null, { validators: [Validators.required] }],
  });
  modalrefAdvertencia: NgbModalRef;
  public routes: any[];
  modelo_configuracion: ServiciosRutas;
  esSoloLectura: any = localStorage.getItem("modoLectura") == "1" ? true : false;
  @Output()
  eventCambioTab = new EventEmitter<number>();
  get denominacionReligiosa() {
    return this.formGroup.get("denominacionReligiosa")
  }

  get numeroRegistroSGAR() {
    return this.formGroup.get("numeroRegistroSGAR")
  }

  public mensajesValidacion = {
    denominacionReligiosa: [
      { type: "required", message: "El campo es requerido." }
    ],
    numeroRegistroSGAR: [
      { type: "required", message: "El campo es requerido." }
    ]
  }

  userChangedSubscription: Subscription | undefined;
  paso1Subscription: Subscription | undefined;
  paso1: boolean = null;

  constructor(
    private activetedRoute: ActivatedRoute,
    private fb: FormBuilder,
    public tabsService: TabService,
    public webRestService: WebRestService
  ) {
    super()
    this.modelo_configuracion = new ServiciosRutas();
  }

  public async ngOnInit() {

    this.userChangedSubscription = this.tabsService.idDeclaratoria$.subscribe(async (valor) => {
      this.id_tramite = valor
    })

    this.paso1Subscription = this.tabsService.paso1Obs$.subscribe(async (valor) => {
      this.paso1 = valor;
    })

    if (this.paso1 && this.id_tramite != 0) {
      await this.setearValoresEdicion()
    } else {
      await this.setearValores()
    }


    this.us_id = AuthIdentity.ObtenerUsuarioRegistro();
    // console.log(AuthIdentity.ObtenerUsuarioSesion(), "usuario sesion")
  }

  public async setearValores() {
    this.formGroup.setValue({
      nombreCompleto: AuthIdentity.ObtenerUsuarioSesion().Nombre + " " + AuthIdentity.ObtenerUsuarioSesion().ApPaterno + " " + AuthIdentity.ObtenerUsuarioSesion().ApMaterno,
      denominacionReligiosa: null,
      numeroRegistroSGAR: null,
      cargo: null
    })
  }

  public async guardar() {
    // console.log(this.id_tramite)
    let formulario = this.formGroup.value;
    let objeto = {
      p_id_declaratoria: this.id_tramite,
      p_nombre_completo: formulario.nombreCompleto.trim(),
      p_denominacion_religiosa: formulario.denominacionReligiosa.trim(),
      p_numero_sgar: formulario.numeroRegistroSGAR.trim(),
      p_i_id_tbl_cargo: formulario.cargo,
      p_i_id_tbl_usuario: this.us_id
    }

    let respuesta = await this.webRestService.postAsync(objeto, this.modelo_configuracion.serviciosOperaciones + "/InsertarTramiteDeclaratoriaProcedencia/InsertarPaso1")
    // console.log(respuesta)
    if (respuesta != null && respuesta.response != null) {
      if (this.id_tramite == 0) {
        // console.log(this.id_tramite)
        this.tabsService.cambiarPasoContinuacion(2)
      }
      this.tabsService.cambiarIdDeclaratoria(respuesta.response[0].id_generico)
      this.openMensajes("La información se ha guardado de forma exitosa.", false, "Declaratoria de procedencia");
      this.tabsService.cambiarValorPaso(1, true)
      this.tabsService.cambiarTab(2);
      this.eventCambioTab.emit(2)
    } else {
      this.openMensajes("La información no se ha guardado de forma exitosa.", true, "Declaratoria de procedencia");
    }
  }

  public async salir() {
    if (this.idPerfil == 11) {
      this.tabsService.salirModal("vista-principal-declaratoria")
    } else {
      this.tabsService.salirModal("declaratoria-procedencia")
    }
  }

  public async setearValoresEdicion() {
    let respuesta = await this.webRestService.getAsync(this.modelo_configuracion.serviciosOperaciones + "/ConsultaTramiteDeclaratoria/ConsultaPaso1?id_declaratoria=" + this.id_tramite)
    // console.log(respuesta)
    if (respuesta != null && respuesta?.response?.length != 0) {
      this.formGroup.setValue({
        nombreCompleto: respuesta.response[0].nombre_completo,
        denominacionReligiosa: respuesta.response[0].denominacion_religiosa,
        numeroRegistroSGAR: respuesta.response[0].numero_sgar,
        cargo: respuesta.response[0].i_id_tbl_cargo
      })
      if (this.deshabilitar || this.esSoloLectura)
        this.formGroup.disable()
    }
  }
}
