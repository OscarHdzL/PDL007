using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Modelos;
using Modelos.Modelos.Response;
using Modelos.Modelos.Utilidades.Response;
using System.IO;
namespace Conexion
{
    public class Contexto : DbContext
    {

        #region DBQuery
        public DbSet<InicioSesionResponse> InicioSesionResponse { get; set; }
        public DbSet<InsertarUsuarioSistemaResponse> InsertarUsuarioSistemaResponse { get; set; }
        public DbSet<ConfirmarCorreoResponse> ConfirmarCorreoResponse { get; set; }
        public DbSet<RecuperarContrasenaResponse> RecuperarContrasenaResponse { get; set; }
        public DbSet<BorraCatalogoSJuridicaResponse> BorraCatalogoSJuridicaResponse { get; set; }
        public DbSet<BorraCatalogoAvisoAperturaResponse> BorraCatalogoAvisoAperturaResponse { get; set; }
        public DbSet<CatalogoAvisoAperturaInsertResponse> CatalogoAvisoAperturaInsertResponse { get; set; }
        public DbSet<CatalogoCnotarioarrInsertResponse> CatalogoCnotarioarrInsertResponse { get; set; }
        public DbSet<BorraCatalogoCnotarioarrResponse> BorraCatalogoCnotarioarrResponse { get; set; }
        public DbSet<CatalogoSJuridicaInsertResponse> CatalogoSJuridicaInsertResponse { get; set; }
        public DbSet<ConsultaListaCaalogoSJuridicaResponse> ConsultaListaCaalogoSJuridicaResponse { get; set; }
        public DbSet<ActualizarCatalogoSJuridicaResponse> ActualizarCatalogoSJuridicaResponse { get; set; }
        public DbSet<ActualizarCatalogoAvisoAperturaResponse> ActualizarCatalogoAvisoAperturaResponse { get; set; }
        public DbSet<ActualizarCatalogoCnotarioarrResponse> ActualizarCatalogoCnotarioarrResponse { get; set; }
        public DbSet<ConsultaListaUsuariosSistemaResponse> ConsultaListaUsuariosSistemaResponse { get; set; }
        public DbSet<BorraUsuarioSistemaResponse> BorraUsuarioSistemaResponse { get; set; }
        public DbSet<ConsultaListaPerfilesResponse> ConsultaListaPerfilesResponse { get; set; }
        public DbSet<ConsultaDetalleUsuarioSistemaResponse> ConsultaDetalleUsuarioSistemaResponse { get; set; }
        public DbSet<ActualizarUsuarioSistemaResponse> ActualizarUsuarioSistemaResponse { get; set; }
        public DbSet<ConsultaListaCatalogoCnotarioarrResponse> ConsultaListaCatalogoCnotarioarrResponse { get; set; }
        public DbSet<ConsultaListaCatalogoAvisoAperturaResponse> ConsultaListaCatalogoAvisoAperturaResponse { get; set; }
        public DbSet<CatalogoColoniaInsertResponse> CatalogoColoniaInsertResponse { get; set; }
        public DbSet<BorraCatalogoColoniaResponse> BorraCatalogoColoniaResponse { get; set; }
        public DbSet<ActualizarCatalogoColoniaResponse> ActualizarCatalogoColoniaResponse { get; set; }
        public DbSet<ConsultaListaCatalogoColoniaResponse> ConsultaListaCatalogoColoniaResponse { get; set; }
        public DbSet<ConsultaListaCatalogoMunicipioResponse> ConsultaListaCatalogoMunicipioResponse { get; set; }
        public DbSet<ConsultaListaCatalogoCPResponse> ConsultaListaCatalogoCPResponse { get; set; }
        public DbSet<ConsultaListaCatalogoTSolRegaResponse> ConsultaListaCatalogoTSolRegaResponse { get; set; }
        public DbSet<ConsultaListaCatalogoPaisoResponse> ConsultaListaCatalogoPaisoResponse { get; set; }
        public DbSet<ConsultaListaCatalogoCredoResponse> ConsultaListaCatalogoCredoResponse { get; set; }
        public DbSet<InsertarTramitePasoUnoResponse> InsertarTramitePasoUnoResponse { get; set; }
        public DbSet<ConsultaDetalleTramitePasoUnoResponse> ConsultaDetalleTramitePasoUnoResponse { get; set; }
        public DbSet<ConsultaEstadosResponse> ConsultaEstadosResponse { get; set; }
        public DbSet<ConsultaMunicipiosResponse> ConsultaMunicipiosResponse { get; set; }
        public DbSet<ConsultaEstatusResponse> ConsultaEstatusResponse { get; set; }
        public DbSet<ReporteResponse> ReporteResponse { get; set; }
        public DbSet<InsertarTramitePasoDosResponse> InsertarTramitePasoDosResponse { get; set; }
        public DbSet<ActualizarTramitePasoDosResponse> ActualizarTramitePasoDosResponse { get; set; }
        public DbSet<ConsultaDetalleTramitePasoDosResponse> ConsultaDetalleTramitePasoDosResponse { get; set; }
        public DbSet<ConsultaDetalleTramitePasoTresResponse> ConsultaDetalleTramitePasoTresResponse { get; set; }
        public DbSet<ActualizarTramitePasoTresResponse> ActualizarTramitePasoTresResponse { get; set; }
        public DbSet<ActualizarTramitePasoUnoResponse> ActualizarTramitePasoUnoResponse { get; set; }
        public DbSet<ActualizarTramitePasoCuatroResponse> ActualizarTramitePasoCuatroResponse { get; set; }
        public DbSet<ConsultaDetalleTramitePasoCuatroResponse> ConsultaDetalleTramitePasoCuatroResponse { get; set; }
        public DbSet<CatalogoCredoInsertResponse> CatalogoCredoInsertResponse { get; set; }
        public DbSet<BorraCatalogoCredoResponse> BorraCatalogoCredoResponse { get; set; }
        public DbSet<ActualizarCatalogoCredoResponse> ActualizarCatalogoCredoResponse { get; set; }
        public DbSet<ConsultaDetalleTramitePasoQuintoResponse> ConsultaDetalleTramitePasoQuintoResponse { get; set; }
        public DbSet<ConsultaDetalleTramitePasoSextoResponse> ConsultaDetalleTramitePasoSextoResponse { get; set; }
        public DbSet<ActualizarTramitePasoSextoResponse> ActualizarTramitePasoSextoResponse { get; set; }
        public DbSet<InsertarTramitePasoCincoResponse> InsertarTramitePasoCincoResponse { get; set; }
        public DbSet<ArchivoResponse> ArchivoResponse { get; set; }
        public DbSet<ConsultaDetalleTramiteIdResponse> ConsultaDetalleTramiteIdResponse { get; set; }
        public DbSet<BorraRepresentanteResponse> BorraRepresentanteResponse { get; set; }
        public DbSet<ConsultaListaTipoRepresentanteResponse> ConsultaListaTipoRepresentanteResponse { get; set; }
        public DbSet<ConsultaListaCatalogoCotejoResponse> ConsultaListaCatalogoCotejoResponse { get; set; }
        public DbSet<ConsultaModulosPerfilResponse> ConsultaModulosPerfilResponse { get; set; }
        public DbSet<ConteoColoniaResponse> ConteoColoniaResponse { get; set; }
        public DbSet<ConteoTramitesResponse> ConteoTramitesResponse { get; set; }
        public DbSet<ConsultaListaTramitesResponse> ConsultaListaTramitesResponse { get; set; }
        public DbSet<ConsultaListaUsuariosDicataminadorResponse> ConsultaListaUsuariosDicataminadorResponse { get; set; }
        public DbSet<AsignarDictaminadorRegistroResponse> AsignarDictaminadorRegistroResponse { get; set; }
        public DbSet<FinalizarTramiteResponse> FinalizarTramiteResponse { get; set; }
        public DbSet<CatalogoPaisoInsertResponse> CatalogoPaisoInsertResponse { get; set; }
        public DbSet<BorraCatalogoPaisoResponse> BorraCatalogoPaisoResponse { get; set; }
        public DbSet<ActualizarCatalogoPaisoResponse> ActualizarCatalogoPaisoResponse { get; set; }
        public DbSet<ConsultaListaEstatusResponse> ConsultaListaEstatusResponse { get; set; }
        public DbSet<ActualizarCotejoResponse> ActualizarCotejoResponse { get; set; }
        public DbSet<ConsultaDetalleCotejoResponse> ConsultaDetalleCotejoResponse { get; set; }
        public DbSet<ConsultaDetalleCotejoPublicoResponse> ConsultaDetalleCotejoPublicoResponse { get; set; }

