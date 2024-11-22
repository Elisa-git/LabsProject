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
    this.listar();
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
