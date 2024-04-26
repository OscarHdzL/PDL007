import { Component, OnInit, TemplateRef, ViewChild } from "@angular/core";
import { ServiceGenerico } from "src/app/services/service-generico.service";
import { GeneralComponent } from "../../components/general/general.component";
import { ActivatedRoute, Router } from "@angular/router";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { WebRestService } from "../../services/crud.rest.service";
import { TabService } from "../../services/tab.service";
import { RespuestaGenerica } from "src/app/model/Operaciones/generales/RespuestaGenerica";
import { ServiciosRutas } from "src/app/model/Operaciones/generales/ServiciosRutas";
import { Subscription } from "rxjs";
import pdfMake from 'pdfmake/build/pdfmake';
import pdfFonts from 'pdfmake/build/vfs_fonts';
import { ModuloModalAdvertenciaComponent } from "src/app/shared/modulo-modal-advertencia/modulo-modal-advertencia.component";
import { BrowserModule, DomSanitizer } from '@angular/platform-browser';
import { ModuloVisorPdfComponent } from "src/app/shared/modulo-visor-pdf/modulo-visor-pdf.component";
import { ConversionService } from '../../../../services/ConversionService';
import { ModeloWordToPdf } from "src/app/model/Operaciones/AsignacionRegistro/AsignacionRegistro";
import { ModuloVisorWordPdfComponent } from "src/app/shared/modulo-visor-word-pdf/modulo-visor-word-pdf.component";
import { NgbModalRef } from "@ng-bootstrap/ng-bootstrap";
pdfMake.vfs = pdfFonts.pdfMake.vfs;

@Component({
    selector: "app-modulo-formulario-registro-paso-cinco",
    templateUrl: "./modulo-formulario-registro-paso-cinco.component.html",
    styleUrls: ["./modulo-formulario-registro-paso-cinco.component.css"],
    providers: [ConversionService]
})
export class ModuloFormularioRegistroPasoCincoComponent extends GeneralComponent implements OnInit {

    private modalVistaPrevia: NgbModalRef;
    private argWordToPdf: ModeloWordToPdf[];
    operacionRespuesta: RespuestaGenerica;
    modelo_configuracion: ServiciosRutas;
    userChangedSubscription: Subscription | undefined;
    id_tramite: any;
    paso5Subscription: Subscription | undefined;
    paso5: boolean = null;
    formGroup: FormGroup;
    observarGroup: FormGroup;
    concluirGroup: FormGroup;
    autorizarGroup: FormGroup;
    lstHorarios = [
        { i_id: 1, c_horario: '10:00 am - 11:00 am' },
        { i_id: 2, c_horario: '11:00 am - 12:00 pm' },
        { i_id: 3, c_horario: '12:00 pm - 13:00 pm' }
    ];
    @ViewChild("contentObservar", { static: false }) modalObservar: TemplateRef<any>;
    @ViewChild("contentConcluir", { static: false }) modalConcluir: TemplateRef<any>;
    @ViewChild("contentAutorizar", { static: false }) modalAutorizar: TemplateRef<any>;
    @ViewChild("contentImagen", { static: false }) modalImagen: TemplateRef<any>;

    public idDocumentoPeticion: any = null;
    public idDocumentoAnexo1: any = null;
    public idDocumentoAnexo2: any = null;

    public archivoDocumentacionPeticion = null;
    public archivoDocumentoAnexo1 = null;
    public archivoDocumentoAnexo2 = null;

    public archivoDocumentacionPeticionTipo = null;
    public archivoDocumentoAnexo1Tipo = null;
    public archivoDocumentoAnexo2Tipo = null;

    archivoDocumentacionAutorizar = null;

    estaCargando: boolean = false;

    esSoloLectura: any = localStorage.getItem("modoLectura") == "1" ? true : false;
    mostrarGeneraOficio: boolean;
    img = null;

    get manifestacion() {
        return this.formGroup.get("manifestacion")
    }


    //#region Variables para viulizar el word
    //#endregion

    constructor(private activetedRoute: ActivatedRoute,
        private fb: FormBuilder,
        public webRestService: WebRestService,
        public tabsService: TabService,
        private router: Router,
        private services: ServiceGenerico,
        private _sanitizer: DomSanitizer,
        private conversionPdf: ConversionService) {
        super()
        this.operacionRespuesta = new RespuestaGenerica();
        this.modelo_configuracion = new ServiciosRutas();
        this.argWordToPdf = new Array<ModeloWordToPdf>();
    }

