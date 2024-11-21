import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginRequest } from './models/login.request';
import { AutenticacaoService } from '../autenticacao.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { finalize } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-entrar',
  templateUrl: './entrar.component.html',
  styleUrl: './entrar.component.css'
})

export class EntrarComponent {

  public loginForm: FormGroup;

  public loginRequest = new LoginRequest({});

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly autenticacaoService: AutenticacaoService,
    private readonly toastr: ToastrService,
    private readonly spinner: NgxSpinnerService
  )
  {
    this.loginForm = this.formBuilder.group({
      email: [ '', Validators.required ],
      senha: [ '', Validators.required ]
    });
  }

  public entrar() {
    this.spinner.show();
    this.loginRequest = new LoginRequest({
      email: this.loginForm.get("email")?.value,
      password: this.loginForm.get("senha")?.value
    });

    this.autenticacaoService
      .entrar(this.loginRequest)
      .pipe(finalize(() => { this.spinner.hide(); }))
      .subscribe(() => {
        // Fazer o redirecionamento
        this.toastr.success('Sucesso!', 'Você logou!!');
      }, erro => {
        this.toastr.error('Erro!', 'Credenciais incorretas');
      }
    );
  }

}
