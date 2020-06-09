import { Component, OnInit } from '@angular/core';
import { GuestServiceService } from '../guest-service.service';
import { Router } from '@angular/router';
import { Sandwich}  from '../../model/sandwich';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {

  constructor(public dataService: GuestServiceService, private router: Router) { }
  sandwich: Sandwich;
  

  ngOnInit(): void {
  }

}
