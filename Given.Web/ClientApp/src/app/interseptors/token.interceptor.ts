import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse,
  HttpErrorResponse,HttpHeaders
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import {
  Router
} from '@angular/router';
import { Injectable } from '@angular/core';


@Injectable()
export class TokenInterceptor implements HttpInterceptor {
	constructor(private router: Router) {}

  	intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

		  const token = localStorage.getItem('userData') ? JSON.parse(localStorage.getItem('userData')).token : '';

		  const authReq = request.clone({
		    headers: new HttpHeaders({
		      'Content-Type':  'application/json',
		      'Authorization': 'Bearer '+token
		    })
		  });

		  console.log('Intercepted HTTP call', authReq);

		  return next.handle(authReq);

		  // if (token) {
		  //   request = request.clone({
		  //     setHeaders: {
		  //       'Api-Key': token
		  //     }
		  //   });
		  // }

		  // if (!request.headers.has('Content-Type')) {
		  //   request = request.clone({
		  //     setHeaders: {
		  //       'content-type': 'application/json'
		  //     }
		  //   });
		  // }

		  // request = request.clone({
		  //   headers: request.headers.set('Api-Key', token)
		  // });

		  // return next.handle(request).pipe(
		  //   map((event: HttpEvent<any>) => {
		  //     if (event instanceof HttpResponse) {
		  //       console.log('event--->>>', event);
		  //     }
		  //     return event;
		  //   }),
		  //   catchError((error: HttpErrorResponse) => {
		  //     if (error.status === 401) {
		  //       if (error.error.success === false) {
		  //         this.presentToast('Login failed');
		  //       } else {
		  //         this.router.navigate(['login']);
		  //       }
		  //     }
		  //     return throwError(error);
		  //   }));
	}

	
}