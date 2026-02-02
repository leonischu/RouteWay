import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Auth } from '../../services/auth';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {

  registerForm: FormGroup;
  loading = false;
  errorMessage = '';
  successMessage = '';

  constructor(
    private fb: FormBuilder,
    private authService: Auth,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      FullName: ['', [Validators.required]],
      Email: ['', [Validators.required, Validators.email]],
      Password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  onSubmit(): void {
    // Prevent multiple submissions
    if (this.registerForm.invalid || this.loading) {
      return;
    }

    this.loading = true;  // ✅ Set to TRUE to show loading state
    this.errorMessage = '';
    this.successMessage = '';

    console.log('Submitting registration...', this.registerForm.value);

    this.authService.register(this.registerForm.value).subscribe({
      next: (response) => {
        console.log('Registration successful:', response);
        this.loading = false;
        this.successMessage = 'Registration successful! Redirecting to login...';
        
        // Reset the form
        this.registerForm.reset();
        
        // Redirect to login after 2 seconds
        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 2000);
      },
      error: (error) => {
        console.error('Registration error:', error);
        this.loading = false;
        
        // Better error handling
        if (error.status === 400) {
          const errorMsg = error.error;
          
          // Check if it's a duplicate email error
          if (typeof errorMsg === 'string' && 
              (errorMsg.includes('duplicate') || errorMsg.includes('UNIQUE KEY'))) {
            this.errorMessage = 'This email is already registered. Please use a different email or login.';
          } else if (errorMsg?.message) {
            this.errorMessage = errorMsg.message;
          } else {
            this.errorMessage = 'Registration failed. Please check your information.';
          }
        } else {
          this.errorMessage = 'An error occurred. Please try again later.';
        }
      }
    });
  }
}