        public DbSet<ConsultaDetalleTomaNotaResponse> ConsultaDetalleTomaNotaResponse { get; set; }
        public DbSet<ConsultaDetalleTomaNotaDomicilioLegalResponse> ConsultaDetalleTomaNotaDomicilioLegalResponse { get; set; }
        public DbSet<ActualizarTomaNotaDomicilioResponse> ActualizarTomaNotaDomicilioResponse { get; set; }
        public DbSet<ActualizarTomaNotaEstatutosDenominacionResponse> ActualizarTomaNotaEstatutosDenominacionResponse { get; set; }
        public DbSet<ConsultaDetalleTomaNotaRepresentantesResponse> ConsultaDetalleTomaNotaRepresentantesResponse { get; set; }
        public DbSet<InsertarTomaNotaRepresentanteResponse> InsertarTomaNotaRepresentanteResponse { get; set; }
        public DbSet<ConsultaListaCatalogosTMovimientosResponse> ConsultaListaCatalogosTMovimientosResponse { get; set; }
        public DbSet<ConsultaListaCatalogosPoderesResponse> ConsultaListaCatalogosPoderesResponse { get; set; }
        public DbSet<InsertarTomaNotaRepresentanteLegalResponse> InsertarTomaNotaRepresentanteLegalResponse { get; set; }
        public DbSet<ConsultaDetalleTomaNotaRepresentanteLegalResponse> ConsultaDetalleTomaNotaRepresentanteLegalResponse { get; set; }
        public DbSet<ConsultaDetalleTomaNotaApoderadoResponse> ConsultaDetalleTomaNotaApoderadoResponse { get; set; }
        public DbSet<InsertarTomaNotaApoderadoLegalResponse> InsertarTomaNotaApoderadoLegalResponse { get; set; }
        public DbSet<ActualizarSolicitudEscritoTomaNotaResponse> ActualizarSolicitudEscritoTomaNotaResponse { get; set; }
        public DbSet<FinalizarTomaNotaResponse> FinalizarTomaNotaResponse { get; set; }
        public DbSet<ConsultaListaUsuariosDictaminadorTomaNotaResponse> ConsultaListaUsuariosDictaminadorTomaNotaResponse { get; set; }
        public DbSet<ConsultaListaTomaNotaResponse> ConsultaListaTomaNotaResponse { get; set; }
        public DbSet<ConteoTomaNotaResponse> ConteoTomaNotaResponse { get; set; }
        public DbSet<AsignarDictaminadorTomaNotaResponse> AsignarDictaminadorTomaNotaResponse { get; set; }
        public DbSet<ConsultaDetalleMovimientosTomaNotaResponse> ConsultaDetalleMovimientosTomaNotaResponse { get; set; }
        public DbSet<ConsultaCatalogoMovimientosTomaNotaResponse> ConsultaCatalogoMovimientosTomaNotaResponse { get; set; }
        public DbSet<ConsultaPermisoPersonaResponse> ConsultaPermisoPersonaResponse { get; set; }
        public DbSet<ConsultaDetalleTomaNotaInfoResponse> ConsultaDetalleTomaNotaInfoResponse { get; set; }
        public DbSet<ConsultaHorariosBloqueoResponse> ConsultaHorariosBloqueoResponse { get; set; }
        public DbSet<ConsultaListaCatalogoDirectorResponse> ConsultaListaCatalogoDirectorResponse { get; set; }
        public DbSet<InsertarCatalogoDirectorResponse> InsertarCatalogoDirectorResponse { get; set; }
        public DbSet<ActualizarDirectorResponse> ActualizarDirectorResponse { get; set; }
        public DbSet<EliminarDirectorResponse> EliminarDirectorResponse { get; set; }
        public DbSet<CatalogoMediosTransmisionResponse> CatalogoMediosTransmisionResponse { get; set; }
        public DbSet<InsertarTransmisionActosReligiososResponse> InsertarTransmisionActosReligiososResponse { get; set; }
        public DbSet<InsertarActosFechasResponse> InsertarActosFechasResponse { get; set; }
        public DbSet<ConsultaActosReligiososResponse> ConsultaActosReligiososResponse { get; set; }
        public DbSet<ConsultaActosMediosTrasmisionResponse> ConsultaActosMediosTrasmisionResponse { get; set; }
        public DbSet<ConsultaActosFechasResponse> ConsultaActosFechasResponse { get; set; }
        public DbSet<ConsultaTramiteTransmisionResponse> ConsultaTramiteTransmisionResponse { get; set; }
        public DbSet<InsertarTramiteTransmisionResponse> InsertarTramiteTransmisionResponse { get; set; }
        public DbSet<ActualizarMediosTransmisionResponse> ActualizarMediosTransmisionResponse { get; set; }
        public DbSet<BorraMediosTransmisionResponse> BorraMediosTransmisionResponse { get; set; }
        public DbSet<ConsultaListaAnexoAsuntoResponse> ConsultaListaAnexoAsuntoResponse { get; set; }
        public DbSet<ConsultaDetalleAnexoAsuntoResponse> ConsultaDetalleAnexoAsuntoResponse { get; set; }
        public DbSet<InsertarAnexoAsuntoResponse> InsertarAnexoAsuntoResponse { get; set; }
        public DbSet<BorrarAnexoAsuntoResponse> BorrarAnexoAsuntoResponse { get; set; }
        public DbSet<BorrarAnexoBDResponse> BorrarAnexoBDResponse { get; set; }
        public DbSet<ActualizarEstatusTransmisionResponse> ActualizarEstatusTransmisionResponse { get; set; }
        public DbSet<ConsultaEstatusTransmisionResponse> ConsultaEstatusTransmisionResponse { get; set; }
        public DbSet<ConsultaListaUsuariosDictaminadorTransmisionResponse> ConsultaListaUsuariosDictaminadorTransmisionResponse { get; set; }
        public DbSet<AsignarTransmisionDictaminadorResponse> AsignarTransmisionDictaminadorResponse { get; set; }
        public DbSet<ConsultaDetalleTramiteTransmisionResponse> ConsultaDetalleTramiteTransmisionResponse { get; set; }
        public DbSet<ReporteTransmisionListaEstatusResponse> ReporteTransmisionListaEstatusResponse { get; set; }
        public DbSet<ActualizarTomaNotaComentarioResponse> ActualizarTomaNotaComentarioResponse { get; set; }
        public DbSet<ReporteTransmisionResponse> ReporteTransmisionResponse { get; set; }
        public DbSet<ConsultaDetalleTransmisionCotejoPublicoResponse> ConsultaDetalleTransmisionCotejoPublicoResponse { get; set; }
        public DbSet<ActualizarAtenderTransmisionResponse> ActualizarAtenderTransmisionResponse { get; set; }
        public DbSet<AutorizarTransmisionResponse> AutorizarTransmisionResponse { get; set; }
        public DbSet<InsertarObservacionTransmisionResponse> InsertarObservacionTransmisionResponse { get; set; }
        public DbSet<ConsultaOficioTransmisionResponse> ConsultaOficioTransmisionResponse { get; set; }
        public DbSet<InsertarActosEmisorasResponse> InsertarActosEmisorasResponse { get; set;  }
        public DbSet<EliminarFechasEmisorasResponse> EliminarFechasEmisorasResponse { get; set; }
        public DbSet<ConsultaArchivosListaResponse> ConsultaArchivosListaResponse { get; set; }
        public DbSet<ConsultaListaRegistrosTramiteResponse> ConsultaListaRegistrosTramiteResponse { get; set; }
        public DbSet<EliminarRegistroTramiteResponse> EliminarRegistroTramiteResponse { get; set; }
        public DbSet<ConsultaListaRegistrosTomaNotaResponse> ConsultaListaRegistrosTomaNotaResponse { get; set; }
        public DbSet<EliminarRegistroTomaNotaResponse> EliminarRegistroTomaNotaResponse { get; set; }
        public DbSet<ConsultaListaRegistrosTransmisionResponse> ConsultaListaRegistrosTransmisionResponse { get; set; }
        public DbSet<InsertarPlantillaDocTransmisionResponse> InsertarPlantillaDocTransmisionResponse { get; set; }
        public DbSet<ConsultarPlantillaDocTransmisionResponse> ConsultarPlantillaDocTransmisionResponse { get; set; }
        public DbSet<BorrarPlantillaDocTransmisionResponse> BorrarPlantillaDocTransmisionResponse { get; set; }
        public DbSet<EliminarRegistroTransmisionResponse> EliminarRegistroTransmisionResponse { get; set; }
        public DbSet<ActulizarPlantillaDocTransmisionResponse> ActulizarPlantillaDocTransmisionResponse { get; set; }
        public DbSet<ConsultaListaRegistrosTramiteDictaminadorResponse> ConsultaListaRegistrosTramiteDictaminadorResponse { get; set; }
        public DbSet<ConsultaListaRegistrosTNotaDictaminadorResponse> ConsultaListaRegistrosTNotaDictaminadorResponse { get; set; }
        public DbSet<ConsultaListaRegistrosTransmisionDictaminadorResponse> ConsultaListaRegistrosTransmisionDictaminadorResponse { get; set; }
        public DbSet<ReporteResponseTnota> ReporteResponseTnota { get; set; }
        public DbSet<ConsultaListaCatalogosMovRealizadosResponse> ConsultaListaCatalogosMovRealizadosResponse { get; set; }
        public DbSet<ResponseGenerico> ResponseGenerico { get; set; }
        public DbSet<CatalogoGenericoResponse> CatalogoGenericoResponse { get; set; }
        public DbSet<ConsultarTramiteDeclaratoriaPaso1> ConsultarTramiteDeclaratoriaPaso1 { get; set; }
        public DbSet<ConsultarTramiteDeclaratoriaPaso2> ConsultarTramiteDeclaratoriaPaso2 { get; set; }
        public DbSet<ConsultarTramiteDeclaratoriaPaso4> ConsultarTramiteDeclaratoriaPaso4 { get; set; }
        public DbSet<ConsultarTramiteDeclaratoriaPaso5> ConsultarTramiteDeclaratoriaPaso5 { get; set; }
        public DbSet<ConsultarTramiteDeclaratoriaAvance> ConsultarTramiteDeclaratoriaAvance { get; set; }
        public DbSet<ConsultarTramiteDeclaratoriaLista> ConsultarTramiteDeclaratoriaLista { get; set; }
        public DbSet<ReporteTramitesResponse> ReporteTramitesResponse { get; set; }
        public DbSet<PlantillaResponse> PlantillaResponse { get; set; }
        
        
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InicioSesionResponse>().HasNoKey();
            modelBuilder.Entity<ConfirmarCorreoResponse>().HasNoKey();
            modelBuilder.Entity<ValidacionRegistroResponse>().HasNoKey();
            modelBuilder.Entity<RechazoRegistroResponse>().HasNoKey();
            modelBuilder.Entity<RecuperarContrasenaResponse>().HasNoKey();
            modelBuilder.Entity<ReporteContactosResponse>().HasNoKey();
            modelBuilder.Entity<RegistroCompletoResponse>().HasNoKey();
            modelBuilder.Entity<InsertarUsuarioSistemaResponse>().HasNoKey();
            modelBuilder.Entity<InsertarConvocatoriaResponse>().HasNoKey();
            modelBuilder.Entity<ValidaCorreoResponse>().HasNoKey();
            modelBuilder.Entity<BorraCatalogoSJuridicaResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaCaalogoSJuridicaResponse>().HasNoKey();
            modelBuilder.Entity<CatalogoSJuridicaInsertResponse>().HasNoKey();
            modelBuilder.Entity<ActualizarCatalogoSJuridicaResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaUsuariosSistemaResponse>().HasNoKey();
            modelBuilder.Entity<BorraUsuarioSistemaResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaPerfilesResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaDetalleUsuarioSistemaResponse>().HasNoKey();
            modelBuilder.Entity<ActualizarUsuarioSistemaResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaCatalogoCnotarioarrResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaCatalogoAvisoAperturaResponse>().HasNoKey();
            modelBuilder.Entity<ActualizarCatalogoCnotarioarrResponse>().HasNoKey();
            modelBuilder.Entity<ActualizarCatalogoAvisoAperturaResponse>().HasNoKey();
            modelBuilder.Entity<BorraCatalogoCnotarioarrResponse>().HasNoKey();
            modelBuilder.Entity<BorraCatalogoAvisoAperturaResponse>().HasNoKey();
            modelBuilder.Entity<CatalogoCnotarioarrInsertResponse>().HasNoKey();
            modelBuilder.Entity<CatalogoAvisoAperturaInsertResponse>().HasNoKey();
            modelBuilder.Entity<ActualizarCatalogoColoniaResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaCatalogoColoniaResponse>().HasNoKey();
            modelBuilder.Entity<BorraCatalogoColoniaResponse>().HasNoKey();
            modelBuilder.Entity<CatalogoColoniaInsertResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaCatalogoMunicipioResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaCatalogoCPResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaCatalogoTSolRegaResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaCatalogoPaisoResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaCatalogoCredoResponse>().HasNoKey();
            modelBuilder.Entity<InsertarTramitePasoUnoResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaDetalleTramitePasoUnoResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaDetalleCotejoResponse>().HasNoKey();
            modelBuilder.Entity<InsertarTramitePasoDosResponse>().HasNoKey();
            modelBuilder.Entity<ActualizarTramitePasoDosResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaDetalleTramitePasoDosResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaDetalleTramitePasoTresResponse>().HasNoKey();
            modelBuilder.Entity<ActualizarTramitePasoTresResponse>().HasNoKey();
            modelBuilder.Entity<ActualizarTramitePasoUnoResponse>().HasNoKey();
            modelBuilder.Entity<ActualizarTramitePasoCuatroResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaDetalleTramitePasoCuatroResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaDetalleTramitePasoQuintoResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaDetalleTramitePasoSextoResponse>().HasNoKey();
            modelBuilder.Entity<ActualizarTramitePasoSextoResponse>().HasNoKey();
            modelBuilder.Entity<InsertarTramitePasoCincoResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaDetalleTramiteIdResponse>().HasNoKey();
            modelBuilder.Entity<BorraRepresentanteResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaTipoRepresentanteResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaCatalogoCotejoResponse>().HasNoKey();
            modelBuilder.Entity<ConteoColoniaResponse>().HasNoKey();
            modelBuilder.Entity<ConteoTramitesResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaTramitesResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaDetalleCotejoPublicoResponse>().HasNoKey();

