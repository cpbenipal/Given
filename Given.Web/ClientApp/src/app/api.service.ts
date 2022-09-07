import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { CanActivate, Router, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material';

@Injectable({
    providedIn: 'root'
})
export class ApiService {
    apiUrl = "https://givenapi.dev-srv.lebertechsolutions.com/api/"
    baseUrl = "https://givenapi.dev-srv.lebertechsolutions.com/"
    profileUrl = "https://givenapi.dev-srv.lebertechsolutions.com/api/Users/GetById/"
    commonErrorMsg: any = 'Something went wrong. Checking the network cables, modem, router, Reconnecting to Wi-Fi.';
 
    // snack bar options
    action = true;
    setAutoHide = true;
    autoHide = 10000;
    addExtraClass = false;

    constructor(public snackBar: MatSnackBar, private router: Router, private http: HttpClient) {


        // var self = this;
        // $.get( "https://ipapi.co/json", function( data ) {
        //   console.log(data);
        //   self.country = data.country;
        //   self.code_calling = data.country_calling_code;
        //   // $( ".result" ).html( data );
        //   // alert( "Load was performed." );
        // });



    }

    getUserDetail(key) {
        let userDetail = JSON.parse(localStorage.getItem('userData'));
        return userDetail[key];
    }

    updateProfileByKey(key, value) {
        let userDetail = JSON.parse(localStorage.getItem('userData'));

        userDetail[key] = value;
        localStorage.setItem('userData', JSON.stringify(userDetail));
    }

    updateProfileLocal(object) {

        localStorage.setItem('userData', JSON.stringify(object));
    }

    diff_years(dt2, dt1) {
        console.log(dt2)
        console.log(dt1)
        var diff = (Number(dt2) - Number(dt1)) / 1000;
        diff /= (60 * 60 * 24);
        return Math.abs(Math.round(diff / 365.25));

    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        const isLoggedIn = localStorage.getItem('isLogin'); // ... your login logic here
        if (isLoggedIn) {
            return true;
        } else {
            this.router.navigate(['/authentication/login']);
            return false;
        }
    }

    getKPIs(): Observable<any> {
        return this.http.get<any>(this.apiUrl + 'Home/GetKPIs');
    }

    getProfile(userId): Observable<any> {
        return this.http.get<any>(this.apiUrl + 'Users/GetById/' + userId);
    }

    login(data): Observable<any> {
        return this.http.post<any>(this.apiUrl + 'Users/Authenticate', data);
    }

    logout(): Observable<any> {
        return this.http.get<any>(this.apiUrl + 'Users/Logout')
            .pipe(
                tap(_ => this.log('logout')),
                catchError(this.handleError('logout', []))
            );
    }
    userregister(data): Observable<any> {
        return this.http.post<any>(this.apiUrl + 'Users/PostUser', data);
    }

    register(data): Observable<any> {
        return this.http.post<any>(this.apiUrl + 'Users/PostCompany', data);
    }

    confirmAccount(data): Observable<any> {
        return this.http.post<any>(this.apiUrl + 'Users/ConfirmAccount', data);
    }

    get(url): Observable<any> {
        return this.http.get<any>(this.apiUrl + url);
    }
    post(url, data): Observable<any> {
        return this.http.post<any>(this.apiUrl + url, data);
    }

    delete(url): Observable<any> {
        // console.log(data)
        return this.http.delete<any>(this.apiUrl + url);
    }
    postForCheckStatus(url, data): Observable<any> {
        return this.http.post<any>(this.apiUrl + url, data);
    }

    put(url, data): Observable<any> {
        return this.http.put<any>(this.apiUrl + url, data);
    }

    create(url, data): Observable<any> {
        return this.http.post<any>(this.apiUrl + url, data);
    }


    openSnackBar(message, actionButtonLabel) {
        const config = new MatSnackBarConfig();
        config.duration = this.autoHide;
        // config.panelClass = this.addExtraClass ? ['party'] : null;
        this.snackBar.open(message, this.action && actionButtonLabel, config);
    }

    userImage(img) {
        img.src = "assets/images/defaultuser.png";
    }


    private handleError<T>(operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {

            // TODO: send the error to remote logging infrastructure
            console.error(error); // log to console instead

            // TODO: better job of transforming error for user consumption
            this.log(`${operation} failed: ${error.message}`);

            // Let the app keep running by returning an empty result.
            return of(result as T);
        };
    }

    /** Log a HeroService message with the MessageService */
    private log(message: string) {
        console.log(message);
    }
}