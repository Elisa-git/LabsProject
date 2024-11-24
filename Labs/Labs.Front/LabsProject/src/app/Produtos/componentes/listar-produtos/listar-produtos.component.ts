import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ProdutoService } from '../../services/produto.service';
import { finalize } from 'rxjs';
import { ProdutosResponse } from '../../models/produtos.response';

@Component({
  selector: 'app-listar-produtos',
  templateUrl: './listar-produtos.component.html',
  styleUrl: './listar-produtos.component.css'
})

export class ListarProdutosComponent implements OnInit {

  public produtoResponse = new Array<ProdutosResponse>();

  constructor(
    private readonly route: Router,
    private readonly toastr: ToastrService,
    private readonly spinner: NgxSpinnerService,
    private readonly produtoService: ProdutoService
  ) { }

  ngOnInit(): void {
    this.AdicionarContent();
    this.listar();
  }

  private AdicionarContent() {
    const content = '<span><svg xmlns="http://www.w3.org/2000/svg" width="30" height="30" fill="#f5f5f5" class="bi bi-box-seam" viewBox="0 0 16 16"> <path d="M8.186 1.113a.5.5 0 0 0-.372 0L1.846 3.5l2.404.961L10.404 2zm3.564 1.426L5.596 5 8 5.961 14.154 3.5zm3.25 1.7-6.5 2.6v7.922l6.5-2.6V4.24zM7.5 14.762V6.838L1 4.239v7.923zM7.443.184a1.5 1.5 0 0 1 1.114 0l7.129 2.852A.5.5 0 0 1 16 3.5v8.662a1 1 0 0 1-.629.928l-7.185 2.874a.5.5 0 0 1-.372 0L.63 13.09a1 1 0 0 1-.63-.928V3.5a.5.5 0 0 1 .314-.464z"/> </svg></span>';
    document.getElementById("content")!.innerHTML = content;
  }

  private listar() {
    this.spinner.show();

    this.produtoService
      .listar()
      .pipe(finalize(() => { this.spinner.hide(); }))
      .subscribe(response => {
        this.produtoResponse = response;
      }, erro => {
        this.toastr.error('Erro!', 'Credenciais incorretas');
      }
    );
  }

  public criarCadastro() {
    this.route.navigate(['/cadastrar-produtos']);
  }

  public editarCadastro(produto: ProdutosResponse) {
    this.route.navigate(['/cadastrar-produtos'], { state: { data: produto } });
  }

  public excluirCadastro(produto: ProdutosResponse) {
    this.produtoService
      .excluir(produto.id)
      .subscribe(response => {
        this.toastr.success('Sucesso!', 'O documento '+ produto.nome +' foi excluído');
        this.listar();
      }, erro => {
        if (erro.status == 401) {
          this.toastr.warning('Você não possui permissão para excluir o produto', 'Atenção!');
        } else {
          this.toastr.error('O prouto '+ produto.nome +' não foi excluído', 'Erro!');
        }
      }
    );
  }


}
