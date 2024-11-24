import { Component, Input } from '@angular/core';
import { UserResponse } from '../Autenticacao/entrar/models/user.response';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})

export class NavbarComponent {

  @Input() info = new UserResponse({});

  constructor(
    private readonly route: Router
  ) {}

  public deslogar() {
    this.info = new UserResponse({});
    localStorage.removeItem('user');
    localStorage.removeItem('token');
    this.route.navigate(['/entrar']);
  }

}
