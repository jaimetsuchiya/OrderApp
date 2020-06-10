import { Component, OnInit } from '@angular/core';
import { Sandwich }  from '../../model/sandwich';
import { NewOrder, OrderIngredient, OrderSandwich }  from '../../model/order';
import { LocalStorageService } from '../local-storage-service.service'
import { ActivatedRoute, Router } from '@angular/router';
import { GuestServiceService } from '../guest-service.service';
import { Ingredient } from 'src/model/ingredient';
import { FormGroup, FormBuilder, Validators }  from '@angular/forms';

@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
  styleUrls: ['./order-form.component.css']
})

export class OrderFormComponent implements OnInit {
  id: string;
  private sub: any;
  order: NewOrder;
  message: string = '';
  messageType: string = '';
  formIsValid: boolean = false;

  constructor(private formBuilder: FormBuilder, public dataService: GuestServiceService, public localService: LocalStorageService, private activeRouter: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.sub = this.activeRouter.params.subscribe(params => {
      this.id = params['id']; 
      this.dataService.getSandwiches(this.id).subscribe((result) => {
        this.order = new NewOrder();
        this.order.sandwich = new OrderSandwich();
        this.order.sandwich.sandwich = result as Sandwich;
        this.order.sandwich.quantity = 1;
        this.order.sandwich.sandwichId = this.order.sandwich.sandwich.id;
        this.order.sandwich.additionalIngredients = new Array<OrderIngredient>();
        this.order.totalAdditional = 0;

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

        console.log('order', this.order);
      },
      (error) => {
        console.error(error);
        this.setMessage("Lanche não encontrado!", 'alert-danger');
      });

    });
  }


  recalculate(): void {
    console.log('calcularTotalItens exec');

    this.order.totalAdditional = 0;
    for( var i=0; i < this.order.sandwich.additionalIngredients.length; i++) {
      this.order.totalAdditional+= ( this.order.sandwich.additionalIngredients[i].ingredient.price * this.order.sandwich.additionalIngredients[i].quantity );
    }
    this.order.total = (this.order.totalAdditional + this.order.sandwich.sandwich.price) * this.order.sandwich.quantity;
    console.log('calcularTotalItens', this.order.totalAdditional);

    this.formIsValid = (this.order.total > 0 && this.order.tableNumber > 0 && this.order.sandwich.position > 0);
  }  

  back(): void {
    this.router.navigateByUrl(`/menu`);
  }

  buy(): void {
  
    if(this.formIsValid){
      
      this.localService.storeOrder(this.order);
      this.setMessage("Pedido inserido!", 'success');

    } else {
      this.setMessage("Informe todos os dados antes de continuar!", 'danger');
    }

  }

  setMessage(message, messageType){
    this.message = message;
    this.messageType = messageType;
  }
}




