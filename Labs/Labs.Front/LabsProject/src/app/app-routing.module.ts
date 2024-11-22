import { RouterModule, Routes } from '@angular/router';
import { CadastrarComponent } from './Autenticacao/cadastrar/cadastrar.component';
import { NgModule } from '@angular/core';
import { ListarProdutosComponent } from './Produtos/componentes/listar-produtos/listar-produtos.component';
import { EntrarComponent } from './Autenticacao/entrar/entrar.component';
import { CadastrarProdutosComponent } from './Produtos/componentes/cadastrar-produtos/cadastrar-produtos.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'entrar',
    pathMatch: 'full'
  },
  {
    path: 'entrar',
    component: EntrarComponent
  },
  {
    path: 'cadastrar',
    component: CadastrarComponent
  },
  {
    path: 'listar-produtos',
    component: ListarProdutosComponent
  },
  {
    path: 'cadastrar-produtos',
    component: CadastrarProdutosComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
