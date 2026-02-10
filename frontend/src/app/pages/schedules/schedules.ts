import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Api } from '../../services/api';
import { Schedule } from '../../model/schedule-info';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-schedules',
  standalone:true,
  imports: [CommonModule],
  templateUrl: './schedules.html',
  styleUrl: './schedules.css',
})
export class Schedules implements OnInit {
schedules: Schedule[] = [];
  from: string = '';
  to: string = '';
   isLoading: boolean = false;
  error: string = '';

 

constructor(
  private apiService:Api,
  //  private cdr: ChangeDetectorRef
){ }
 

  ngOnInit(): void {
    // this.loadSchedule();
      }



      getSchedule(){
        this.apiService.getSchedule(this.from , this.to).subscribe({
          next:(response) =>{
            this.schedules=response;
            console.log(this.schedules)
          }
        })
      }

// loadSchedule(): void {
//   // Validate input
//   if (!this.from || !this.to) {
//     this.error = 'Please enter both From and To cities.';
//     return;
//   }

//   this.isLoading = true;
//   this.error = '';

//   // Call service
//   this.apiService.getSchedule(this.from, this.to).subscribe({
//      next: (data: Schedule[]) => {
//         this.schedules = data;
//         this.isLoading = false;
//       },
//     error: (err) => {
//       console.error('Schedule API error:', err);
//       this.error = 'Failed to load schedules. Please try again.';
//       this.isLoading = false;
//     }
//   });
// }

}
