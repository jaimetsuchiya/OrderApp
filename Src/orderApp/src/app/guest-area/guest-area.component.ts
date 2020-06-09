import { Component, OnInit, ChangeDetectionStrategy    } from '@angular/core';
import { GuestServiceService } from '../guest-service.service';
import { Sandwich}  from '../../model/sandwich';
import { Router } from '@angular/router';
import { LocalStorageService } from '../local-storage-service.service'


@Component({
  selector: 'app-guest-area',
  templateUrl: './guest-area.component.html',
  styleUrls: ['./guest-area.component.css'],
  changeDetection: ChangeDetectionStrategy.Default
})

export class GuestAreaComponent implements OnInit  {

    constructor(public dataService: GuestServiceService, public localService: LocalStorageService, private router: Router ) { 
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

      this.localService.storeSandwich(sandwich);
      this.router.navigateByUrl(`/order/${sandwich.id}`);

    };

}
