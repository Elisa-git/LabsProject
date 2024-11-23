export class UserResponse {
  public nome!: string;
  public email!: string;

  constructor(params: Partial<UserResponse>) {
    this.nome = params.nome || '';
    this.email = params.email || '';
  }
}
