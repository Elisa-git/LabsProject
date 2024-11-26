import { ProdutoService } from './../../services/produto.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ProdutoRequest } from '../../models/produtos.request';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-cadastrar-produtos',
  templateUrl: './cadastrar-produtos.component.html',
  styleUrl: './cadastrar-produtos.component.css'
})

export class CadastrarProdutosComponent implements OnInit {

  public produtoForm: FormGroup;
  public produtoRequest = new ProdutoRequest({});

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly toastr: ToastrService,
    private readonly route: Router,
    private readonly produtoService: ProdutoService
  )
  {
    this.produtoForm = this.formBuilder.group({
      nome: [ '', Validators.required ],
      marca: [ '', Validators.required ],
      quantidade: [ null, Validators.required ]
    });
  }

  ngOnInit(): void {
    this.AdicionarContent();
    this.produtoRequest = history.state.data as ProdutoRequest;
    this.inicializarFormulario();
  }

  private AdicionarContent() {
    const content = '<span><svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="#f5f5f5" class="bi bi-box-seam" viewBox="0 0 16 16"> <path d="M8.186 1.113a.5.5 0 0 0-.372 0L1.846 3.5l2.404.961L10.404 2zm3.564 1.426L5.596 5 8 5.961 14.154 3.5zm3.25 1.7-6.5 2.6v7.922l6.5-2.6V4.24zM7.5 14.762V6.838L1 4.239v7.923zM7.443.184a1.5 1.5 0 0 1 1.114 0l7.129 2.852A.5.5 0 0 1 16 3.5v8.662a1 1 0 0 1-.629.928l-7.185 2.874a.5.5 0 0 1-.372 0L.63 13.09a1 1 0 0 1-.63-.928V3.5a.5.5 0 0 1 .314-.464z"/> </svg></span> <h2 class="mt-2">Labs</h2>';
    document.getElementById("content")!.innerHTML = content;
  }

  private inicializarFormulario() {
    this.produtoForm = this.formBuilder.group({
      nome: [ this.produtoRequest?.nome || '', Validators.required ],
      marca: [ this.produtoRequest?.marca || '', Validators.required ],
      quantidade: [ this.produtoRequest?.quantidade || null, Validators.required ]
    })
  }

  public salvar() {
    var request = new ProdutoRequest({
      nome: this.produtoForm.get("nome")?.value,
      marca: this.produtoForm.get("marca")?.value,
      quantidade: this.produtoForm.get("quantidade")?.value
    });

    if (this.produtoRequest) {
      request.id = this.produtoRequest.id;
      this.editarProduto(request);
    } else {
      this.inserirProduto(request);
    }
  }

  private editarProduto(produto: ProdutoRequest) {
    this.produtoService
      .editar(produto)
      .pipe(finalize(() => {
        this.route.navigate(['/listar-produtos']);
      }))
      .subscribe(response => {
        this.toastr.success("O produto foi atualizado", "Sucesso!")
      }, erro => {
        if (erro.status == 401) {
          this.toastr.warning('Você não possui permissão para editar o produto', 'Atenção!');
        } else {
          this.toastr.error('O produto '+ produto.nome +' não foi editado', 'Erro!');
        }
      }
    );
  }

  private inserirProduto(produto: ProdutoRequest) {
    this.produtoService
      .inserir(produto)
      .pipe(finalize(() => {
        this.route.navigate(['/listar-produtos']);
      }))
      .subscribe(response => {
        this.toastr.success("O produto " + produto.nome + " foi cadastrado", "Sucesso!")
      }, erro => {
        if (erro.status == 401) {
          this.toastr.warning('Você não possui permissão para adicionar um produto', 'Atenção!');
        } else {
          this.toastr.error('O produto '+ produto.nome +' não foi criado', 'Erro!');
        }
      }
    );
  }

  public voltar() {
    this.route.navigate(['/listar-produtos']);
  }
}