    public async ngOnInit() {
        this.userChangedSubscription = this.tabsService.idDeclaratoria$.subscribe(async (valor) => {
            this.id_tramite = valor
        })

        this.paso5Subscription = this.tabsService.paso5Obs$.subscribe(async (valor) => {
            this.paso5 = valor;
        })

        this.formGroup = this.fb.group({
            peticion: [null, { validators: [Validators.required] }],
            primerDocumento: [null],
            segundoDocumento: [null],
            manifestacion: [null]
        });

        this.observarGroup = this.fb.group({
            comentarios: [null, { validators: [Validators.required] }]
        });

        this.autorizarGroup = this.fb.group({
            oficio: [null, { validators: [Validators.required] }],
            fecha: [null, { validators: [Validators.required] }],
            horario: [null, { validators: [Validators.required] }],
            direccion: ["Londres No. 102, Piso 4, Colonia Juarez, Alcaldia Cuauhtemoc, Ciudad de Mexico, C.P. 06600.", { validators: [Validators.required] }],
        });

        this.concluirGroup = this.fb.group({
            comentarios: [null, { validators: [Validators.required] }],
            opcion: [null, { validators: [Validators.required] }],
        });

        if (this.paso5) {
            await this.setearValores()
        }
    }

    public async salir() {
        if (this.idPerfil == 11 || this.idPerfil == 12) {
            this.tabsService.salirModal("vista-principal-declaratoria")
        } else {
            this.tabsService.salirModal("declaratoria-procedencia")
        }
    }

    public async guardar() {
        if (!this.idDocumentoPeticion) {
            await this.cargarArchivo(this.archivoDocumentacionPeticion, 31, this.id_tramite, 1);
        }

        if (!this.idDocumentoAnexo1) {
            await this.cargarArchivo(this.archivoDocumentoAnexo1, 32, this.id_tramite, 1);
        }

        if (!this.idDocumentoAnexo2) {
            await this.cargarArchivo(this.archivoDocumentoAnexo2, 33, this.id_tramite, 1);
        }

        let objeto = {
            "p_id_declaratoria": this.id_tramite,
            "p_manifiesto_verdad": true
        }
        let respuesta = await this.webRestService.postAsync(objeto, this.modelo_configuracion.serviciosOperaciones + "/InsertarTramiteDeclaratoriaProcedencia/InsertarPaso5")
        if (respuesta != null && respuesta.response != null) {
            this.router.navigate(['/vista-principal-declaratoria']);
        } else {
            this.openMensajes("La información no se ha guardado de forma exitosa.", true, "Declaratoria de procedencia");
        }
    }

    public async guardarD() {
        let objeto = {
            "p_id_declaratoria": this.id_tramite,
            "p_manifiesto_verdad": true
        }
        let respuesta = await this.webRestService.postAsync(objeto, this.modelo_configuracion.serviciosOperaciones + "/InsertarTramiteDeclaratoriaProcedencia/InsertarPaso5")
        if (respuesta != null && respuesta.response != null) {
            this.openMensajes("“¡Éxito!, se ha guardado exitosamente su solicitud.", false, "Declaratoria de procedencia");
            this.router.navigate(['/vista-principal-declaratoria']);
        } else {
            this.openMensajes("La información no se ha guardado de forma exitosa.", true, "Declaratoria de procedencia");
        }
    }

    public async setearValores() {
        let respuesta = await this.webRestService.getAsync(this.modelo_configuracion.serviciosOperaciones + "/ConsultaTramiteDeclaratoria/ConsultaPaso5?id_declaratoria=" + this.id_tramite)

        if (respuesta != null && respuesta?.response?.length != 0) {
            this.idDocumentoPeticion = respuesta.response[0].peticion;
            this.idDocumentoAnexo1 = respuesta.response[0].anexo1;
            this.idDocumentoAnexo2 = respuesta.response[0].anexo2;
            this.formGroup.get('manifestacion').setValue(respuesta.response[0].declaratoria_verdad);
            this.mostrarGeneraOficio = respuesta.response[0].genera_oficio;
            if (this.deshabilitar || this.esSoloLectura)
                this.formGroup.disable()
        }
    }

