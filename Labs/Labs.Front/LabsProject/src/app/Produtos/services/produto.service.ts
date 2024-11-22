import { Injectable } from '@angular/core';
import { ProdutoRequest } from '../models/produtos.request';
import { Observable } from 'rxjs';
import { ProdutosResponse } from '../models/produtos.response';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class ProdutoService {

  private readonly url = 'https://localhost:7042/api/Produtos/';

  constructor(private readonly httpClient: HttpClient) { }

  public listar(): Observable<Array<ProdutosResponse>> {
    return this.httpClient.get<Array<ProdutosResponse>>(this.url)
  }

  public inserir(request: ProdutoRequest) : Observable<ProdutosResponse> {
    return this.httpClient.post<ProdutosResponse>(this.url, request)
  }

  public editar(request: ProdutoRequest) : Observable<ProdutosResponse> {
    return this.httpClient.put<ProdutosResponse>(this.url + request.id, request)
  }

  public excluir(id: number) : Observable<ProdutosResponse> {
    return this.httpClient.delete<ProdutosResponse>(this.url + id);
  }
}
