import { Component, OnInit, ChangeDetectionStrategy    } from '@angular/core';
import { GuestServiceService } from '../guest-service.service';
import { takeUntil } from 'rxjs/operators';
import { Subject, Observable } from 'rxjs';
import { Sandwich}  from '../../model/sandwich';


@Component({
  selector: 'app-guest-area',
  templateUrl: './guest-area.component.html',
  styleUrls: ['./guest-area.component.css'],
  changeDetection: ChangeDetectionStrategy.Default
})

export class GuestAreaComponent implements OnInit  {

    constructor(public dataService: GuestServiceService) { 
    }

    sandwiches: Sandwich[];

    ngOnInit() {
      this.dataService.listSandwiches().subscribe((result) => {
        this.sandwiches = result as Sandwich[];
        console.log('listSandwiches', this.sandwiches);
      },
      (error) => {
        console.error(error);
      });
    };

    placeOrder(sandwich: Sandwich) {

    };

}