            modelBuilder.Entity<ConsultaEstadosResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaMunicipiosResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaEstatusResponse>().HasNoKey();
            modelBuilder.Entity<ReporteResponse>().HasNoKey();
            modelBuilder.Entity<CatalogoCredoInsertResponse>().HasNoKey();
            modelBuilder.Entity<BorraCatalogoCredoResponse>().HasNoKey();
            modelBuilder.Entity<ActualizarCatalogoCredoResponse>().HasNoKey();
            modelBuilder.Entity<ArchivoResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaModulosPerfilResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaUsuariosDicataminadorResponse>().HasNoKey();
            modelBuilder.Entity<AsignarDictaminadorRegistroResponse>().HasNoKey();
            modelBuilder.Entity<FinalizarTramiteResponse>().HasNoKey();
            modelBuilder.Entity<CatalogoPaisoInsertResponse>().HasNoKey();
            modelBuilder.Entity<BorraCatalogoPaisoResponse>().HasNoKey();
            modelBuilder.Entity<ActualizarCatalogoPaisoResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaEstatusResponse>().HasNoKey();
            modelBuilder.Entity<ActualizarCotejoResponse>().HasNoKey();

            modelBuilder.Entity<ConsultaDetalleTomaNotaResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaDetalleTomaNotaDomicilioLegalResponse>().HasNoKey();
            modelBuilder.Entity<ActualizarTomaNotaDomicilioResponse>().HasNoKey();
            modelBuilder.Entity<ActualizarTomaNotaEstatutosDenominacionResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaDetalleTomaNotaRepresentantesResponse>().HasNoKey();
            modelBuilder.Entity<InsertarTomaNotaRepresentanteResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaCatalogosTMovimientosResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaCatalogosPoderesResponse>().HasNoKey();
            modelBuilder.Entity<InsertarTomaNotaRepresentanteLegalResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaDetalleTomaNotaRepresentanteLegalResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaDetalleTomaNotaApoderadoResponse>().HasNoKey();
            modelBuilder.Entity<InsertarTomaNotaApoderadoLegalResponse>().HasNoKey();
            modelBuilder.Entity<ActualizarSolicitudEscritoTomaNotaResponse>().HasNoKey();
            modelBuilder.Entity<FinalizarTomaNotaResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaUsuariosDictaminadorTomaNotaResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaTomaNotaResponse>().HasNoKey();
            modelBuilder.Entity<ConteoTomaNotaResponse>().HasNoKey();
            modelBuilder.Entity<AsignarDictaminadorTomaNotaResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaDetalleMovimientosTomaNotaResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaCatalogoMovimientosTomaNotaResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaPermisoPersonaResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaDetalleTomaNotaInfoResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaHorariosBloqueoResponse>().HasNoKey();
            modelBuilder.Entity<ActualizarDirectorResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaActosFechasResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaActosMediosTrasmisionResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaActosReligiososResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaCatalogoDirectorResponse>().HasNoKey();
            modelBuilder.Entity<EliminarDirectorResponse>().HasNoKey();
            modelBuilder.Entity<InsertarActosFechasResponse>().HasNoKey();
            modelBuilder.Entity<InsertarTransmisionActosReligiososResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaTramiteTransmisionResponse>().HasNoKey();
            modelBuilder.Entity<InsertarTramiteTransmisionResponse>().HasNoKey();
            modelBuilder.Entity<ActualizarMediosTransmisionResponse>().HasNoKey();
            modelBuilder.Entity<BorraMediosTransmisionResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaCatalogoDirectorResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaAnexoAsuntoResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaDetalleAnexoAsuntoResponse>().HasNoKey();
            modelBuilder.Entity<InsertarAnexoAsuntoResponse>().HasNoKey();
            modelBuilder.Entity<BorrarAnexoAsuntoResponse>().HasNoKey();
            modelBuilder.Entity<BorrarAnexoBDResponse>().HasNoKey();
            modelBuilder.Entity<ActualizarEstatusTransmisionResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaUsuariosDictaminadorTransmisionResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaEstatusTransmisionResponse>().HasNoKey();
            modelBuilder.Entity<AsignarTransmisionDictaminadorResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaDetalleTramiteTransmisionResponse>().HasNoKey();

