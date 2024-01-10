import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import jwt_decode from 'jwt-decode';
import { environment } from 'src/environments/environment.development';
import { User } from 'src/app/models/User';
import { Router } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedIn = false;
  private username = '';
  private apiUrl = environment.apiUrl;
  private userId = '';
  private token = '';
  private user: any; // New property to store the UserDto object

  constructor(private http: HttpClient,private router: Router) {
    // Check for JWT token in localStorage during initialization
    const jwtToken = this.getJwtToken();
    if (jwtToken) {
      this.setLoggedInStatus(true, this.getUsernameFromToken());
      this.token = jwtToken;
    }
  }

  login(email: string, password: string): Observable<any> {
    const loginData = { email, password };
    return this.http.post(`${this.apiUrl}/api/Users/login`, loginData).pipe(
      tap((response: any) => {
        const { jwt, user } = response;
        this.username = user.fullName;
        console.log(user);
        this.token = jwt;
        this.userId = user.userId; // store the JWT token 
        this.user = user; // Store the UserDto object return
        this.setLoggedInStatus(true, this.userId);
        this.storeJwtToken(jwt); // Store the JWT token in localStorage
      })
    );
  } 

  signup(user: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/api/Users/signup`, user).pipe(
      tap((response: any) => {
        console.log(response);
        this.router.navigate(["/login"]);
      })
    );
  }

  setLoggedInStatus(status: boolean, userId: string) {
    this.loggedIn = status;
    this.userId = userId;
  }

  isLoggedIn(): boolean {
    return this.loggedIn;
  }

  getToken(): string {
    return this.token;
  }

  getUser(): any {
    return this.user;
  }

  logout() {
    this.loggedIn = false;
    //this.username = '';
    this.token = '';
    this.user = null; // Clear the stored UserDto object
    this.clearJwtToken(); // Remove the JWT token from localStorage
  }

  // Store the JWT token in localStorage
  private storeJwtToken(token: string): void {
    localStorage.setItem('jwtToken', token);
  }

  // Retrieve the JWT token from localStorage
  getJwtToken(): any {
    return localStorage.getItem('jwtToken');
  }

  // Remove the JWT token from localStorage
  private clearJwtToken(): void {
    localStorage.removeItem('jwtToken');
  }

  getUsernameFromToken(): any {
    const token = this.getJwtToken();
     const decodedToken: any = jwt_decode(token);
     return decodedToken.Name;
  }
  
  getUserIdFromToken() :any{
     const token = this.getJwtToken();
     const decodedToken: any = jwt_decode(token);
     return decodedToken.Id;
  }
}
