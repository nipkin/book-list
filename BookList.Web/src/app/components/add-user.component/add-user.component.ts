import { Component, inject } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'add-user',
  templateUrl: './add-user.component.html',
  imports: [ReactiveFormsModule, CommonModule],
  standalone: true
})
export class AddUserComponent {
  addUserForm: FormGroup;
  errorMessage = '';
  successMessage = '';

  private formBuilder = inject(FormBuilder);
  private authService = inject(AuthService);

  constructor() {
    this.addUserForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.addUserForm.invalid) {
      this.addUserForm.markAllAsTouched();
      return;
    }

    if (this.addUserForm.value.password !== this.addUserForm.value.confirmPassword) {
      this.errorMessage = 'Lösenorden matchar inte';
      return;
    }

    const payload = {
      username: this.addUserForm.value.username,
      password: this.addUserForm.value.password,
      confirmPassword: this.addUserForm.value.confirmPassword
    };

    this.authService.addUser(payload).subscribe({
      next: (response) => {
        if (response.success) {
          this.successMessage = 'Användaren har skapats!';
          this.errorMessage = '';
          this.addUserForm.reset();
        } else {
          this.errorMessage = response.errorMessage ?? 'Något gick fel';
        }
      },
      error: () => {
        this.errorMessage = 'Något gick fel';
      }
    });
  }
}
