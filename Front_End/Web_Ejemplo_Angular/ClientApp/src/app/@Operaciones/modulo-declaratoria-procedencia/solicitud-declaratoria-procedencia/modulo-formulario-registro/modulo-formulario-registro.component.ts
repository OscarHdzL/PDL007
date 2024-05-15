import { Component, OnDestroy, OnInit, TemplateRef, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, Routes } from "@angular/router";
import { Subscription } from "rxjs";
import { AuthIdentity } from "src/app/guards/AuthIdentity";
import { route } from "src/app/model/Utilities/route";
import { ServiceGenerico } from "src/app/services/service-generico.service";
import { TabService } from "../../services/tab.service";
import { WebRestService } from "../../services/crud.rest.service";
import { ServiciosRutas } from "src/app/model/Operaciones/generales/ServiciosRutas";
import { RespuestaGenerica } from "src/app/model/Operaciones/generales/RespuestaGenerica";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: "app-modulo-formulario-registro",
  templateUrl: "./modulo-formulario-registro.component.html",
  styleUrls: ["./modulo-formulario-registro.component.css"],
  providers: [ServiceGenerico],
})
export class ModuloFormularioRegistroComponent implements OnInit, OnDestroy {

  us_id: number;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;

  public routes: any[] = [];
  // public pasoSeleccionado: number = 1;

  public pasoSeleccionado: number = 0;
  userChangedSubscription: Subscription | undefined;
  restWebSubscription: Subscription | undefined;
  idDeclaratoriaSubscription: Subscription | undefined;

  paso1Subscription: Subscription | undefined;
  paso2Subscription: Subscription | undefined;
  paso3Subscription: Subscription | undefined;
  paso4Subscription: Subscription | undefined;
  paso5Subscription: Subscription | undefined;
  paso1: boolean = null;
  paso2: boolean = null;
  paso3: boolean = null;
  paso4: boolean = null;
  paso5: boolean = null;

  pasoContinuacionSubscription: Subscription | undefined;
  pasoContinuacion: any = 1;

  estaCargando: boolean = false;
  idDeclaratoria: any = null;
  modelo_configuracion: ServiciosRutas;
  operacionRespuesta: RespuestaGenerica;

  @ViewChild("contentEliminar", { static: false }) ModalEliminar: TemplateRef<any>;
  mensajePasoPendiente: any = "";

  constructor(
    private activetedRoute: ActivatedRoute,
    private _formBuilder: FormBuilder,
    public tabService: TabService,
    public webRestService: WebRestService,
    public modalService: NgbModal
  ) {
    this.operacionRespuesta = new RespuestaGenerica();
    this.modelo_configuracion = new ServiciosRutas();
  }

  public async ngOnInit() {
    this.routes = [
      {
        id: 1,
        nombre: "Paso 1",
        activo: true
      },
      {
        id: 2,
        nombre: "Paso 2",
        activo: false
      },
      {
        id: 3,
        nombre: "Paso 3",
        activo: false
      },
      {
        id: 4,
        nombre: "Paso 4",
        activo: false
      },
      {
        id: 5,
        nombre: "Paso 5",
        activo: false
      }
    ];

    this.idDeclaratoria = this.activetedRoute.snapshot.paramMap.get("declaratoria") ? this.activetedRoute.snapshot.paramMap.get("declaratoria") : 0;
    this.tabService.cambiarIdDeclaratoria(this.idDeclaratoria)
    if(this.idDeclaratoria == 0)
    // console.log(this.idDeclaratoria)



    this.userChangedSubscription = this.tabService.numeroTabObs$.subscribe(async (valor) => {
      this.pasoSeleccionado = valor;
      // console.log(valor)
      // console.log("cambio el componente que se abre", valor)
      await this.cambiarComponente({ id: valor })
    })

    this.restWebSubscription = this.tabService.estaCargando$.subscribe(async (valor) => {
      this.estaCargando = valor;
    })

    this.idDeclaratoriaSubscription = this.tabService.idDeclaratoria$.subscribe(async (valor) => {
      this.idDeclaratoria = valor;
      // console.log("cambio el id", valor)
    })

    this.pasoContinuacionSubscription = this.tabService.pasoContinuacionObs$.subscribe(async (valor) => {
      this.pasoContinuacion = valor;
      // console.log("haste ste conponente se puede acceder", valor)
    })

    this.paso1Subscription = this.tabService.paso1Obs$.subscribe(async (valor) => {
      this.paso1 = valor;
    })
    this.paso2Subscription = this.tabService.paso2Obs$.subscribe(async (valor) => {
      this.paso2 = valor;
    })
    this.paso3Subscription = this.tabService.paso3Obs$.subscribe(async (valor) => {
      this.paso3 = valor;
    })
    this.paso4Subscription = this.tabService.paso4Obs$.subscribe(async (valor) => {
      this.paso4 = valor;
    })
    this.paso5Subscription = this.tabService.paso5Obs$.subscribe(async (valor) => {
      this.paso5 = valor;
    })

    this.us_id = AuthIdentity.ObtenerUsuarioRegistro();
    if (this.idDeclaratoria != 0) {
      await this.revisarPasoSiguiente()
    }

  }
  cambioTab(event: any){
    this.cambiarComponente(this.routes.find(x => x.id == event), false);

  }

