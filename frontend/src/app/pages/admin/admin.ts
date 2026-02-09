import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Auth } from '../../services/auth';
import { Router, RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-admin',
  standalone:true,
  imports: [CommonModule,RouterLink,RouterOutlet],
  templateUrl: './admin.html',
  styleUrl: './admin.css',
})
export class Admin {
 userDetail:any;
  constructor(
  public authService:Auth,
  private router:Router

){}
ngOnInit(){
  this.userDetail = this.authService.getUserName();
}
isLoggedIn(): boolean {
  return this.authService.isLoggedIn();
}
  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

}
