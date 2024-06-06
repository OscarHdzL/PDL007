import { NgModule } from "@angular/core";

import { Routes, RouterModule } from "@angular/router";

// Layouts

import { AuthGuard } from "./guards/AuthGuard";
import { ModuloInicioComponent } from "./@Operaciones/modulo-inicio/modulo-inicio.component";
import { ModuloUsuarioSistemaRegistroComponent } from "./@Operaciones/modulo-usuario-registro/modulo-usuario-registro.component";
import { ModuloAutenticacionComponent } from "./@Operaciones/modulo-autenticacion/modulo-autenticacion.component";
import { ModuloConsultaCorreoComponent } from "./@Operaciones/modulo-consulta-correo/modulo-consulta-correo.component";
import { ModuloCambioContrasenaComponent } from "./@Operaciones/modulo-cambio-contrasena/modulo-cambio-contrasena.component";
import { ModuloCorreoConfirmacionComponent } from "./@Operaciones/modulo-correo-confirmacion/modulo-correo-confirmacion.component";
import { HomeComponent } from "./home/home.component";
import { ModuloListCatalogosJuridicaComponent } from "./@Operaciones/modulo-gestion-catalogos/modulo-list-catalogos-sjuridica/modulo-list-catalogos-sjuridica.component";
import { ModuloListCatalogosAvisoAperturaComponent } from "./@Operaciones/modulo-gestion-catalogos-aviso-apertura/modulo-list-catalogos-aviso-apertura/modulo-list-catalogos-aviso-apertura.component";
import { ModuloListConstanciaNotarioComponent } from "./@Operaciones/modulo-gestion-catalogos-const-not/modulo-list-const-not/modulo-list-constancia-notario.component";
import { ModuloListColoniaComponent } from "./@Operaciones/modulo-gestion-catalgos-colonia/modulo-list-colonia/modulo-list-colonia.component";
import { ModuloTramiteComponent } from "./@Operaciones/modulo-tramite/modulo-tramite.component";
import { ModuloSolicitudPrimerPasoComponent } from "./@Operaciones/modulo-tramite/modulo-solicitud-primer-paso/modulo-solicitud-primer-paso.component";
import { ModuloSolicitudSegundoPasoComponent } from "./@Operaciones/modulo-tramite/modulo-solicitud-segundo-paso/modulo-solicitud-segundo-paso.component";
import { ModuloSolicitudTercerPasoComponent } from "./@Operaciones/modulo-tramite/modulo-solicitud-tercer-paso/modulo-solicitud-tercer-paso.component";
import { ModuloSolicitudCuartoPasoComponent } from "./@Operaciones/modulo-tramite/modulo-solicitud-cuarto-paso/modulo-solicitud-cuarto-paso.component";
import { ModuloSolicitudQuintoPasoComponent } from "./@Operaciones/modulo-tramite/modulo-solicitud-quinto-paso/modulo-solicitud-quinto-paso.component";
import { ModuloReporteRegistroComponent } from "./@Operaciones/modulo-reporte-registro/modulo-reporte-registro.component";
import { ModuloListCatalogoCredoComponent } from "./@Operaciones/modulo-gestion-catalogos-credo/modulo-list-catalogo-credo/modulo-list-catalogo-credo.component";
import { ModuloSolicitudSextoPasoComponent } from "./@Operaciones/modulo-tramite/modulo-solicitud-sexto-paso/modulo-solicitud-sexto-paso.component";
import { ModuloTramiteTomaNotaComponent } from "./@Operaciones/modulo-tramite-toma-nota/modulo-tramite-toma-nota.component";
import { ModuloSolicitudTomaNotaComponent } from "./@Operaciones/modulo-tramite-toma-nota/modulo-solicitud-toma-nota/modulo-solicitud-toma-nota.component";
import { ModuloSolicitudMovimientosEstatutosComponent } from "./@Operaciones/modulo-tramite-toma-nota/modulo-solicitud-movimientos-estatutos/modulo-solicitud-movimientos-estatutos.component";
import { ModuloSolicitudMovimientosDenominacionComponent } from "./@Operaciones/modulo-tramite-toma-nota/modulo-solicitud-movimientos-denominacion/modulo-solicitud-movimientos-denominacion.component";
import { ModuloSolicitudMovimientosRelacionMiembrosComponent } from "./@Operaciones/modulo-tramite-toma-nota/modulo-solicitud-movimientos-relacion-miembros/modulo-solicitud-movimientos-relacion-miembros.component";
import { ModuloSolicitudRepresentanteLegalComponent } from "./@Operaciones/modulo-tramite-toma-nota/modulo-solicitud-representante-legal/modulo-solicitud-representante-legal.component";
import { ModuloSolicitudApoderadoLegalComponent } from "./@Operaciones/modulo-tramite-toma-nota/modulo-solicitud-apoderado-legal/modulo-solicitud-apoderado-legal.component";
import { ModuloSolicitudDomicilioLegalComponent } from "./@Operaciones/modulo-tramite-toma-nota/modulo-solicitud-domicilio-legal/modulo-solicitud-domicilio-legal.component";
import { ModuloSolicitudDomicilioNotificacionComponent } from "./@Operaciones/modulo-tramite-toma-nota/modulo-solicitud-domicilio-notificacion/modulo-solicitud-domicilio-notificacion.component";
import { ModuloUsuarioSistemaComponent } from "./@Operaciones/modulo-usuario-sistema/modulo-usuario-sistema.component";
import { ModuloUsuarioSistemaPerfilRegistroComponent } from "./@Operaciones/modulo-usuario-sistema/modulo-usuario-sistema-perfil-registro/modulo-usuario-sistema-perfil-registro.component";
import { ModuloUsuarioSistemaPerfilActualizaComponent } from "./@Operaciones/modulo-usuario-sistema/modulo-usuario-sistema-perfil-actualiza/modulo-usuario-sistema-perfil-actualiza.component";
import { ModuloAsignacionRegistroComponent } from "./@Operaciones/modulo-asignacion-registro/modulo-asignacion-registro.component";
import { ModuloListCatalogoPaisoComponent } from './@Operaciones/modulo-gestion-catalogos-paiso/modulo-list-catalogo-paiso/modulo-list-catalogo-paiso.component';
import { ModuloAtenderRegistrosComponent } from "./@Operaciones/modulo-atender-registros/modulo-atender-registros.component";
import { ModuloCotejoDocumentacionComponent } from "./@Operaciones/modulo-tramite/modulo-cotejo-documentacion/modulo-cotejo-documentacion.component";
import { ModuloCotejoDocumentacionFisicaComponent } from "./@Operaciones/modulo-tramite/modulo-cotejo-documentacion-fisica/modulo-cotejo-documentacion-fisica.component";
import { ModuloFinalizacionCotejoComponent } from "./@Operaciones/modulo-tramite/modulo-finalizacion-cotejo/modulo-finalizacion-cotejo.component";
import { ModuloCotejoConcluidoComponent } from "./@Operaciones/modulo-tramite/modulo-cotejo-concluido/modulo-cotejo-concluido.component";
import { ModuloConsultaTramiteComponent } from "./@Operaciones/modulo-tramite/modulo-consulta-tramite/modulo-consulta-tramite.component";
import { ModuloAsignacionTomaNotaComponent } from './@Operaciones/modulo-asignacion-toma-nota/modulo-asignacion-toma-nota.component';
import { ModuloAtenderTomaNotaComponent } from './@Operaciones/modulo-atender-toma-nota/modulo-atender-toma-nota.component';
import { ModuloAtencionTomaNotaComponent } from "./@Operaciones/modulo-tramite-toma-nota/modulo-atencion-toma-nota/modulo-atencion-toma-nota.component";
import { ModuloConsultaTomaNotaComponent } from "./@Operaciones/modulo-tramite-toma-nota/modulo-consulta-toma-nota/modulo-consulta-toma-nota.component";
import { ModuloTransmisionesComponent } from "./@Operaciones/modulo-transmisiones/modulo-transmisiones.component";
import { ModuloSolicitudTransmisionComponent } from "./@Operaciones/modulo-transmisiones/modulo-solicitud-transmision/modulo-solicitud-transmision.component";
import { ModuloAsignacionTransmisionesComponent } from "./@Operaciones/modulo-transmisiones/modulo-asignacion-transmisiones/modulo-asignacion-transmisiones.component";
import { ModuloListCatalogoDirectorComponent } from './@Operaciones/modulo-gestion-catalogos-director/modulo-list-catalogo-director/modulo-list-catalogo-director.component';
import { ModuloListCatalogoEmisoraComponent } from './@Operaciones/modulo-gestion-catalogos-emisoras/modulo-list-catalogo-emisora/modulo-list-catalogo-emisora.component';
import { ModuloReporteTransmisionesComponent } from "./@Operaciones/modulo-reporte-transmisiones/modulo-reporte-transmisiones.component";
import { ModuloAtencionTransmisionesComponent } from "./@Operaciones/modulo-transmisiones/modulo-atencion-transmisiones/modulo-atencion-transmisiones.component";
import { ModuloConsultaTransmisionComponent } from "./@Operaciones/modulo-transmisiones/modulo-consulta-transmisiones/modulo-consulta-transmision.component";
import { ModuloConsultaTransmisionPublicoComponent } from "./@Operaciones/modulo-transmisiones/modulo-consulta-transmision/modulo-consulta-transmision-publico.component";
import { ModuloRegistrosTramiteComponent } from "./@Operaciones/modulo-registros-tramite/modulo-registros-tramite.component";
import { ModuloRegistrosTomaNotaComponent } from "./@Operaciones/modulo-registros-toma-nota/modulo-registros-toma-nota.component";
import { ModuloGestionCatalogoPlantillaTransmisionComponent } from "./@Operaciones/modulo-gestion-catalogo-plantilla-transmision/modulo-gestion-catalogo-plantilla-transmision.component";
import { ModuloRegistrosTransmisionComponent } from "./@Operaciones/modulo-registros-transmision/modulo-registros-transmision.component";
import { ModuloRegistrosTramiteDictaminadorComponent } from "./@Operaciones/modulo-registros-tramite-dictaminador/modulo-registros-tramite-dictaminador.component";
import { ModuloRegistrosTomaNotaDictaminadorComponent } from "./@Operaciones/modulo-registros-toma-nota-dictaminador/modulo-registros-toma-nota-dictaminador.component";
import { ModuloRegistrosTransmisionDictaminadorComponent } from "./@Operaciones/modulo-registros-transmision-dictaminador/modulo-registros-transmision-dictaminador.component";
import { ModuloDeclaratoriaProcedenciaComponent } from "./@Operaciones/modulo-declaratoria-procedencia/solicitud-declaratoria-procedencia/modulo-declaratoria-procedencia.component";
import { ModuloFormularioRegistroComponent } from "./@Operaciones/modulo-declaratoria-procedencia/solicitud-declaratoria-procedencia/modulo-formulario-registro/modulo-formulario-registro.component";
import { ModuloVistaPrincipalComponent } from "./@Operaciones/modulo-declaratoria-procedencia/modulo-vista-principal/modulo-vista-principal.component";
import { ModuloComentariosComponent } from "./@Operaciones/modulo-declaratoria-procedencia/components/modulo-comentarios/modulo-comentarios.component";
import { ModuloNuevoReporteComponent } from "./@Operaciones/modulo-declaratoria-procedencia/modulo-reporte/modulo-reporte.component";
import { ModuloAdminPlantillasComponent } from "./@Operaciones/modulo-declaratoria-procedencia/modulo-administracion-plantillas/modulo-administracion-plantillas.component";
import { RefactorModuloTramitesComponent } from "./@Operaciones/refactor-modulo-tramites/refactor-modulo-tramites.component";
import { SesionActiveGuard } from "./guards/SesionActive.guard";



