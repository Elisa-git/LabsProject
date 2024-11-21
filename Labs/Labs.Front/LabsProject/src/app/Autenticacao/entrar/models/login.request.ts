export class LoginRequest {
  public email!: string;
  public password!: string;

  constructor(params: Partial<LoginRequest>) {
    this.email = params.email || '';
    this.password = params.password || '';
  }
}
