import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, Routes } from "@angular/router";
import { AuthIdentity } from "src/app/guards/AuthIdentity";
import { ConsultaListaCatalogoColoniaResponse } from "src/app/model/Catalogos/CatalogoColonia";
import { RespuestaGenerica } from "src/app/model/Operaciones/generales/RespuestaGenerica";
import { ServiciosRutas } from "src/app/model/Operaciones/generales/ServiciosRutas";
import { route } from "src/app/model/Utilities/route";
import { ServiceGenerico } from "src/app/services/service-generico.service";
import { WebRestService } from "../../services/crud.rest.service";
import { TabService } from "../../services/tab.service";
import { NgbModal, NgbModalRef } from "@ng-bootstrap/ng-bootstrap";
import { GeneralComponent } from "../../components/general/general.component";
import { Subscription } from "rxjs";

@Component({
  selector: "app-modulo-formulario-registro-paso-dos",
  templateUrl: "./modulo-formulario-registro-paso-dos.component.html",
  styleUrls: ["./modulo-formulario-registro-paso-dos.component.css"],
  providers: [ServiceGenerico],
})
export class ModuloFormularioRegistroPasoDosComponent extends GeneralComponent implements OnInit {

  us_id: number;
  id_tramite: any;
  formGroup: FormGroup;
  personaNotificaciones = new FormArray([]);
  operacionRespuesta: RespuestaGenerica;
  public routes: any[];
  listaColonia: ConsultaListaCatalogoColoniaResponse[];
  modelo_configuracion: ServiciosRutas;
  userChangedSubscription: Subscription | undefined;
  paso2Subscription: Subscription | undefined;
  paso2: boolean = null;
  esSoloLectura: any = localStorage.getItem("modoLectura") == "1" ? true : false;
  @Output()
  eventCambioTab = new EventEmitter<number>();
  get d_cpostal() {
    return this.formGroup.get("d_cpostal")
  }
  get d_colonia() {
    return this.formGroup.get("d_colonia")
  }
  get d_calle() {
    return this.formGroup.get("d_calle")
  }
  get nombrePersonaAutorizada() {
    return this.formGroup.get("nombrePersonaAutorizada")
  }
  get correoElectronico() {
    return this.formGroup.get("correoElectronico")
  }
  get numeroTelefonico() {
    return this.formGroup.get("numeroTelefonico")
  }


  public mensajesValidacion = {
    d_cpostal: [
      { type: "required", message: "El campo es requerido." },
      { type: "maxlength", message: "El texto es demasiado largo." },
      { type: "minlength", message: "El texto es muy corto." },
    ],
    d_colonia: [
      { type: "required", message: "El campo es requerido." }
    ],
    d_calle: [
      { type: "required", message: "El campo es requerido." },
      { type: "maxlength", message: "El texto es demasiado largo." },
      { type: "minlength", message: "El texto es muy corto." }
    ],
    nombrePersonaAutorizada: [
      { type: "required", message: "El campo es requerido." },
    ],
    correoElectronico: [
      { type: "required", message: "El campo es requerido." },
      { type: "pattern", message: "Ingrese un correo electrónico valido." },
    ],
    numeroTelefonico: [
      { type: "pattern", message: "El número de teléfono debe tener 10 caracteres." },
      { type: "required", message: "El campo es requerido." },
    ],
  }
  constructor(
    private activetedRoute: ActivatedRoute,
    private fb: FormBuilder,
    public webRestService: WebRestService,
    public tabsService: TabService
  ) {
    super()
    this.operacionRespuesta = new RespuestaGenerica();
    this.modelo_configuracion = new ServiciosRutas();
  }

  public async ngOnInit() {
    this.userChangedSubscription = this.tabsService.idDeclaratoria$.subscribe(async (valor) => {
      this.id_tramite = valor
    })

    this.paso2Subscription = this.tabsService.paso2Obs$.subscribe(async (valor) => {
      this.paso2 = valor;
    })
    this.formGroup = this.fb.group({
      d_cpostal: [null, {
        validators: [Validators.required,
        Validators.maxLength(5),
        Validators.minLength(5)]
      },],
      d_endidad: [null],
      d_colonia: [null, { validators: [Validators.required] },],
      d_ciudad: [null],
      d_calle: [null, {
        validators: [Validators.required,
        Validators.minLength(1),
        Validators.maxLength(250)]
      }],
      d_numeroi: [""],
      d_numeroe: [""],
      lote: [null],
      manzana: [null],
      superManzana: [null],
      delegacion: [null],
      sector: [null],
      zona: [null],
      region: [null]
    });

    // agregamos la el primer formulario
    this.addPersona();
    // console.log(this.formGroup)
    this.us_id = AuthIdentity.ObtenerUsuarioRegistro();

    // console.log(this.paso2)
    if (this.paso2) {
      await this.setearValoresEdicion()
    }
  }

