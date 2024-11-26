import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginRequest } from './models/login.request';
import { AutenticacaoService } from '../autenticacao.service';
import { finalize } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { UserResponse } from './models/user.response';

@Component({
  selector: 'app-entrar',
  templateUrl: './entrar.component.html',
  styleUrl: './entrar.component.css'
})

export class EntrarComponent implements OnInit {

  @Output() infoForNavbar = new EventEmitter<UserResponse>();
  public loginForm: FormGroup;

  public loginRequest = new LoginRequest({});

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly autenticacaoService: AutenticacaoService,
    private readonly toastr: ToastrService,
    private readonly route: Router
  )
  {
    this.loginForm = this.formBuilder.group({
      email: [ '', Validators.required ],
      senha: [ '', Validators.required ]
    });
  }

  ngOnInit(): void {
    this.AdicionarContent();
  }

  private AdicionarContent() {
    const content = '<h1>Bem-vindo de volta!</h1> <p>Acesse sua conta agora mesmo.</p>';
    document.getElementById("content")!.innerHTML = content;
  }

  public entrar() {
    this.loginRequest = new LoginRequest({
      email: this.loginForm.get("email")?.value,
      password: this.loginForm.get("senha")?.value
    });

    this.autenticacaoService
      .entrar(this.loginRequest)
      .subscribe(response => {
        this.infoForNavbar.emit(response);
        this.toastr.success('Sucesso!', 'Você logou!!');
        this.route.navigate(['/listar-produtos']);
      }, erro => {
        this.toastr.error(erro.error.message, 'Erro!');
      }
    );
  }

}
