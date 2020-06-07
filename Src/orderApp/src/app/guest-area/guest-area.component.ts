import { Component, OnInit  } from '@angular/core';
import { GuestServiceService } from '../guest-service.service';
import {  takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { Sandwich}  from '../../model/sandwich';
import {Observable} from 'rxjs';    


@Component({
  selector: 'app-guest-area',
  templateUrl: './guest-area.component.html',
  styleUrls: ['./guest-area.component.css']
})

export class GuestAreaComponent {

  constructor(public dataService: GuestServiceService) { 
    this.onAsync();
    this.onSubscribe();
  }
  
  public response:Observable<Sandwich[]>;
  public sandwiches:Sandwich[];

  onSubscribe()
  {
    this.dataService.listSandwiches().subscribe(result=>{
      this.sandwiches=result;
    })
  }
  onAsync()
  {
   this.response= this.dataService.listSandwiches();
  }
}
