import { Component, EventEmitter, OnInit, Output, TemplateRef, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from "@angular/router";
import { AuthIdentity } from "src/app/guards/AuthIdentity";
import { ServiceGenerico } from "src/app/services/service-generico.service";
import { TabService } from "../../services/tab.service";
import { GeneralComponent } from "../../components/general/general.component";
import { Subscription } from "rxjs";
import { DomSanitizer } from "@angular/platform-browser";
import { ModuloModalAdvertenciaComponent } from "src/app/shared/modulo-modal-advertencia/modulo-modal-advertencia.component";


@Component({
  selector: "app-modulo-formulario-registro-paso-cuatro",
  templateUrl: "./modulo-formulario-registro-paso-cuatro.component.html",
  styleUrls: ["./modulo-formulario-registro-paso-cuatro.component.css"],
  providers: [ServiceGenerico],
})
export class ModuloFormularioRegistroPasoCuatroComponent extends GeneralComponent implements OnInit {

  us_id: number;
  id_tramite: any;
  formGroupBase: FormGroup;
  formGroupRegular: FormGroup;
  formGroupIRegular: FormGroup;
  img: any = "";
  public routes: any[];
  public esRegular: number = 1;
  userChangedSubscription: Subscription | undefined;
  listaUso: any[];
  paso4Subscription: Subscription | undefined;
  paso4: boolean = null;
  public idDocumento: any = null;
  unidad: any = 1;
  destinadoAlPublico: boolean = true;
  destinadoAlPublicoI: boolean = true;
  esSoloLectura: any = localStorage.getItem("modoLectura") == "1" ? true : false;
  archivoIregural: any = null;
  archivoRegular: any = null;
  conteoMinimoEspecoficacion: number = 0;
  @ViewChild("contentAutorizar", { static: false }) modalAutorizar: TemplateRef<any>;
  @ViewChild("contentAudvertencia", { static: false }) modalAdvertencia: TemplateRef<any>;
  @Output()
  eventCambioTab = new EventEmitter<number>();
  get superficie() {
    return this.formGroupBase.get("superficie");
  }
  get norte() {
    return this.formGroupRegular.get("norte");
  }
  get sur() {
    return this.formGroupRegular.get("sur");
  }
  get esteOriente() {
    return this.formGroupRegular.get("esteOriente");
  }
  get oestePoniente() {
    return this.formGroupRegular.get("oestePoniente");
  }
  get norEste() {
    return this.formGroupRegular.get("norEste");
  }
  get noroEste() {
    return this.formGroupRegular.get("noroEste");
  }
  get sureste() {
    return this.formGroupRegular.get("sureste");
  }
  get suroEste() {
    return this.formGroupRegular.get("suroEste");
  }
  get fechaInicioActividades() {
    return this.formGroupRegular.get("fechaInicioActividades");
  }
  get usoPretendeDestinar() {
    return this.formGroupRegular.get("usoPretendeDestinar");
  }
  get otro() {
    return this.formGroupRegular.get("otro");
  }
  get colindancia() {
    return this.formGroupIRegular.get("colindancia");
  }
  get descripcionSalida() {
    return this.formGroupIRegular.get("descripcionSalida");
  }
  get urlMapa() {
    return this.formGroupRegular.get("urlMapa");
  }
  get imagenMapa() {
    return this.formGroupRegular.get("imagenMapa");
  }
  get imagenMapaI() {
    return this.formGroupIRegular.get("imagenMapa");
  }

  get fechaInicioActividadesI() {
    return this.formGroupIRegular.get("fechaInicioActividadesI");
  }
  get usoPretendeDestinarI() {
    return this.formGroupIRegular.get("usoPretendeDestinarI");
  }
  get otroI() {
    return this.formGroupIRegular.get("otroI");
  }

  public mensajesValidacion = {
    superficie: [
      { type: "required", message: "El campo es requerido." }
    ],
    norte: [
      { type: "required", message: "El campo es requerido." },
      { type: "min", message: "Debes agregar una cantidad mas grande." }
    ],
    sur: [
      { type: "required", message: "El campo es requerido." },
      { type: "min", message: "Debes agregar una cantidad mas grande." }
    ],
    esteOriente: [
      { type: "required", message: "El campo es requerido." },
      { type: "min", message: "Debes agregar una cantidad mas grande." }
    ],
    oestePoniente: [
      { type: "required", message: "El campo es requerido." },
      { type: "min", message: "Debes agregar una cantidad mas grande." }
    ],
    norEste: [
      { type: "required", message: "El campo es requerido." },
      { type: "min", message: "Debes agregar una cantidad mas grande." }
    ],
    noroEste: [
      { type: "required", message: "El campo es requerido." },
      { type: "min", message: "Debes agregar una cantidad mas grande." }
    ],
    sureste: [
      { type: "required", message: "El campo es requerido." },
      { type: "min", message: "Debes agregar una cantidad mas grande." }
    ],
    suroEste: [
      { type: "required", message: "El campo es requerido." },
      { type: "min", message: "Debes agregar una cantidad mas grande." }
    ],
    fechaInicioActividades: [
      { type: "required", message: "El campo es requerido." }
    ],
    usoPretendeDestinar: [
      { type: "required", message: "El campo es requerido." }
    ],
    otro: [
      { type: "required", message: "El campo es requerido." }
    ],
    colindancia: [
      { type: "required", message: "El campo es requerido." }
    ],
    descripcionSalida: [
      { type: "required", message: "El campo es requerido." }
    ],
    urlMapa: [
      { type: "required", message: "El campo es requerido." }
    ],
    imagenMapa: [
      { type: "required", message: "El campo es requerido." }
    ],
    fechaInicioActividadesI: [
      { type: "required", message: "El campo es requerido." }
    ],
    usoPretendeDestinarI: [
      { type: "required", message: "El campo es requerido." }
    ],
    otroI: [
      { type: "required", message: "El campo es requerido." }
    ],
  };

  constructor(
    private activetedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private _sanitizer: DomSanitizer,
    public tabsService: TabService
  ) {
    super()
  }

  public async ngOnInit() {

    this.userChangedSubscription = this.tabsService.idDeclaratoria$.subscribe(async (valor) => {
      this.id_tramite = valor
    })

    this.paso4Subscription = this.tabsService.paso4Obs$.subscribe(async (valor) => {
      this.paso4 = valor;
    })

    this.formGroupBase = this.fb.group({
      // especificacion: [true, { validators: [Validators.required] }],
      // unidad: [null, { validators: [Validators.required] }],
      superficie: [null, { validators: [Validators.required] }],

    });

    this.formGroupRegular = this.fb.group({
      norte: [{ value: 0, disabled: true }],
      sur: [{ value: 0, disabled: true }],
      esteOriente: [{ value: 0, disabled: true }],
      oestePoniente: [{ value: 0, disabled: true }],
      norEste: [{ value: 0, disabled: true }],
      noroEste: [{ value: 0, disabled: true }],
      sureste: [{ value: 0, disabled: true }],
      suroEste: [{ value: 0, disabled: true }],
      norteCh: [null],
      surCh: [null],
      esteOrienteCh: [null],
      oestePonienteCh: [null],
      norEsteCh: [null],
      noroEsteCh: [null],
      suresteCh: [null],
      suroEsteCh: [null],
      urlMapa: [null, { validators: [Validators.required] }],
      imagenMapa: [null],
      fechaInicioActividades: [null],
      usoPretendeDestinar: [null, { validators: [Validators.required] }],
      otro: [null],
    });

    this.formGroupIRegular = this.fb.group({
      colindancia: [null, { validators: [Validators.required] }],
      descripcionSalida: [null, { validators: [Validators.required] }],
      urlMapa: [null, { validators: [Validators.required] }],
      imagenMapa: [null],
      fechaInicioActividadesI: [null, { validators: [Validators.required] }],
      usoPretendeDestinarI: [null, { validators: [Validators.required] }],
      otroI: [null],
    });

    await this.obtenerUsoSuelo();
    this.us_id = AuthIdentity.ObtenerUsuarioRegistro();

    if (this.paso4) {
      await this.setearValoresEdicion()
    }
  }

  public async salir() {
    if (this.idPerfil == 11) {
      this.tabsService.salirModal("vista-principal-declaratoria")
    } else {
      this.tabsService.salirModal("declaratoria-procedencia")
    }
  }

  public async guardar() {
    let formularioRegular = this.formGroupRegular.value;
    let formularioIRegular = this.formGroupIRegular.value;
    // console.log(formularioIRegular)

    let tipoFormulario = this.formGroupBase.value;
    // console.log(this.formGroupBase)
    let objeto = null;

    if (this.esRegular == 1) {
      if (this.conteoMinimoEspecoficacion < 3) {
        this.modalService.open(this.modalAdvertencia, { ariaLabelledBy: 'modal-basic-title', backdrop: 'static' });
        return;
      }
      objeto = {
        "p_id_declaratoria": Number(this.id_tramite),
        "p_superficie": tipoFormulario.superficie,
        "p_unidad": this.unidad ? 1 : 2,
        "p_ubicacion": formularioRegular.urlMapa,
        "p_culto_publico": this.destinadoAlPublico,
        "p_inicio_actividades": this.destinadoAlPublico ? formularioRegular.fechaInicioActividades : null,
        "p_uso": Number(formularioRegular.usoPretendeDestinar),
        "p_norte": formularioRegular.norte ? Number(formularioRegular.norte) : 0,
        "p_noreste": formularioRegular.norEste ? Number(formularioRegular.norEste) : 0,
        "p_noroeste": formularioRegular.noroEste ? Number(formularioRegular.noroEste) : 0,
        "p_sur": formularioRegular.sur ? Number(formularioRegular.sur) : 0,
        "p_sureste": formularioRegular.sureste ? Number(formularioRegular.sureste) : 0,
        "p_suroeste": formularioRegular.suroEste ? Number(formularioRegular.suroEste) : 0,
        "p_oriente": formularioRegular.esteOriente ? Number(formularioRegular.esteOriente) : 0,
        "p_poniente": formularioRegular.oestePoniente ? Number(formularioRegular.oestePoniente) : 0,
        "p_otro": formularioRegular.otro,
        "p_colindancia": null,
        "p_descripcion_salida": null,
        "p_regular": true
      }
    } else {
      objeto = {
        "p_id_declaratoria": Number(this.id_tramite),
        "p_superficie": tipoFormulario.superficie,
        "p_unidad": this.unidad ? 1 : 2,
        "p_ubicacion": formularioIRegular.urlMapa,
        "p_culto_publico": this.destinadoAlPublicoI,
        "p_inicio_actividades": this.destinadoAlPublicoI ? formularioIRegular.fechaInicioActividadesI : null,
        "p_uso": Number(formularioIRegular.usoPretendeDestinarI),
        "p_norte": 0,
        "p_noreste": 0,
        "p_noroeste": 0,
        "p_sur": 0,
        "p_sureste": 0,
        "p_suroeste": 0,
        "p_oriente": 0,
        "p_poniente": 0,
        "p_otro": formularioIRegular.otroI,
        "p_colindancia": formularioIRegular.colindancia,
        "p_descripcion_salida": formularioIRegular.descripcionSalida,
        "p_regular": false
      }
    }

    if (!this.idDocumento) {
      let archivo: any = await this.cargarArchivo(this.esRegular == 1 ? this.archivoRegular : this.archivoIregural, 30, this.id_tramite, 1);
    }

    let respuesta = await this.webRestService.postAsync(objeto, this.modelo_configuracion.serviciosOperaciones + "/InsertarTramiteDeclaratoriaProcedencia/InsertarPaso4")
    // console.log(respuesta)
    if (respuesta != null && respuesta.response != null) {
      this.openMensajes("La información se ha guardado de forma exitosa.", false, "Declaratoria de procedencia");
      this.tabsService.cambiarValorPaso(4, true)
      this.tabsService.cambiarPasoContinuacion(5)
      this.tabsService.cambiarTab(5);
      this.eventCambioTab.emit(5)
    } else {
      this.openMensajes("La información no se ha guardado de forma exitosa.", true, "Declaratoria de procedencia");
    }

  }

  public async checkValue(tipo: number, event: any) {

    // console.log(event.currentTarget.checked)
    event.currentTarget.checked == true ? this.conteoMinimoEspecoficacion++ : this.conteoMinimoEspecoficacion--;

    if (tipo == 1) {
      let valor = this.formGroupRegular.value.norteCh;
      if (valor) {
        this.formGroupRegular.controls["norte"].setValue(null)
        this.formGroupRegular.controls["norte"].setValidators([Validators.required, Validators.min(1)]);
        this.formGroupRegular.controls["norte"].enable();
        // console.log(this.formGroupRegular);
      } else {
        this.formGroupRegular.controls["norte"].setValue(0)
        this.formGroupRegular.controls["norte"].clearValidators();
        this.formGroupRegular.controls["norte"].disable();
      }
    }

    if (tipo == 2) {
      let valor = this.formGroupRegular.value.surCh;
      if (valor) {
        this.formGroupRegular.controls["sur"].setValue(null)
        this.formGroupRegular.controls["sur"].setValidators([Validators.required, Validators.min(1)]);
        this.formGroupRegular.controls["sur"].enable();
        // console.log(this.formGroupRegular);
      } else {
        this.formGroupRegular.controls["sur"].setValue(0)
        this.formGroupRegular.controls["sur"].clearValidators();
        this.formGroupRegular.controls["sur"].disable();
      }
    }

    if (tipo == 3) {
      let valor = this.formGroupRegular.value.esteOrienteCh;
      if (valor) {
        this.formGroupRegular.controls["esteOriente"].setValue(null)
        this.formGroupRegular.controls["esteOriente"].setValidators([Validators.required, Validators.min(1)]);
        this.formGroupRegular.controls["esteOriente"].enable();
        // console.log(this.formGroupRegular);
      } else {
        this.formGroupRegular.controls["esteOriente"].setValue(0)
        this.formGroupRegular.controls["esteOriente"].clearValidators();
        this.formGroupRegular.controls["esteOriente"].disable();
      }
    }

    if (tipo == 4) {
      let valor = this.formGroupRegular.value.oestePonienteCh;
      if (valor) {
        this.formGroupRegular.controls["oestePoniente"].setValue(null)
        this.formGroupRegular.controls["oestePoniente"].setValidators([Validators.required, Validators.min(1)]);
        this.formGroupRegular.controls["oestePoniente"].enable();
        // console.log(this.formGroupRegular);
      } else {
        this.formGroupRegular.controls["oestePoniente"].setValue(0)
        this.formGroupRegular.controls["oestePoniente"].clearValidators();
        this.formGroupRegular.controls["oestePoniente"].disable();
      }
    }

    if (tipo == 5) {
      let valor = this.formGroupRegular.value.norEsteCh;
      if (valor) {
        this.formGroupRegular.controls["norEste"].setValue(null)
        this.formGroupRegular.controls["norEste"].setValidators([Validators.required, Validators.min(1)]);
        this.formGroupRegular.controls["norEste"].enable();
        // console.log(this.formGroupRegular);
      } else {
        this.formGroupRegular.controls["norEste"].setValue(0)
        this.formGroupRegular.controls["norEste"].clearValidators();
        this.formGroupRegular.controls["norEste"].disable();
      }
    }

    if (tipo == 6) {
      let valor = this.formGroupRegular.value.noroEsteCh;
      if (valor) {
        this.formGroupRegular.controls["noroEste"].setValue(null)
        this.formGroupRegular.controls["noroEste"].setValidators([Validators.required, Validators.min(1)]);
        this.formGroupRegular.controls["noroEste"].enable();
        // console.log(this.formGroupRegular);
      } else {
        this.formGroupRegular.controls["noroEste"].setValue(0)
        this.formGroupRegular.controls["noroEste"].clearValidators();
        this.formGroupRegular.controls["noroEste"].disable();
      }
    }

    if (tipo == 7) {
      let valor = this.formGroupRegular.value.suresteCh;
      if (valor) {
        this.formGroupRegular.controls["sureste"].setValue(null)
        this.formGroupRegular.controls["sureste"].setValidators([Validators.required, Validators.min(1)]);
        this.formGroupRegular.controls["sureste"].enable();
        // console.log(this.formGroupRegular);
      } else {
        this.formGroupRegular.controls["sureste"].setValue(0)
        this.formGroupRegular.controls["sureste"].clearValidators();
        this.formGroupRegular.controls["sureste"].disable();
      }
    }

    if (tipo == 8) {
      let valor = this.formGroupRegular.value.suroEsteCh;
      if (valor) {
        this.formGroupRegular.controls["suroEste"].setValue(null)
        this.formGroupRegular.controls["suroEste"].setValidators([Validators.required, Validators.min(1)]);
        this.formGroupRegular.controls["suroEste"].enable();
        // console.log(this.formGroupRegular);
      } else {
        this.formGroupRegular.controls["suroEste"].setValue(0)
        this.formGroupRegular.controls["suroEste"].clearValidators();
        this.formGroupRegular.controls["suroEste"].disable();
      }
    }

  }

  imprimir(algo: any) {
    //console.log(algo);
  }


  async obtenerUsoSuelo() {
    let respuesta = await this.webRestService.getAsync(this.modelo_configuracion.serviciosCatalogos + "/CatalogosTramiteDeclaratoria/GetUsoInmueble")
    // console.log(respuesta)
    if (respuesta != null && respuesta!.response!.length > 0) {
      this.listaUso = [] = respuesta.response as any[];
    } else {
      this.listaUso = [];
    }
  }

  public async setearValoresEdicion() {
    let respuesta = await this.webRestService.getAsync(this.modelo_configuracion.serviciosOperaciones + "/ConsultaTramiteDeclaratoria/ConsultaPaso4?id_declaratoria=" + this.id_tramite)
    // console.log(respuesta)

    if (respuesta != null && respuesta?.response?.length != 0) {
      this.esRegular = respuesta.response[0].regular ? 1 : 2;
      this.unidad = respuesta.response[0].unidad ? 1 : 0;
      this.idDocumento = respuesta.response[0].imagen_ubicacion;
      this.formGroupBase.setValue({
        // unidad: respuesta.response[0].unidad ? true : false,
        superficie: respuesta.response[0].superficie
      })
      if (this.deshabilitar || this.esSoloLectura)
        this.formGroupBase.disable()

      if (this.esRegular == 1) {
        if (respuesta.response[0].norte != null && respuesta.response[0].norte != "") {
          this.formGroupRegular.controls["norte"].enable();
          this.formGroupRegular.controls['norte'].setValidators([Validators.required, Validators.min(1)])
          this.formGroupRegular.get('norte').setValue(respuesta.response[0].norte);
          this.formGroupRegular.get('norteCh').setValue(true);
          this.conteoMinimoEspecoficacion++;
        } else {
          this.formGroupRegular.get('norte').setValue(0);
          this.formGroupRegular.controls['norte'].setValidators([])
        }
        if (respuesta.response[0].sur != null && respuesta.response[0].sur != "") {
          this.formGroupRegular.controls["sur"].enable();
          this.formGroupRegular.controls['sur'].setValidators([Validators.required, Validators.min(1)])
          this.formGroupRegular.get('sur').setValue(respuesta.response[0].sur);
          this.formGroupRegular.get('surCh').setValue(true);
          this.conteoMinimoEspecoficacion++;
        } else {
          this.formGroupRegular.get('sur').setValue(0);
          this.formGroupRegular.controls['sur'].setValidators([])
        }
        if (respuesta.response[0].oriente != null && respuesta.response[0].oriente != "") {
          this.formGroupRegular.controls["esteOriente"].enable();
          this.formGroupRegular.controls['esteOriente'].setValidators([Validators.required, Validators.min(1)])
          this.formGroupRegular.get('esteOriente').setValue(respuesta.response[0].oriente);
          this.formGroupRegular.get('esteOrienteCh').setValue(true);
          this.conteoMinimoEspecoficacion++;
        } else {
          this.formGroupRegular.get('esteOriente').setValue(0);
          this.formGroupRegular.controls['esteOriente'].setValidators([])
        }
        if (respuesta.response[0].poniente != null && respuesta.response[0].poniente != "") {
          this.formGroupRegular.controls["oestePoniente"].enable();
          this.formGroupRegular.controls['oestePoniente'].setValidators([Validators.required, Validators.min(1)])
          this.formGroupRegular.get('oestePoniente').setValue(respuesta.response[0].poniente);
          this.formGroupRegular.get('oestePonienteCh').setValue(true);
          this.conteoMinimoEspecoficacion++;
        } else {
          this.formGroupRegular.get('oestePoniente').setValue(0);
          this.formGroupRegular.controls['oestePoniente'].setValidators([])
        }
        if (respuesta.response[0].noreste != null && respuesta.response[0].noreste != "") {
          this.formGroupRegular.controls["norEste"].enable();
          this.formGroupRegular.controls['norEste'].setValidators([Validators.required, Validators.min(1)])
          this.formGroupRegular.get('norEste').setValue(respuesta.response[0].noreste);
          this.formGroupRegular.get('norEsteCh').setValue(true);
          this.conteoMinimoEspecoficacion++;
        } else {
          this.formGroupRegular.get('norEste').setValue(0);
          this.formGroupRegular.controls['norEste'].setValidators([])
        }
        if (respuesta.response[0].noroeste != null && respuesta.response[0].noroeste != "") {
          this.formGroupRegular.controls["noroEste"].enable();
          this.formGroupRegular.controls['noroEste'].setValidators([Validators.required, Validators.min(1)])
          this.formGroupRegular.get('noroEste').setValue(respuesta.response[0].noroeste);
          this.formGroupRegular.get('noroEsteCh').setValue(true);
          this.conteoMinimoEspecoficacion++;
        } else {
          this.formGroupRegular.controls['noroEste'].setValidators([])
          this.formGroupRegular.get('noroEste').setValue(0);
        }
        if (respuesta.response[0].sureste != null && respuesta.response[0].sureste != "") {
          this.formGroupRegular.controls["sureste"].enable();
          this.formGroupRegular.controls['sureste'].setValidators([Validators.required, Validators.min(1)])
          this.formGroupRegular.get('sureste').setValue(respuesta.response[0].sureste);
          this.formGroupRegular.get('suresteCh').setValue(true);
          this.conteoMinimoEspecoficacion++;
        } else {
          this.formGroupRegular.get('sureste').setValue(0);
          this.formGroupRegular.controls['sureste'].setValidators([])
        }
        if (respuesta.response[0].suroeste != null && respuesta.response[0].suroeste != "") {
          this.formGroupRegular.controls["suroEste"].enable();
          this.formGroupRegular.controls['suroEste'].setValidators([Validators.required, Validators.min(1)])
          this.formGroupRegular.get('suroEste').setValue(respuesta.response[0].suroeste);
          this.formGroupRegular.get('suroEsteCh').setValue(true);
          this.conteoMinimoEspecoficacion++;
        } else {
          this.formGroupRegular.get('suroEste').setValue(0);
          this.formGroupRegular.controls['suroEste'].setValidators([])
        }

        this.formGroupRegular.get('urlMapa').setValue(respuesta.response[0].ubicacion);
        this.formGroupRegular.get('fechaInicioActividades').setValue(respuesta.response[0].inicio_actividades);
        this.formGroupRegular.get('usoPretendeDestinar').setValue(respuesta.response[0].usoPretendeDestinar);
        this.formGroupRegular.get('otro').setValue(respuesta.response[0].otro);
        this.formGroupRegular.get('usoPretendeDestinar').setValue(respuesta.response[0].uso);
        this.destinadoAlPublico = respuesta.response[0].culto_publico;
        if (!this.destinadoAlPublico) {
          this.formGroupRegular.controls["fechaInicioActividades"].clearValidators();
          this.formGroupRegular.controls["fechaInicioActividades"].disable();
        }
        if (this.deshabilitar || this.esSoloLectura)
          this.formGroupRegular.disable()
      } else {

        this.formGroupIRegular.setValue({
          colindancia: respuesta.response[0].colindancia,
          descripcionSalida: respuesta.response[0].descripcion_salida,
          urlMapa: respuesta.response[0].ubicacion,
          imagenMapa: respuesta.response[0].imagen_ubicacion,
          fechaInicioActividadesI: respuesta.response[0].inicio_actividades,
          usoPretendeDestinarI: respuesta.response[0].uso,
          otroI: respuesta.response[0].otro
        })

        this.destinadoAlPublicoI = respuesta.response[0].culto_publico;
        if (!this.destinadoAlPublicoI) {
          this.formGroupIRegular.controls["fechaInicioActividadesI"].clearValidators();
          this.formGroupIRegular.controls["fechaInicioActividadesI"].disable();
        }
        if (this.deshabilitar || this.esSoloLectura)
          this.formGroupIRegular.disable()
      }
    }

  }

  async cambiarCultoPublico(formulario: number, valor) {

    if (formulario == 1) {
      if (!valor) {
        this.formGroupRegular.controls["fechaInicioActividades"].clearValidators();
        this.formGroupRegular.controls["fechaInicioActividades"].disable();
      } else {
        //this.formGroupRegular.controls["fechaInicioActividades"].setValidators(Validators.required);
        this.formGroupRegular.controls["fechaInicioActividades"].enable();
      }
    } else {
      if (!valor) {
        this.formGroupIRegular.controls["fechaInicioActividadesI"].clearValidators();
        this.formGroupIRegular.controls["fechaInicioActividadesI"].disable();
      } else {
        this.formGroupIRegular.controls["fechaInicioActividadesI"].setValidators(Validators.required);
        this.formGroupIRegular.controls["fechaInicioActividadesI"].enable();
      }
    }
  }

  public async cargarArchivoComponent(event, formulario) {
    this.idDocumento = null;
    let file = <File>event.target.files[0];
    if (file.size > this.max_size) {
      this.openMensajes("El máximo tamaño permitido es " + 250 + "Mb", true, "Carga de Documento");
      return;
    }

    let tipo = event.target.files[0].type;
    if (tipo.includes("png") || tipo.includes("jpg") || tipo.includes("jpeg")) {
      if (formulario == 1) this.archivoRegular = file;
      else this.archivoIregural = file
    } else {
      this.modalrefAdvertencia = this.modalService.open(ModuloModalAdvertenciaComponent, { ariaLabelledBy: "modal-basic-title" });
      this.modalrefAdvertencia.componentInstance.mensajeTitulo = "Error al cargar el archivo.";
      this.modalrefAdvertencia.componentInstance.mensaje = "Solo se admiten formatos .png o .jpg";
    }
  }

  public async detalleVer(tipoFormulario) {

    let hayArchivo = tipoFormulario == 1 ? this.archivoRegular : this.archivoIregural;
    // console.log(this.idDocumento, hayArchivo)
    if (!this.idDocumento && !hayArchivo) return;

    if (tipoFormulario == 1 && this.archivoRegular && !this.idDocumento) {
      const reader = new FileReader();
      reader.readAsDataURL(this.archivoRegular);
      reader.onload = async () => {
        // console.log(reader.result);
        let img: any = reader.result;
        this.img = this._sanitizer.bypassSecurityTrustResourceUrl(img)
        this.modalService.open(this.modalAutorizar, { ariaLabelledBy: 'modal-basic-title', backdrop: 'static' });
      };
      return;
    }

    if (tipoFormulario == 2 && this.archivoIregural && !this.idDocumento) {
      const reader = new FileReader();
      reader.readAsDataURL(this.archivoIregural);
      reader.onload = async () => {
        // console.log(reader.result);
        let img: any = reader.result;
        this.img = this._sanitizer.bypassSecurityTrustResourceUrl(img)
        this.modalService.open(this.modalAutorizar, { ariaLabelledBy: 'modal-basic-title', backdrop: 'static' });
      };
      return;
    }

    let img = await this.detalle(this.id_tramite, 30, true);
    img = "data:image/png;base64, " + img;
    this.img = this._sanitizer.bypassSecurityTrustResourceUrl(img)
    const modalref = this.modalService.open(this.modalAutorizar, { ariaLabelledBy: 'modal-basic-title', backdrop: 'static' });

  }

  async verificarOtro(event) {
    let valor = this.usoPretendeDestinar.value;
    if (valor == 11) {
      this.otro.setValidators([Validators.required])
      this.otro.updateValueAndValidity()
    } else {
      this.otro.setValidators([])
      this.otro.updateValueAndValidity()
    }
  }

  async verificarOtroI(event) {
    let valor = this.usoPretendeDestinarI.value;
    if (valor == 11) {
      this.otroI.setValidators([Validators.required])
      this.otroI.updateValueAndValidity()
    } else {
      this.otroI.setValidators([])
      this.otroI.updateValueAndValidity()
    }
  }

  openAdvertenciaInterno(idDocumento?, tipo?, formulario?) {
    if (formulario == 1 && !idDocumento && this.archivoRegular != null) {
      // eliminamos el que aun no se sube
      this.archivoRegular = null;
      this.idDocumento = null;
      this.formGroupRegular.controls['imagenMapa'].setValue(null)
      return;
    }

    if (formulario == 2 && !idDocumento && this.archivoIregural != null) {
      // eliminamos el que aun no se sube
      this.archivoIregural = null;
      this.idDocumento = null;
      this.formGroupIRegular.controls['imagenMapa'].setValue(null)
      return;
    }

    this.modalrefAdvertencia = this.modalService.open(ModuloModalAdvertenciaComponent, { ariaLabelledBy: "modal-basic-title" });
    this.modalrefAdvertencia.componentInstance.mensajeTitulo = "Eliminación del Documento";
    this.modalrefAdvertencia.componentInstance.mensaje = "¿Está seguro que desea eliminar el documento?";
    this.modalrefAdvertencia.result.then(async (result) => {
      if (result) {
        // await this.eliminar(idDocumento, tipo);
        this.archivoIregural = null;
        this.archivoRegular = null;
        this.idDocumento = null;
        if (formulario == 1) {
          this.formGroupRegular.controls['imagenMapa'].setValue(null)
        } else {
          this.formGroupIRegular.controls['imagenMapa'].setValue(null)
        }
      }
    });
  }
}
