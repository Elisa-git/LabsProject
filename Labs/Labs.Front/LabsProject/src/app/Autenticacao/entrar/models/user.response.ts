export class UserResponse {
  public nome!: string;
  public email!: string;
  public token!: string;

  constructor(params: Partial<UserResponse>) {
    this.nome = params.nome || '';
    this.email = params.email || '';
    this.token = params.token || '';
  }
}