  public async cambiarComponente(paso: any, esAutomatico: boolean = true) {
    // console.log(paso)
    this.mensajePasoPendiente = this.pasoContinuacion == 5 ? "quinto" : this.pasoSeleccionado == 4 ?
      "cuarto" : this.pasoSeleccionado == 3 ? "tercer" : this.pasoSeleccionado == 2 ? "segundo" :
        this.pasoSeleccionado == 1 ? "primero" : "";


    // vamos a validar a que paso quiere ir, para revisar si ya lo tiene
    if (!esAutomatico) {
      if (paso.id > this.pasoContinuacion) {
        this.openAdvertencia();
        return;
      }
    }


    this.pasoSeleccionado = paso.id;
    for (let i = 0; i < this.routes.length; i++) {
      if (this.routes[i].id == paso.id) {
        this.routes[i].activo = true;
      } else {
        this.routes[i].activo = false
      }
    }
    // console.log(this.routes)
  }


  async ngOnDestroy() {
    // console.log("se va a desctrir el componente")
    localStorage.removeItem("modoLectura");
    await this.cambiarComponente(1);
    this.tabService.cambiarIdDeclaratoria(0);
    this.tabService.cambiarPasoContinuacion(1)
    this.pasoSeleccionado = 0;
    this.tabService.cambiarTab(1);
    this.userChangedSubscription.unsubscribe()
    this.restWebSubscription.unsubscribe()
    this.idDeclaratoriaSubscription.unsubscribe()
    this.pasoContinuacionSubscription.unsubscribe()
    this.paso1Subscription.unsubscribe()
    this.paso2Subscription.unsubscribe()
    this.paso3Subscription.unsubscribe()
    this.paso4Subscription.unsubscribe()
    this.paso5Subscription.unsubscribe()
  }

  public async revisarPasoSiguiente() {
    let respuesta = await this.webRestService.getAsync(this.modelo_configuracion.serviciosOperaciones + "/ConsultaTramiteDeclaratoria/ConsultaAvance?id_declaratoria=" + this.idDeclaratoria)
    // console.log(respuesta)

    if (respuesta != null && respuesta.response.length != 0) {


      let paso1 = respuesta.response[0].paso1;
      let paso2 = respuesta.response[0].paso2;
      let paso3 = respuesta.response[0].paso3;
      let paso4 = respuesta.response[0].paso4;
      let paso5 = respuesta.response[0].paso5;

      this.tabService.cambiarValorPaso(1, paso1)
      this.tabService.cambiarValorPaso(2, paso2)
      this.tabService.cambiarValorPaso(3, paso3)
      this.tabService.cambiarValorPaso(4, paso4)
      this.tabService.cambiarValorPaso(5, paso5)

      let ultimoPasoRealizado = paso5 ? 5 : paso4 ? 4 : paso3 ? 3 : paso2 ? 2 : 1;
      this.pasoContinuacion = ultimoPasoRealizado == 5 ? 5 : ultimoPasoRealizado + 1;
      await this.cambiarComponente({ id: AuthIdentity.ObtenerPerfilUsuarioSesion() == 1 ? this.pasoContinuacion : 1 })
    }
  }


  openAdvertencia() {
    const modalref = this.modalService.open(this.ModalEliminar, { ariaLabelledBy: 'modal-basic-title' });
  }




}