    public async finalizar() {
        let objeto = {
            "p_id_declaratoria": this.id_tramite
        }
        let respuesta = await this.webRestService.postAsync(objeto, this.modelo_configuracion.serviciosOperaciones + "/OperacionesTramiteDeclaratoria/Finalizar")
        if (respuesta != null && respuesta.response != null) {
            this.router.navigate(['/vista-principal-declaratoria']);
            this.openMensajes("La declaratoria se ha finalizado de forma exitosa.", false, "Declaratoria de procedencia");
        } else {
            this.openMensajes("La declaratoria no se ha finalizado de forma exitosa.", true, "Declaratoria de procedencia");
        }
    }


    public async abrirModal(tipo: number) {
        if (tipo == 1) {
            const modalref = this.modalService.open(this.modalObservar, { ariaLabelledBy: 'modal-basic-title', backdrop: 'static' });
        }
        if (tipo == 3) {
            this.autorizarGroup.reset();
            this.archivoDocumentacionAutorizar = null;
            this.autorizarGroup.controls['direccion'].setValue("Londres No. 102, Piso 4, Colonia Juarez, Alcaldia Cuauhtemoc, Ciudad de Mexico, C.P. 06600.")
            const modalref = this.modalService.open(this.modalAutorizar, { ariaLabelledBy: 'modal-basic-title', backdrop: 'static' });
        }
        if (tipo == 4) {
            const modalref = this.modalService.open(this.modalConcluir, { ariaLabelledBy: 'modal-basic-title', backdrop: 'static' });
        }
    }

    public async observar() {
        let objeto = {
            "p_id_declaratoria": this.id_tramite,
            "p_comentarios": this.observarGroup.controls.comentarios.value
        }

        this.modalService.dismissAll();
        let respuesta = await this.webRestService.postAsync(objeto, this.modelo_configuracion.serviciosOperaciones + "/OperacionesTramiteDeclaratoria/PostComentarios")
        if (respuesta != null && respuesta.response != null) {
            this.openMensajes("¡Éxito! La información se ha cargado de forma exitosa.", false, "Declaratoria de procedencia");
        } else {
            this.openMensajes("Error! La información no se ha cargado de forma exitosa.", true, "Declaratoria de procedencia");
        }
    }

    public async generarOficio() {
        this.modalService.dismissAll();

        const url = `${`${this.modelo_configuracion.serviciosOperaciones}/OperacionesTramiteDeclaratoria/GenerarOficio?p_id_declaratoria=${this.id_tramite}`}`;
        this.services.HttpGetFile(url)
            .subscribe((response: Blob) => {
                this.mostrarGeneraOficio = true;
                const file = URL.createObjectURL(response);
                window.open(file);
            });
    }

    public async autorizar() {
        await this.cargarArchivo(this.archivoDocumentacionAutorizar, 34, this.id_tramite, 1);

        let objeto = {
            "p_id_declaratoria": this.id_tramite,
            "p_fecha": this.autorizarGroup.controls.fecha.value,
            "p_horario": this.autorizarGroup.controls.horario.value,
            "p_direccion": this.autorizarGroup.controls.direccion.value
        }

        this.modalService.dismissAll();
        let respuesta = await this.webRestService.postAsync(objeto, this.modelo_configuracion.serviciosOperaciones + "/OperacionesTramiteDeclaratoria/Autorizar")
        if (respuesta != null && respuesta.response != null) {
            this.openMensajes("¡Éxito! La información se ha autorizado de forma exitosa.", false, "Declaratoria de procedencia");
        } else {
            this.openMensajes("Error! La información no se ha autorizado de forma exitosa.", true, "Declaratoria de procedencia");
        }
    }

    public async concluir() {
        let objeto = {
            "p_id_declaratoria": this.id_tramite,
            "p_estatus": this.concluirGroup.controls.opcion.value,
            "p_comentarios": this.concluirGroup.controls.comentarios.value
        }

        this.modalService.dismissAll();
        let respuesta = await this.webRestService.postAsync(objeto, this.modelo_configuracion.serviciosOperaciones + "/OperacionesTramiteDeclaratoria/Concluir")
        if (respuesta != null && respuesta.response != null) {
            this.openMensajes("¡Éxito! La información se ha concluido de forma exitosa.", false, "Declaratoria de procedencia");
        } else {
            this.openMensajes("Error! La información no se ha concluido de forma exitosa.", true, "Declaratoria de procedencia");
        }
    }

