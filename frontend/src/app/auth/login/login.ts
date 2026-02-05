import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { Auth } from '../../services/auth';
import { CommonModule } from '@angular/common';
import { provideToastr, ToastrService } from 'ngx-toastr';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
   loginForm: FormGroup;
  loading = false;
  errorMessage = '';
  toaster=inject(ToastrService)
 

  constructor(
    private fb: FormBuilder,
    private authService: Auth,
    private router: Router
  ) {
    // Create form
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  onSubmit(): void {
    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;
    this.errorMessage = '';

    this.authService.login(this.loginForm.value).subscribe({
      next: (response) => {
        this.loading = false;
        this.router.navigate(['/']); 
        Swal.fire({
        title: "Welcome",
        text: "Login Successful!",
        icon: "success"
        } );
        // //this.toaster.success('Login successfull',"Sucess");
        // alert('Login successful! ✅');
      },
      error: (error) => {
        this.loading = false;
        Swal.fire({
        title: "",
        text: "Please Check your username or password",
        icon: "error"
        } );
        // this.router.navigate(['/login']); 
          this.loginForm.reset();

      }
    });
  }

}
