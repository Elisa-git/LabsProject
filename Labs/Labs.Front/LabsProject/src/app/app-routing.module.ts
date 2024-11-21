import { RouterModule, Routes } from '@angular/router';
import { CadastrarComponent } from './Autenticacao/cadastrar/cadastrar.component';
import { NgModule } from '@angular/core';
import { ListarProdutosComponent } from './Produtos/listar-produtos/listar-produtos.component';
import { EntrarComponent } from './Autenticacao/entrar/entrar.component';

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
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
