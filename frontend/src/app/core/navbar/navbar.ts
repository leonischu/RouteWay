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
    // this.userDetail = this.authService.getUserName();
  }

  //  getUserName() {
  //    this.authService.getUserName().subscribe({
  //     next:(response) =>{
  //       this.userDetail=response;
  //     },
  //     error:(err:any) =>{
  //       console.error("Error fetching username",err)
  //     }
  //    });
  // }
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
