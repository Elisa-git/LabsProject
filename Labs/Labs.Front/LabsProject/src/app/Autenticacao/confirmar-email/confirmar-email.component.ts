import { Component, Input, OnInit } from '@angular/core';
import { AutenticacaoService } from '../autenticacao.service';
import { LoginRequest } from '../entrar/models/login.request';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-confirmar-email',
  templateUrl: './confirmar-email.component.html',
  styleUrl: './confirmar-email.css'
})

export class ConfirmarEmailComponent implements OnInit {

  public login = new LoginRequest({});
  public emailEnviado: boolean = false;

  constructor(
    private readonly toastr: ToastrService,
    private readonly autenticacaoService: AutenticacaoService
  ) { }

  ngOnInit(): void {
    this.login = history.state.data as LoginRequest;
    this.AdicionarContent();
  }

  private AdicionarContent() {
    const content = '<span><svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="#f5f5f5" class="bi bi-box-seam" viewBox="0 0 16 16"> <path d="M8.186 1.113a.5.5 0 0 0-.372 0L1.846 3.5l2.404.961L10.404 2zm3.564 1.426L5.596 5 8 5.961 14.154 3.5zm3.25 1.7-6.5 2.6v7.922l6.5-2.6V4.24zM7.5 14.762V6.838L1 4.239v7.923zM7.443.184a1.5 1.5 0 0 1 1.114 0l7.129 2.852A.5.5 0 0 1 16 3.5v8.662a1 1 0 0 1-.629.928l-7.185 2.874a.5.5 0 0 1-.372 0L.63 13.09a1 1 0 0 1-.63-.928V3.5a.5.5 0 0 1 .314-.464z"/> </svg></span> <h2 class="mt-2">Labs</h2>';
    document.getElementById("content")!.innerHTML = content;
  }

  public reenviarEmail() {
    this.autenticacaoService
      .reenviarEmail(this.login.email)
      .subscribe(response => {
        this.emailEnviado = true;
        setTimeout(() => {
          this.emailEnviado = false;
        }, 15000)
      }, erro => {
        this.toastr.error(erro.error.message, 'Erro!');
      }
    );
  }

}
