import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest } from './entrar/models/login.request';
import { Observable } from 'rxjs';
import { UserResponse } from './entrar/models/user.response';

@Injectable({
  providedIn: 'root'
})

export class AutenticacaoService {

  private readonly url = 'https://localhost:7042/auth/';
  private readonly apiAuthPersonalizada = 'https://localhost:7042/api/auth/';

  constructor(private readonly httpClient: HttpClient) { }

  public entrar(request: LoginRequest) : Observable<UserResponse> {
    return this.httpClient.post<UserResponse>(this.apiAuthPersonalizada + "login", request)
  }

  public cadastrarUsuario(request: LoginRequest) {
    return this.httpClient.post(this.apiAuthPersonalizada + "register", request)
  }

  public reenviarEmail(request: string) {
    const aa = {
      email: request
    };
    return this.httpClient.post(this.url + 'resendConfirmationEmail', aa);
  }

  public validarScoreSenha(senha: string) : number {
    let score = 0;

    const criterios = [
      { regex: /.{6,}/, id: 'tamanho' },          // Pelo menos 6 caracteres
      { regex: /[A-Z]/, id: 'maiusculo' },        // Contém letras maiúsculas
      { regex: /[a-z]/, id: 'minusculo' },        // Contém letras minúsculas
      { regex: /[0-9]/, id: 'numero' },           // Contém números
      { regex: /[!@#$%^&*(),.?":{}|<>]/, id: 'especial' } // Contém caracteres especiais
    ];

    criterios.forEach(criterio => {
      const elemento = document.getElementById(criterio.id);
      if (!elemento) return;

      if (criterio.regex.test(senha)) {
        score++;
        elemento.classList.replace(elemento.className, 'sucesso');
      } else {
        score--;
        elemento.classList.replace('sucesso', 'falha');
      }
    });

    return score;
  }
}
