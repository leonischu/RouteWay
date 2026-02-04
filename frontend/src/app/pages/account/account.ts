import { Component, OnInit } from '@angular/core';
import { UserDetail } from '../../model/userDetail';
import { Auth } from '../../services/auth';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-account',
  imports: [CommonModule],
  templateUrl: './account.html',
  styleUrl: './account.css',
})
export class Account implements OnInit{
  user:UserDetail | null =null;
 constructor(private authService:Auth){}
 ngOnInit(): void {
   this.authService.getUserProfile().subscribe(data => {
    this.user = data;
   });
 }
}
