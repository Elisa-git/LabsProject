import { Component, OnInit } from '@angular/core';
import { UserResponse } from './Autenticacao/entrar/models/user.response';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  title = 'LabsProject';
  public navbarInfo = new UserResponse({});

  ngOnInit() {
    const storedInfo = localStorage.getItem('user');
    if (storedInfo) {
      this.navbarInfo = JSON.parse(storedInfo);
    }
  }

  public onRouterOutletActivate(component: any) {
    if (component.infoForNavbar) {
      component.infoForNavbar.subscribe((data: UserResponse) => {
        this.navbarInfo = data;
        localStorage.setItem('user', JSON.stringify(data));
      });
    }
  }
}
