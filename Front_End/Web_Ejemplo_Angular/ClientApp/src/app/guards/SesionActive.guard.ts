import { Injectable } from "@angular/core";
import { LocalStorageService } from "../services/local-storage.service";
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from "rxjs";
import { JwtHelperService } from "@auth0/angular-jwt";

@Injectable({
  providedIn: 'root'
})

export class SesionActiveGuard {
  constructor(
    private jwtHelper: JwtHelperService,
    private router: Router
  ) {}

  async canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Promise<Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree> {
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token, 300)) {
      return true;
    }
    return this.router.parseUrl('/iniciar-sesion');
  }
}
