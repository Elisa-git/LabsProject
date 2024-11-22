import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';

// @Injectable()

// export class CookieInterceptor implements HttpInterceptor {
//   intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
//     // Aqui você pode obter o cookie que deseja adicionar
//     const cookieValue = this.getCookie('nomeDoCookie');

//     // Clonando a requisição para adicionar o cookie
//     const clonedRequest = req.clone({
//       setHeaders: {
//         Cookie: cookieValue ? cookieValue : '' // Adicionando o cookie no cabeçalho, se existir
//       }
//     });

//     // Passando a requisição clonada para o próximo handler
//     return next.handle(clonedRequest);
//   }

//   private getCookie(name: string): string | null {
//     const value = `; ${document.cookie}`;
//     const parts = value.split(`; ${name}=`);
//     if (parts.length === 2) return parts.pop().split(';').shift();
//     return null;
//   }
// }
