import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService, LoginRequest } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html'
})
export class LoginComponent {
  loginForm: FormGroup;
  error = '';

  private formBuilder = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  constructor() {
    this.loginForm = this.formBuilder.group({
      userName: ['', [Validators.required]],
      password: ['', Validators.required]
    });
  }

  login() {

    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    const payload: LoginRequest = {
      username: this.loginForm.value.userName,
      password: this.loginForm.value.password
    };

    this.authService.login(payload).subscribe({
      next: (response) => {
        if (response.success) {
          console.log("Login successful");
          this.router.navigate(['/']);
        } else {
          this.error = response.errorMessage ?? "Inloggningen misslyckades";
        }
      },
      error: () => {
        this.error = "Inloggningen misslyckades";
      }
    });
  }
}
