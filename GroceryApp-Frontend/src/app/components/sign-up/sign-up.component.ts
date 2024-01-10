import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/service/Auth-service/auth.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignupComponent implements OnInit {
  signupForm!: FormGroup;
  confirmPasswordError = false;

  constructor(
    private authService: AuthService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.signupForm = this.formBuilder.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],
      password: ['', [Validators.required, Validators.minLength(8), Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$/)]],
      confirmPassword: ['', Validators.required]
    });
  }

  get formControls() {
    return this.signupForm.controls;
  }

  signup() {
    if (this.signupForm.valid) {
      const user = {
        FullName: this.formControls['fullName'].value,
        Email: this.formControls['email'].value,
        Phone: this.formControls['phone'].value,
        Password: this.formControls['password'].value,
      };

      if (this.formControls['password'].value === this.formControls['confirmPassword'].value) {
        this.authService.signup(user).subscribe(
          response => {
            // Handle successful sign-up
            console.log(response);
          },
          error => {
            // Handle sign-up error
            console.log(error);
          }
        );
      } else {
        // Passwords do not match, set error flag to display the error message
        this.confirmPasswordError = true;
      }
    }
  }
}