  public addPersona() {
    let formulario = this.fb.group({
      nombrePersonaAutorizada: [null, { validators: [Validators.required] },],
      correoElectronico: [null, [Validators.required, Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+.[a-z]{2,4}$")]],
      numeroTelefonico: [null, [Validators.required, Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]],
    });
    this.personaNotificaciones.push(formulario);
    // console.log(this.personaNotificaciones)
  }

  public eliminarPersona() {
    let minimo: number = this.personaNotificaciones.length;
    if (minimo == 1) {
      return
    }

    this.personaNotificaciones.removeAt(this.personaNotificaciones.length - 1)
  }

  imprimir(algo: any) {
    // console.log(algo)
  }

  async onEntrySelected(valor: any) {
    if (!this.d_cpostal.valid) {
      return
    }
    this.d_colonia.setValue(null)
    await this.obtenerColonias(valor)
  }

  async obtenerColonias(keyword: string) {
    let respuesta = await this.webRestService.getAsync(this.modelo_configuracion.serviciosCatalogos + "/ConsultaListaCatalogosColonia/Get?keyword=" + keyword)
    // console.log(respuesta)
    if (respuesta != null && respuesta!.response!.length > 0) {
      this.listaColonia = [] = respuesta.response as ConsultaListaCatalogoColoniaResponse[];
      this.formGroup.get("d_cpostal").patchValue(keyword);
      this.formGroup.get("d_endidad").patchValue(respuesta!.response[0].estado);
      this.formGroup.get("d_ciudad").patchValue(respuesta!.response[0].municipio);
    } else {
      this.formGroup.get("d_cpostal").patchValue(null);
      this.formGroup.get("d_endidad").patchValue(null);
      this.formGroup.get("d_colonia").patchValue(null);
      this.formGroup.get("d_calle").patchValue(null);
      this.formGroup.get("d_ciudad").patchValue(null);
      this.listaColonia = [];
    }
  }

  public async guardar() {

    let formulario = this.formGroup.value;
    let personas: any = this.personaNotificaciones.controls
    // console.log(personas)


    let objetoJSON = {}
    let arrayPersonas = [];

    for (let i = 0; i < personas.length; i++) {
      // console.log(personas[i].value)
      objetoJSON = {
        "i_id_tbl_declaratoria": this.id_tramite,
        "c_nombre": personas[i].value.nombrePersonaAutorizada,
        "c_correo_electronico": personas[i].value.correoElectronico,
        "c_numero_tel": Number(personas[i].value.numeroTelefonico)
      }
      arrayPersonas.push(objetoJSON)
    }

    // console.log(JSON.stringify(arrayPersonas))


    let objeto = {
      "p_id_declaratoria": this.id_tramite,
      "p_calle": formulario.d_calle,
      "p_numeroe": formulario.d_numeroe,
      "p_numeroi": formulario.d_numeroi,
      "p_i_id_tbl_colonia": formulario.d_colonia,
      "p_tipo_domicilio": 2,
      "p_lote": formulario.lote,
      "p_manzana": formulario.manzana,
      "p_super_manzana": formulario.superManzana,
      "p_delegacion": formulario.delegacion,
      "p_sector": formulario.sector,
      "p_zona": formulario.zona,
      "p_region": formulario.region,
      "p_personas_aut": JSON.stringify(arrayPersonas)
    }

    let respuesta = await this.webRestService.postAsync(objeto, this.modelo_configuracion.serviciosOperaciones + "/InsertarTramiteDeclaratoriaProcedencia/InsertarPaso2")
    // console.log(respuesta)
    if (respuesta != null && respuesta.response != null) {
      this.openMensajes("La información se ha guardado de forma exitosa.", false, "Declaratoria de procedencia");
      this.tabsService.cambiarValorPaso(2, true)
      this.tabsService.cambiarPasoContinuacion(3)
      this.tabsService.cambiarTab(3);
      this.eventCambioTab.emit(3);
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
    let respuesta = await this.webRestService.getAsync(this.modelo_configuracion.serviciosOperaciones + "/ConsultaTramiteDeclaratoria/ConsultaPaso2?id_declaratoria=" + this.id_tramite + "&tipo_domicilio=" + 2)
    // console.log(respuesta)

    if (respuesta != null && respuesta?.response?.length != 0) {
      await this.obtenerColonias(respuesta.response[0].codigo_postal)
      this.formGroup.setValue({
        d_cpostal: respuesta.response[0].codigo_postal,
        d_endidad: respuesta.response[0].estado,
        d_ciudad: respuesta.response[0].ciudad,
        d_colonia: respuesta.response[0].id_colonia,
        d_calle: respuesta.response[0].calle,
        d_numeroe: respuesta.response[0].numeroe,
        d_numeroi: respuesta.response[0].numeroi,
        delegacion: respuesta.response[0].delegacion,
        lote: respuesta.response[0].lote,
        manzana: respuesta.response[0].manzana,
        region: respuesta.response[0].region,
        sector: respuesta.response[0].sector,
        superManzana: respuesta.response[0].super_manzana,
        zona: respuesta.response[0].zona

      })
    }


    let personas = JSON.parse(respuesta.response[0].personas_autorizadas);

    // creamos los formularios necesarios
    if (personas.length != 1) {
      for (let i = 0; i < personas.length - 1; i++) {
        this.addPersona()
      }
    }


    for (let i = 0; i < this.personaNotificaciones.controls.length; i++) {
      this.personaNotificaciones.controls[i].setValue({
        nombrePersonaAutorizada: personas[i].c_nombre,
        correoElectronico: personas[i].c_correo_electronico,
        numeroTelefonico: personas[i].c_numero_tel
      })
    }

    if (this.deshabilitar || this.esSoloLectura) {
      this.personaNotificaciones.disable();
      this.formGroup.disable()
    }

  }

}
