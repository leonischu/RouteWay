import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { Auth } from '../../services/auth';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-navbar',
  imports: [RouterModule,CommonModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar {

  constructor(
  private authService: Auth,
  private router:Router
  ){}
    isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }
    getUserName(): string {
    return this.authService.getUserName();
  }

    logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }




}