const routes: Routes = [
  {
    path: "",
    component: HomeComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "iniciar-sesion",
    component: ModuloAutenticacionComponent,
  },
  {
    path: "tramites-electronicos",
    component: ModuloInicioComponent,
  },
  {
    path: "consulta-correo",
    component: ModuloConsultaCorreoComponent,
  },
  {
    path: "recuperar-contrasena/:token",
    component: ModuloCambioContrasenaComponent,
  },
  {
    path: "cambio-contrasena",
    component: ModuloCambioContrasenaComponent,
  },
  {
    path: "registro",
    component: ModuloUsuarioSistemaRegistroComponent,
  },
  {
    path: "confirmar-correo/:iduser",
    component: ModuloCorreoConfirmacionComponent,
  },
  {
    path: "catalogos/situacion-juridica",
    component: ModuloListCatalogosJuridicaComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "catalogos/aviso-apertura",
    component: ModuloListCatalogosAvisoAperturaComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "catalogos/constancia-notario",
    component: ModuloListConstanciaNotarioComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "catalogos/colonias",
    component: ModuloListColoniaComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "catalogos/credo",
    component: ModuloListCatalogoCredoComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "catalogos/director",
    component: ModuloListCatalogoDirectorComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "catalogos/emisora",
    component: ModuloListCatalogoEmisoraComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "catalogos/paisorigen",
    component: ModuloListCatalogoPaisoComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "catalogos/plantilla",
    component: ModuloGestionCatalogoPlantillaTransmisionComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "tramites",
    component: ModuloTramiteComponent,
    canActivate: [SesionActiveGuard]
  },
  { path: "solicitudregistro", redirectTo: "solicitudregistro/", pathMatch: "full" },
  {
    path: "solicitudregistro/:Id",
    component: ModuloSolicitudPrimerPasoComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "solicitudescrito/domiciliolegal/:Id",
    component: ModuloSolicitudSegundoPasoComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "solicitudescrito/relacioninmueble/:Id",
    component: ModuloSolicitudTercerPasoComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "solicitudescrito/especificacionesterreno/:Id",
    component: ModuloSolicitudCuartoPasoComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "solicitudescrito/representantes/:Id",
    component: ModuloSolicitudQuintoPasoComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "solicitudescrito/constancianotario/:Id",
    component: ModuloSolicitudSextoPasoComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "tomanota",
    component: ModuloTramiteTomaNotaComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "solicitudtomanota/:Id",
    component: ModuloSolicitudTomaNotaComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "solicitudtomanotaconsulta/:Id",
    component: ModuloSolicitudTomaNotaComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "movimientos-estatutos",
    component: ModuloSolicitudMovimientosEstatutosComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "movimientos-denominacion",
    component: ModuloSolicitudMovimientosDenominacionComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "movimientos-miembros",
    component: ModuloSolicitudMovimientosRelacionMiembrosComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "movimientos-representante",
    component: ModuloSolicitudRepresentanteLegalComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "movimientos-apoderado",
    component: ModuloSolicitudApoderadoLegalComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "movimientos-domicilio-legal",
    component: ModuloSolicitudDomicilioLegalComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "movimientos-domicilio-notificacion",
    component: ModuloSolicitudDomicilioNotificacionComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "usuario-sistema",
    component: ModuloUsuarioSistemaComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "atencion/registro",
    component: ModuloAtenderRegistrosComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "asignacion/registro-asociacion",
    component: ModuloAsignacionRegistroComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "asignacion/toma-nota",
    component: ModuloAsignacionTomaNotaComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "usuario-sistema/nuevo-usuario",
    component: ModuloUsuarioSistemaPerfilRegistroComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "atencion/validardocumentacion/:Id",
    component: ModuloCotejoDocumentacionComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "atencion/validardocumentacionfisica/:Id",
    component: ModuloCotejoDocumentacionFisicaComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "atencion/finalizacion-cotejo/:Id",
    component: ModuloFinalizacionCotejoComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "atencion/registro-concluido/:Id",
    component: ModuloCotejoConcluidoComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "usuario-sistema/editar-usuario-sistema/:id",
    component: ModuloUsuarioSistemaPerfilActualizaComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "consulta-registro",
    component: ModuloConsultaTramiteComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "consulta-registro-tramite/:Id",
    component: ModuloConsultaTramiteComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "reporte-registro",
    component: ModuloReporteRegistroComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "reporte-toma-nota",
    component: ModuloReporteRegistroComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "reporte-transmisiones",
    component: ModuloReporteTransmisionesComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "atencion/toma-nota",
    component: ModuloAtenderTomaNotaComponent,
    canActivate: [SesionActiveGuard]
    // canActivate: [AuthGuard]
  },
  {
    path: "atencion/toma-nota/:id",
    component: ModuloAtencionTomaNotaComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "consulta-toma-nota/:Id",
    component: ModuloConsultaTomaNotaComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "transmisiones",
    component: ModuloTransmisionesComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "consulta-transmision-Id/:Id",
    component: ModuloConsultaTransmisionPublicoComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "solicitud-transmision",
    component: ModuloSolicitudTransmisionComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "solicitud-transmision/:transmision",
    component: ModuloSolicitudTransmisionComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "asignacion-transmision",
    component: ModuloAsignacionTransmisionesComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "atencion-transmision",
    component: ModuloAtencionTransmisionesComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "registros-tramite",
    component: ModuloRegistrosTramiteComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "registros-toma-nota",
    component: ModuloRegistrosTomaNotaComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "registros-transmision",
    component: ModuloRegistrosTransmisionComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "registros-tramite-dictaminador",
    component: ModuloRegistrosTramiteDictaminadorComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "registros-tnota-dictaminador",
    component: ModuloRegistrosTomaNotaDictaminadorComponent,
    canActivate: [SesionActiveGuard]

  },
  {
    path: "registros-transmision-dictaminador",
    component: ModuloRegistrosTransmisionDictaminadorComponent,
    canActivate: [SesionActiveGuard]

  },
  {
    path: "consulta-transmision/:transmision",
    component: ModuloConsultaTransmisionComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "declaratoria-procedencia",
    component: ModuloDeclaratoriaProcedenciaComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "alta-declaratoria",
    component: ModuloFormularioRegistroComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "alta-declaratoria/:declaratoria",
    component: ModuloFormularioRegistroComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "vista-principal-declaratoria",
    component: ModuloVistaPrincipalComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "comentarios",
    component: ModuloComentariosComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "reportes",
    component: ModuloNuevoReporteComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "admin-plantillas",
    component: ModuloAdminPlantillasComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "alta-solicitud-registro",
    component: RefactorModuloTramitesComponent,
    canActivate: [SesionActiveGuard]
  },
  {
    path: "",
    component: ModuloAdminPlantillasComponent
  },



  { path: "**", redirectTo: "inicio" },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
