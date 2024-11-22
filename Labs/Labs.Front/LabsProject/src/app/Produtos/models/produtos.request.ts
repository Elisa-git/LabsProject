export class ProdutoRequest {
  public id?: number;
  public nome!: string;
  public marca!: string;
  public quantidade!: number;

  constructor(params: Partial<ProdutoRequest>) {
    this.id = params.id;
    this.nome = params.nome || '';
    this.marca = params.marca || '';
    this.quantidade = params.quantidade || 0;
  }
}
