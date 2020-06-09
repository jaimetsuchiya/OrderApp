import { Component, OnInit, Input } from '@angular/core';
import { Sandwich }  from '../../model/sandwich';
import { LocalStorageService } from '../local-storage-service.service'
import { ActivatedRoute } from '@angular/router';
import { GuestServiceService } from '../guest-service.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  @Input() public sandwiches: Sandwich[];
  
  id: string;
  private sub: any;
  sandwich: Sandwich;

  constructor(  public dataService: GuestServiceService, public localService: LocalStorageService, private router: ActivatedRoute) { }

  ngOnInit(): void {
    this.sub = this.router.params.subscribe(params => {
      this.id = params['id']; 
      this.dataService.getSandwiches(this.id).subscribe((result) => {
        this.sandwich = result as Sandwich;

      },
      (error) => {
        console.error(error);
      });

    });
  }

}
