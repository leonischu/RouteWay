import { ChangeDetectorRef, Component } from '@angular/core';
import { Api } from '../../services/api';
import { Searches, searchResult } from '../../model/Searches';
import { errorContext } from 'rxjs/internal/util/errorContext';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Schedule } from '../../model/schedule-info';

@Component({
  selector: 'app-user-page',
  imports: [CommonModule,FormsModule],
  templateUrl: './user-page.html',
  styleUrl: './user-page.css',
})
export class UserPage {
  schedules:Schedule[] = [];
  newSearch: Searches = {
    from: '',
    to: '',
    travelDate: '',
    startTime: '',
    endTime: '',
    maxPrice: 0
  };

  searchResults:searchResult[] = [];
  searchCompleted: boolean = false;
  error: string = '';
  

  constructor(
     private apiService:Api,
     private cdr: ChangeDetectorRef

  ){}
  ngOnInit():void{
 this.performSearch();
 this.loadSchedule();
 this.loadRides();
  }
  performSearch(){
    this.searchCompleted = false; 
        console.log(this.newSearch)
    this.apiService.search(this.newSearch).subscribe(
      (data)=>{
        this.searchResults = data;
         this.searchCompleted = true;
              this.cdr.detectChanges();     
    
      },
      (error)=> {
        console.error('Search error', error);
      }
    );
  }

  loadSchedule():void{
    this.apiService.getSchedule().subscribe({
      next: (res:Schedule[]) => {
        this.schedules=[...res];
        console.log(this.schedules);
        this.cdr.detectChanges();       
      },
     error:(err) => console.error(err)
    })
  }


loadRides(): void {
    this.apiService.getAllSearch().subscribe(
      (searchResults: searchResult[]) => {
        this.searchResults = searchResults;
        console.log( this.searchResults);
      },
       (err) => {
        console.error('Error loading rides', err);
        this.cdr.detectChanges();
      }
    );
  }



}
