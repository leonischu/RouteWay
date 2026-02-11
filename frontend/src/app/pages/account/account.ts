import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { UserDetail } from '../../model/userDetail';
import { Auth } from '../../services/auth';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-account',
  imports: [CommonModule],
  templateUrl: './account.html',
  styleUrl: './account.css',
})
export class Account implements OnInit{
  user:UserDetail | null =null;
 constructor(private authService:Auth,private router: Router,private cdr: ChangeDetectorRef){}


 ngOnInit(): void {
  this.authService.getUserProfile().subscribe({
    next: (data) => {
      console.log('Profile data received:', data);
      this.user = data;
            this.cdr.detectChanges();        

    },
    error: (err) => {
      console.error('Error loading profile:', err);
    }
  });
}

 logout():void{
  // localStorage.removeItem('token');
  // this.user=null;
  // this.router.navigate(['/login'])
     this.authService.logout();
    this.router.navigate(['/login']);
 }
}
