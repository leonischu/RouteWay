import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Navbar } from './core/navbar/navbar';
import { Footer } from './core/footer/footer';
import { Auth } from './services/auth';
import { UserDetail } from './model/userDetail';
import { Token } from '@angular/compiler';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,Navbar,Footer],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('frontend');

// authService = inject(Auth)
//   userModel : UserDetail | null = null
//   token : Token | null = null

//   getUserName(){
//     const myToken = localStorage.getItem('token')
//     if(myToken){
//       this.authService.getUserName().subscribe({
//         next: (response) =>{
//           // console.log(response)
//           this.userModel = response
//         },
//         error: (err: any) => {
//           console.error("Error fetching data", err)
//         }
//       })
//     }else{
//       console.error('No Token Found')
//     }
//   }
}
