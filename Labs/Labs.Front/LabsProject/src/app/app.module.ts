import { AppComponent } from "./app.component";
import { CadastrarComponent } from "./Autenticacao/cadastrar/cadastrar.component";
import { ListarProdutosComponent } from "./Produtos/componentes/listar-produtos/listar-produtos.component";
import { AppRoutingModule } from "./app-routing.module";
import { BrowserModule } from "@angular/platform-browser";
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from "@angular/core";
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxSpinnerModule } from "ngx-spinner";
import { EntrarComponent } from "./Autenticacao/entrar/entrar.component";
import { ToastNoAnimationModule } from "ngx-toastr";
import { CadastrarProdutosComponent } from "./Produtos/componentes/cadastrar-produtos/cadastrar-produtos.component";
import { NavbarComponent } from "./Navbar/navbar.component";
import { ConfirmarEmailComponent } from "./Autenticacao/confirmar-email/confirmar-email.component";

@NgModule({
  declarations: [
    AppComponent,
    EntrarComponent,
    CadastrarComponent,
    ListarProdutosComponent,
    CadastrarProdutosComponent,
    NavbarComponent,
    ConfirmarEmailComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule,
    NgxSpinnerModule.forRoot({ type: 'ball-8bits' }),
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastNoAnimationModule.forRoot()
  ],
  bootstrap: [ AppComponent ],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ],

})

export class AppModule { }