    public async cargarArchivoComponent(event, tipo) {
        this.argWordToPdf = [];
        this.estaCargando = true;

        let file = <File>event.target.files[0];

        if (tipo == 31) {
            this.idDocumentoPeticion = null
        }

        if (tipo == 32) {
            this.idDocumentoAnexo1 = null
        }

        if (tipo == 33) {
            this.idDocumentoAnexo2 = null
        }

        if (file.size > this.max_size) {
            this.openMensajes("El máximo tamaño permitido es " + 250 + "Mb", true, "Carga de Documento");
            this.estaCargando = false;
            return;
        }

        let tipoDocumento = event.target.files[0].type;
        if (tipoDocumento.includes("png") || tipoDocumento.includes("jpg") || tipoDocumento.includes("jpeg")
            || tipoDocumento.includes("docx") || tipoDocumento.includes("pdf") || tipoDocumento.includes("word")) {
            if (tipo == 31) {
                this.archivoDocumentacionPeticion = file;
                this.archivoDocumentacionPeticionTipo = file.type.includes("image") ? 1 : file.type.includes("pdf") ? 2 : 3;
            }

            if (tipo == 32) {
                this.archivoDocumentoAnexo1 = file;
                this.archivoDocumentoAnexo1Tipo = file.type.includes("image") ? 1 : file.type.includes("pdf") ? 2 : 3;
            }

            if (tipo == 33) {
                this.archivoDocumentoAnexo2 = file;
                this.archivoDocumentoAnexo2Tipo = file.type.includes("image") ? 1 : file.type.includes("pdf") ? 2 : 3;
            }

            if (tipoDocumento === 'application/vnd.openxmlformats-officedocument.wordprocessingml.document') {
                var htmlWordToPdf = await this.conversionPdf.convertWordToPdfAsync(file);
                var elemento = this.argWordToPdf.find(f => f.tipo == tipo);
                if (elemento !== undefined)
                    elemento.data = htmlWordToPdf;
                else
                    this.argWordToPdf.push(new ModeloWordToPdf(tipo, htmlWordToPdf));

            }
        } else {
            this.modalrefAdvertencia = this.modalService.open(ModuloModalAdvertenciaComponent, { ariaLabelledBy: "modal-basic-title" });
            this.modalrefAdvertencia.componentInstance.mensajeTitulo = "Error al cargar el archivo.";
            this.modalrefAdvertencia.componentInstance.mensaje = "Solo se admiten formatos Word, PDF, JPG, PNG ";
        }

        this.estaCargando = false;
    }

    public async cargarArchivoAutorizar(event, tipo) {
        let file = <File>event.target.files[0];
        // this.autorizarGroup.controls['oficio'].setValue(null);
        this.archivoDocumentacionAutorizar = null;

        if (file.size > this.max_size) {
            this.openMensajes("El máximo tamaño permitido es " + 250 + "Mb", true, "Carga de Documento");
            return;
        }


        if (file.type.includes("pdf")) {
            this.archivoDocumentacionAutorizar = file;
        } else {
            this.modalrefAdvertencia = this.modalService.open(
                ModuloModalAdvertenciaComponent, { ariaLabelledBy: "modal-basic-title" }
            );
            this.modalrefAdvertencia.componentInstance.mensajeTitulo = "Error al cargar el archivo.";
            this.modalrefAdvertencia.componentInstance.mensaje = "Solo se admiten formatos PDF ";
        }
    }

