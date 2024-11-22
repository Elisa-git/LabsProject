export class LoginRequest {
  public nome!: string;
  public email!: string;
  public password!: string;

  constructor(params: Partial<LoginRequest>) {
    this.nome = params.nome || '';
    this.email = params.email || '';
    this.password = params.password || '';
  }
}
