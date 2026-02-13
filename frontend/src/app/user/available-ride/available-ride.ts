import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Api } from '../../services/api';
import { Searches } from '../../model/Searches';

@Component({
  selector: 'app-available-ride',
  imports: [],
  templateUrl: './available-ride.html',
  styleUrl: './available-ride.css',
})
export class AvailableRide implements OnInit {
  search:Searches[]=[];
  constructor(
    private apiService:Api,
    private cdr:ChangeDetectorRef    
  ){}
  ngOnInit(): void {
    
  }

 

}
