import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest } from './entrar/models/login.request';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class AutenticacaoService {

  private readonly url = 'https://localhost:7042/auth/';
  private readonly apiAuthPersonalizada = 'https://localhost:7042/api/Auth/';

  constructor(private readonly httpClient: HttpClient) { }

  public entrar(request: LoginRequest) {
    return this.httpClient.post(this.url + "login", request)
  }

  public cadastrarUsuario(request: LoginRequest) {
    return this.httpClient.post(this.apiAuthPersonalizada + "register", request)
  }
}
