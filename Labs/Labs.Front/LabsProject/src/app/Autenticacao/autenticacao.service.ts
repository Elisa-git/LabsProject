import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest } from './entrar/models/login.request';
import { Observable, ReplaySubject } from 'rxjs';
import { UserResponse } from './entrar/models/user.response';

@Injectable({
  providedIn: 'root'
})

export class AutenticacaoService {

  private userSource = new ReplaySubject<LoginRequest | null>(1);
  private readonly url = 'https://localhost:7042/auth/';
  private readonly apiAuthPersonalizada = 'https://localhost:7042/api/auth/';

  constructor(private readonly httpClient: HttpClient) { }

  public entrar(request: LoginRequest) : Observable<UserResponse> {
    return this.httpClient.post<UserResponse>(this.apiAuthPersonalizada + "login", request)
  }

  public cadastrarUsuario(request: LoginRequest) {
    return this.httpClient.post(this.apiAuthPersonalizada + "register", request)
  }
}
