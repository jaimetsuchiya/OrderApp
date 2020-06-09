import { Component, OnInit } from '@angular/core';
import { Sandwich }  from '../../model/sandwich';
import { NewOrder, OrderIngredient, OrderSandwich }  from '../../model/order';
import { LocalStorageService } from '../local-storage-service.service'
import { ActivatedRoute } from '@angular/router';
import { GuestServiceService } from '../guest-service.service';
import { Ingredient } from 'src/model/ingredient';

@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
  styleUrls: ['./order-form.component.css']
})

export class OrderFormComponent implements OnInit {
  id: string;
  private sub: any;
  order: NewOrder;

  constructor(public dataService: GuestServiceService, public localService: LocalStorageService, private router: ActivatedRoute) { }

  ngOnInit(): void {
    this.sub = this.router.params.subscribe(params => {
      this.id = params['id']; 
      this.dataService.getSandwiches(this.id).subscribe((result) => {
        this.order = new NewOrder();
        this.order.sandwich = new OrderSandwich();
        this.order.sandwich.sandwich = result as Sandwich;
        this.order.sandwich.additionalIngredients = new Array<OrderIngredient>();

        this.dataService.listIngredients().subscribe((result) => {
          let ingredientsList = result.sort((a,b)=>a.name.localeCompare(b.name)) as Ingredient[];

          for( var i=0; i < ingredientsList.length; i++ ) {
            var tmp = new OrderIngredient();
            tmp.quantity = 0;
            tmp.ingredient = ingredientsList[i];
            tmp.ingredientId = ingredientsList[i].id;
            this.order.sandwich.additionalIngredients.push(tmp);
          }

        });
      },
      (error) => {
        console.error(error);
      });

    });
  }

  
}
