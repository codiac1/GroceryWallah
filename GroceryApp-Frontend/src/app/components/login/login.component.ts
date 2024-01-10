import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/service/Auth-service/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email!: string;
  password!: string;

  constructor(private authService: AuthService, private router: Router) { }

  login() {
    // Validate the email and password 
    this.authService.login(this.email, this.password).subscribe(
      (_response: any) => {
        console.log('Login successful');
        this.router.navigate(['/home'])
      },
      (error: any) => {
        console.error('Login failed', error);
      }
    );
  }

}
