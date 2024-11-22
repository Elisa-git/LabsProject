import { ProdutoService } from './../../services/produto.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
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
    private readonly spinner: NgxSpinnerService,
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
    this.produtoRequest = history.state.data as ProdutoRequest;
    console.log("req", this.produtoRequest);
    this.inicializarFormulario();
  }

  private inicializarFormulario() {
    this.produtoForm = this.formBuilder.group({
      nome: [ this.produtoRequest?.nome || '', Validators.required ],
      marca: [ this.produtoRequest?.marca || '', Validators.required ],
      quantidade: [ this.produtoRequest?.quantidade || null, Validators.required ]
    })
  }

  public salvar() {
    this.spinner.show();

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
        this.spinner.hide();
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
        this.spinner.hide();
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
