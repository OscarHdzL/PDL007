import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { CatalogoRutas } from "src/app/model/Catalogos/CatalogoRutas";
import { LocalStorageService } from "src/app/services/local-storage.service";
import { AuthGuard } from "../../../app/guards/AuthGuard";
import { UserIdleService } from "angular-user-idle";
import { AuthService } from "src/app/services/auth.service";
import { PipeTransform, Pipe } from "@angular/core";
import { AuthIdentity } from "src/app/guards/AuthIdentity";

@Component({
  selector: "app-nav-menu",
  templateUrl: "./nav-menu.component.html",
  styleUrls: ["./nav-menu.component.css"],
  providers: [AuthGuard],
})
export class NavMenuComponent implements OnInit {
  public isAuth: boolean;

  isExpanded = false;
  public ListModulos: Array<CatalogoRutas> = [];
  listaEncabezados: any[];

  public banner: string;

  constructor(
    private auth: AuthGuard,
    private router: Router,
    private authService: AuthService,
    private localStorageService: LocalStorageService,
    private userIdle: UserIdleService
  ) {}

  ngOnInit(): void {
    this.ListModulos = this.localStorageService.getJsonValue("ListaMenuAgrupado");
    this.isAuth = this.auth.canActivate();
    this.authService.estatusActualDelUsuarioEnSesion$().subscribe((isAuth) => {
      this.isAuth = isAuth;
      this.setupidleSession();
    });
    this.authService.refrescarMenuPermisos$().subscribe((modules) => {
      this.ListModulos = modules;

      this.banner = AuthIdentity.GetBannerSession();
    });
    this.setupidleSession();
    this.banner = AuthIdentity.GetBannerSession();
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  //#region Métodos publicos
  public cerrarSesion(): void {
    this.isAuth = false;
    this.auth.logOut();
    this.authService.EsEstaAutenticado(false);
    this.localStorageService.clearToken();
    this.router.navigate(["/iniciar-sesion"]);
    this.banner = "";
  }
  //#endregion

  setupidleSession() {
    //configuración para el tiempo de inactividad  // 600 10 minutos - 1800 30 min
    if (this.isAuth) {
      this.userIdle.stopWatching();
      this.userIdle.setConfigValues({ idle: 3600, timeout: 1 });
      this.userIdle.startWatching();
      this.userIdle.onTimerStart().subscribe((count) => {});
      this.userIdle.onTimeout().subscribe(() => this.accionPorinactividad());
    }
  }

  accionPorinactividad() {
    this.isAuth ? this.cerrarSesion() : null;
    this.userIdle.resetTimer();
  }
}
