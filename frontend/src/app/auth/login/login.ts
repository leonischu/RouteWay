import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { Auth } from '../../services/auth';
import { CommonModule } from '@angular/common';
import { provideToastr, ToastrService } from 'ngx-toastr';
import Swal from 'sweetalert2';

declare const google: any;

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

        const role = this.authService.getUserRole();
        if(role === 'Admin'){
          this.router.navigate(['/adminPage']);
        }
        else{
          this.router.navigate(['/User'])
        }
        Swal.fire({
        title: "Welcome",
        text: "Login Successful!",
        icon: "success"
        } );
        // //this.toaster.success('Login successfull',"Sucess");
        // alert('Login successful! ');
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

 // Google Button Setup (component only)
ngAfterViewInit(): void {
  google.accounts.id.initialize({
    client_id: '596508608140-eic4vkkoud60jd15mrrokvf0e6a4ef5a.apps.googleusercontent.com',
    callback: (response: any) => this.handleGoogleLogin(response),

     
  });

  google.accounts.id.renderButton(
    document.getElementById('googleButton'),
    {
      theme: 'outline',
      size: 'large',
      width: 300
    }
  );


}

 // Google Login Handler (component only)
handleGoogleLogin(response: any): void {
  const idToken = response.credential;

  this.authService.googleLogin(idToken).subscribe({
    next: (res) => {
      localStorage.setItem('token', res.token);
      this.router.navigate(['/User']);
    },
    error: () => {
      this.errorMessage = 'Google login failed';
    }
  });
}


}
