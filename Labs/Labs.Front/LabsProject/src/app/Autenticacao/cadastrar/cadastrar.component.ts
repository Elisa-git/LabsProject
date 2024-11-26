import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AutenticacaoService } from '../autenticacao.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoginRequest } from '../entrar/models/login.request';
import { finalize } from 'rxjs';
import { UserResponse } from '../entrar/models/user.response';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cadastrar',
  templateUrl: './cadastrar.component.html',
  styleUrl: './cadastrar.component.css'
})

export class CadastrarComponent implements OnInit {

  @Output() infoForNavbar = new EventEmitter<UserResponse>();

  public usuarioForm: FormGroup;
  public loginRequest = new LoginRequest({});

  public nivelSenha = 0;

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly autenticacaoService: AutenticacaoService,
    private readonly toastr: ToastrService,
    private readonly route: Router
  )
  {
    this.usuarioForm = this.formBuilder.group({
      nome: [ '', Validators.required ],
      email: [ '', Validators.required ],
      senha: [ '', Validators.required ]
    });

    this.usuarioForm.get('senha')?.valueChanges.subscribe(value => {
      this.nivelSenha = this.avaliarForcaSenha(value);
    });
  }

  ngOnInit(): void {
    this.AdicionarContent();
  }

  private avaliarForcaSenha(senha: string) : number {
    return this.autenticacaoService.validarScoreSenha(senha);
  }

  private AdicionarContent() {
    const content = '<h1>Seja bem-vindo <br> ao Labs!</h1> <p>Crie sua conta agora mesmo.</p>';
    document.getElementById("content")!.innerHTML = content;
  }

  public cadastrarUsuario() {
    this.loginRequest = new LoginRequest({
      nome: this.usuarioForm.get("nome")?.value,
      email: this.usuarioForm.get("email")?.value,
      password: this.usuarioForm.get("senha")?.value
    });

    this.autenticacaoService
      .cadastrarUsuario(this.loginRequest)
      .subscribe(() => {
        this.route.navigate(['/confirmar-cadastro'], { state: { data: this.loginRequest }});
      }, erro => {
        this.toastr.error(erro.error.message, 'Erro!');
      }
    );
  }
}