    public async detalleVer(tipo) {
        if (tipo == 31) {
            if (!this.idDocumentoPeticion && !this.archivoDocumentacionPeticion) return;
            if (this.archivoDocumentacionPeticion) {
                this.estaCargando = true;
                if (this.archivoDocumentacionPeticionTipo == 1) {
                    const reader = new FileReader();
                    reader.readAsDataURL(this.archivoDocumentacionPeticion);
                    reader.onload = async () => {
                        let img: any = reader.result;
                        this.img = this._sanitizer.bypassSecurityTrustResourceUrl(img)
                        this.modalService.open(this.modalImagen, { ariaLabelledBy: 'modal-basic-title', backdrop: 'static' });
                    };
                } else {

                    var elemento = this.argWordToPdf.find(f => f.tipo == tipo);

                    if (elemento !== undefined && elemento?.data?.length > 0) {

                        this.modalVistaPrevia = this.modalService.open(ModuloVisorWordPdfComponent, { size: "lg", });
                        this.modalVistaPrevia.componentInstance.datosDocumento = elemento.data;

                    } else {
                        let base64 = await this.blobToData(this.archivoDocumentacionPeticion);
                        const modalRef = this.modalService.open(ModuloVisorPdfComponent, {
                            size: "lg",
                        });
                        modalRef.componentInstance.src = base64.split(",")[1];
                        modalRef.componentInstance.esWord = this.archivoDocumentacionPeticionTipo == 2 ? false : true;

                    }
                }

            } else {
                let respuesta = await this.detalleDesacoplado(this.id_tramite, 31);

                this.estaCargando = false;

                if (respuesta == null) return
                this.img = this._sanitizer.bypassSecurityTrustResourceUrl('data:image/jpeg;base64,' + respuesta)
                this.modalService.open(this.modalImagen, { ariaLabelledBy: 'modal-basic-title', backdrop: 'static' });
            }
            this.estaCargando = false;
        }

        if (tipo == 32) {

            if (!this.idDocumentoAnexo1 && !this.archivoDocumentoAnexo1) return;
            if (this.archivoDocumentoAnexo1) {
                if (this.archivoDocumentoAnexo1Tipo == 1) {
                    const reader = new FileReader();
                    reader.readAsDataURL(this.archivoDocumentoAnexo1);
                    reader.onload = async () => {
                        let img: any = reader.result;
                        this.img = this._sanitizer.bypassSecurityTrustResourceUrl(img)
                        this.modalService.open(this.modalImagen, { ariaLabelledBy: 'modal-basic-title', backdrop: 'static' });
                    };
                    return;
                } else {

                    var elemento = this.argWordToPdf.find(f => f.tipo == tipo);

                    if (elemento !== undefined && elemento?.data?.length > 0) {

                        this.modalVistaPrevia = this.modalService.open(ModuloVisorWordPdfComponent, { size: "lg", });
                        this.modalVistaPrevia.componentInstance.datosDocumento = elemento.data;

                    } else {
                        let base64 = await this.blobToData(this.archivoDocumentoAnexo1);
                        const modalRef = this.modalService.open(ModuloVisorPdfComponent, {
                            size: "lg",
                        });
                        modalRef.componentInstance.src = base64.split(",")[1]
                        modalRef.componentInstance.esWord = this.archivoDocumentoAnexo1Tipo == 2 ? false : true;

                    }
                    return;
                }
            } else {
                let respuesta = await this.detalleDesacoplado(this.id_tramite, 32);
                if (respuesta == null) return
                this.img = this._sanitizer.bypassSecurityTrustResourceUrl('data:image/jpeg;base64,' + respuesta)
                this.modalService.open(this.modalImagen, { ariaLabelledBy: 'modal-basic-title', backdrop: 'static' });
            }
        }

        if (tipo == 33) {
            if (!this.idDocumentoAnexo2 && !this.archivoDocumentoAnexo2) return;
            if (this.archivoDocumentoAnexo2) {
                if (this.archivoDocumentoAnexo2Tipo == 1) {
                    const reader = new FileReader();
                    reader.readAsDataURL(this.archivoDocumentoAnexo2);
                    reader.onload = async () => {
                        let img: any = reader.result;
                        this.img = this._sanitizer.bypassSecurityTrustResourceUrl(img)
                        this.modalService.open(this.modalImagen, { ariaLabelledBy: 'modal-basic-title', backdrop: 'static' });
                    };
                    return;
                } else {

                    var elemento = this.argWordToPdf.find(f => f.tipo == tipo);

                    if (elemento !== undefined && elemento?.data?.length > 0) {

                        this.modalVistaPrevia = this.modalService.open(ModuloVisorWordPdfComponent, { size: "lg", });
                        this.modalVistaPrevia.componentInstance.datosDocumento = elemento.data;

                    } else {
                        let base64 = await this.blobToData(this.archivoDocumentoAnexo2);
                        const modalRef = this.modalService.open(ModuloVisorPdfComponent, {
                            size: "lg",
                        });
                        modalRef.componentInstance.src = base64.split(",")[1]
                        modalRef.componentInstance.esWord = this.archivoDocumentoAnexo2Tipo == 2 ? false : true;

                    }
                    return;
                }
            } else {
                let respuesta = await this.detalleDesacoplado(this.id_tramite, 33);
                if (respuesta == null) return
                this.img = this._sanitizer.bypassSecurityTrustResourceUrl('data:image/jpeg;base64,' + respuesta)
                this.modalService.open(this.modalImagen, { ariaLabelledBy: 'modal-basic-title', backdrop: 'static' });
            }
        }

        if (tipo == 34) {
            if (!this.archivoDocumentacionAutorizar) return;
            let base64 = await this.blobToData(this.archivoDocumentacionAutorizar);
            const modalRef = this.modalService.open(ModuloVisorPdfComponent, {
                size: "lg",
            });
            modalRef.componentInstance.src = base64.split(",")[1]
        }

        this.estaCargando = false;
    }

