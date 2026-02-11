import { ChangeDetectorRef, Component } from '@angular/core';
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
  private router:Router,
  private cdr: ChangeDetectorRef


  ){}
    isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }
  
  ngOnInit() {
    this.getUserName();
  }

    getUserName(){
     this.userDetail=this.authService.getUserName();
           this.cdr.detectChanges();        

  }

    logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
  viewProfile(){
    this.router.navigate(['/profile'])
          this.cdr.detectChanges();        

  }




}