            modelBuilder.Entity<ReporteTransmisionListaEstatusResponse>().HasNoKey();
            modelBuilder.Entity<ActualizarTomaNotaComentarioResponse>().HasNoKey();
            modelBuilder.Entity<ActualizarAtenderTransmisionResponse>().HasNoKey();
            modelBuilder.Entity<AutorizarTransmisionResponse>().HasNoKey();
            modelBuilder.Entity<InsertarObservacionTransmisionResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaOficioTransmisionResponse>().HasNoKey();
            modelBuilder.Entity<ReporteTransmisionResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaDetalleTransmisionCotejoPublicoResponse>().HasNoKey();
            modelBuilder.Entity<InsertarActosEmisorasResponse>().HasNoKey();
            modelBuilder.Entity<EliminarFechasEmisorasResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaArchivosListaResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaRegistrosTramiteResponse>().HasNoKey();
            modelBuilder.Entity<EliminarRegistroTramiteResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaRegistrosTomaNotaResponse>().HasNoKey();
            modelBuilder.Entity<EliminarRegistroTomaNotaResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaRegistrosTransmisionResponse>().HasNoKey();
            modelBuilder.Entity<InsertarPlantillaDocTransmisionResponse>().HasNoKey();
            modelBuilder.Entity<ConsultarPlantillaDocTransmisionResponse>().HasNoKey();
            modelBuilder.Entity<BorrarPlantillaDocTransmisionResponse>().HasNoKey();
            modelBuilder.Entity<EliminarRegistroTransmisionResponse>().HasNoKey();
            modelBuilder.Entity<ActulizarPlantillaDocTransmisionResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaRegistrosTramiteDictaminadorResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaRegistrosTNotaDictaminadorResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaListaRegistrosTransmisionDictaminadorResponse>().HasNoKey();
            modelBuilder.Entity<ReporteResponseTnota>().HasNoKey();
            modelBuilder.Entity<ConsultaListaCatalogosMovRealizadosResponse>().HasNoKey();
            modelBuilder.Entity<ResponseGenerico>().HasNoKey();
            modelBuilder.Entity<CatalogoGenericoResponse>().HasNoKey();
            modelBuilder.Entity<ConsultarTramiteDeclaratoriaPaso1>().HasNoKey();
            modelBuilder.Entity<ConsultarTramiteDeclaratoriaPaso2>().HasNoKey();
            modelBuilder.Entity<ConsultarTramiteDeclaratoriaPaso4>().HasNoKey();
            modelBuilder.Entity<ConsultarTramiteDeclaratoriaPaso5>().HasNoKey();
            modelBuilder.Entity<ConsultarTramiteDeclaratoriaAvance>().HasNoKey();
            modelBuilder.Entity<ConsultarTramiteDeclaratoriaLista>().HasNoKey();
            modelBuilder.Entity<ReporteTramitesResponse>().HasNoKey();
            modelBuilder.Entity<PlantillaResponse>().HasNoKey();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {

            IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
            int TipoBase = int.Parse(Configuration["TipoBase"].ToString());
            var conect = Configuration.GetConnectionString("ConnectionDBPostGreSQL");
            switch (TipoBase)
            {
                //MySql
                case 1:
                    optionBuilder.UseMySql(Configuration.GetConnectionString("ConnectionDBMysql"), null);


                    break;
                //PostgreSQL
                case 2:
                    optionBuilder.UseNpgsql(Configuration.GetConnectionString("ConnectionDBPostGreSQL"));
                    break;
            }
        }
    }
}