    blobToData = (blob: Blob): any => {
        return new Promise((resolve) => {
            const reader = new FileReader()
            reader.onloadend = () => resolve(reader.result)
            reader.readAsDataURL(blob)
        });
    }

    openAdvertenciaInterno(idDocumento?, tipo?) {
        this.modalrefAdvertencia = this.modalService.open(ModuloModalAdvertenciaComponent, { ariaLabelledBy: "modal-basic-title", });
        this.modalrefAdvertencia.componentInstance.mensajeTitulo = "Eliminación del Documento";
        this.modalrefAdvertencia.componentInstance.mensaje = "¿Está seguro que desea eliminar el documento?";
        this.modalrefAdvertencia.result.then(async (result) => {
            if (result) {
                // await this.eliminar(idDocumento, tipo);
                if (tipo == 31) {
                    if (idDocumento == null) {
                        this.formGroup.controls['peticion'].setValue(null);
                        this.archivoDocumentacionPeticion = null;
                        return;
                    }
                    // await this.eliminar(idDocumento, tipo);
                    this.idDocumentoPeticion = null;
                    this.formGroup.controls['peticion'].setValue(null)
                }
                if (tipo == 32) {
                    if (idDocumento == null) {
                        this.formGroup.controls['primerDocumento'].setValue(null);
                        this.archivoDocumentoAnexo1 = null;
                        return;
                    }
                    // await this.eliminar(idDocumento, tipo);
                    this.idDocumentoAnexo1 = null;
                    this.formGroup.controls['primerDocumento'].setValue(null)
                }
                if (tipo == 33) {
                    if (idDocumento == null) {
                        this.formGroup.controls['segundoDocumento'].setValue(null);
                        this.archivoDocumentoAnexo2 = null;
                        return;
                    }
                    // await this.eliminar(idDocumento, tipo);
                    this.idDocumentoAnexo2 = null;
                    this.formGroup.controls['segundoDocumento'].setValue(null)
                }
            }
        });
    }


    async detalleDesacoplado(id: number, archivoTramite: number, tipo?) {

        let respuesta = await this.webRestService.getAsync(this.modelo_configuracion.serviciosOperaciones + "/ConsultaArchivo/Get?id=" + id + "&idArchivoTramite=" + archivoTramite)

        if (tipo) {
            return respuesta.response[0].ruta;
        }

        let tipoDoc = respuesta.response[0].ext.includes("pdf") ? 1 : respuesta.response[0].ext.includes("docx") ? 2 : 3;

        if (tipoDoc == 3) return respuesta.response[0].ruta;

        if (tipoDoc === 2) {
            var archivoTemporal = this.conversionPdf.convertBase64ToFile(respuesta.response[0].ruta);
            var htmlWordToPdf = await this.conversionPdf.convertWordToPdfAsync(archivoTemporal);
            this.modalVistaPrevia = this.modalService.open(ModuloVisorWordPdfComponent, { size: "lg", });
            this.modalVistaPrevia.componentInstance.datosDocumento = htmlWordToPdf;
        } else if (respuesta != null) {
            const modalRef = this.modalService.open(ModuloVisorPdfComponent, {
                size: "lg",
            });
            modalRef.componentInstance.src = respuesta.response[0].ruta;
            modalRef.componentInstance.esWord = tipoDoc == 2 ? true : false;
            modalRef.componentInstance.esImagen = tipoDoc == 3 ? true : false;
        }
    }
}
