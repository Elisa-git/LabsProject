import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AutenticacaoService } from '../autenticacao.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoginRequest } from '../entrar/models/login.request';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-cadastrar',
  templateUrl: './cadastrar.component.html',
  styleUrl: './cadastrar.component.css'
})

export class CadastrarComponent {

  public usuarioForm: FormGroup;
  public loginRequest = new LoginRequest({});

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly autenticacaoService: AutenticacaoService,
    private readonly toastr: ToastrService,
    private readonly spinner: NgxSpinnerService
  )
  {
    this.usuarioForm = this.formBuilder.group({
      nome: [ '', Validators.required ],
      email: [ '', Validators.required ],
      senha: [ '', Validators.required ]
    });
  }

  public cadastrarUsuario() {
    this.spinner.show();
    this.loginRequest = new LoginRequest({
      nome: this.usuarioForm.get("nome")?.value,
      email: this.usuarioForm.get("email")?.value,
      password: this.usuarioForm.get("senha")?.value
    });

    this.autenticacaoService
      .cadastrarUsuario(this.loginRequest)
      .pipe(finalize(() => { this.spinner.hide(); }))
      .subscribe(() => {
        // Fazer o redirecionamento
        this.toastr.success('Sucesso!', 'Usuário criado!!');
      }, erro => {
        this.toastr.error('Erro!', 'Um problema aconteceu');
      }
    );
  }
}
