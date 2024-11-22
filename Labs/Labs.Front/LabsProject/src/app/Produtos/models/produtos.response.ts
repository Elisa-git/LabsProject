export class ProdutosResponse {
  public id!: number;
  public nome!: string;
  public marca!: string;
  public quantidade!: number;

  constructor(params: Partial<ProdutosResponse>) {
    this.id = params.id || 0;
    this.nome = params.nome || '';
    this.marca = params.marca || '';
    this.quantidade = params.quantidade || 0;
  }
}
