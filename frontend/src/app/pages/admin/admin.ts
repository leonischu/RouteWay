import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Auth } from '../../services/auth';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-admin',
  standalone:true,
  imports: [CommonModule,RouterLink],
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

}
