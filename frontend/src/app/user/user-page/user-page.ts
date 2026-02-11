import { ChangeDetectorRef, Component } from '@angular/core';
import { Api } from '../../services/api';
import { Searches, searchResult } from '../../model/Searches';
import { errorContext } from 'rxjs/internal/util/errorContext';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-page',
  imports: [CommonModule,FormsModule],
  templateUrl: './user-page.html',
  styleUrl: './user-page.css',
})
export class UserPage {
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

  constructor(
     private apiService:Api,
     private cdr: ChangeDetectorRef

  ){}
  ngOnInit():void{
 this.performSearch();
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


}
