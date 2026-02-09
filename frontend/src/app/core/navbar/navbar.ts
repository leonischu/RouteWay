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
   userDetail: any;
  constructor(
  public authService: Auth,
  private router:Router
  ){}
    isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }
  
  ngOnInit() {
    this.getUserName();
  }

    getUserName(){
     this.userDetail=this.authService.getUserName();
  }

    logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
  viewProfile(){
    this.router.navigate(['/profile'])
  }




}
