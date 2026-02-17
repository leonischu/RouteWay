import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Api } from '../../services/api';
import { User } from '../../model/User-detail';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-users',
  imports: [CommonModule,RouterLink],
  templateUrl: './users.html',
  styleUrl: './users.css',
})
export class Users implements OnInit {
  users:User[]=[];

constructor(private apiService:Api,   private cdr: ChangeDetectorRef){}
  

ngOnInit(): void {
this.loadUsers();
  }

  
  loadUsers():void {
    this.apiService.getUser().subscribe({
      next :(res)=>{
        this.users = [...res.data];
        console.log(this.users)
        this.cdr.detectChanges();       

      }
    })
  }


}
