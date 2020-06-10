import { Ingredient } from './ingredient';
import { Sandwich } from './sandwich';

export class Order {
    id: string;
    tableNumber: number;
    status: number;
    totalItens: number;
    total: number;
    rules: [];
    sandwiches: OrderSandwich[];
  }

  export class NewOrder {
    tableNumber: number;
    totalAdditional: number;
    total: number;
    sandwich: OrderSandwich;
  }

export class OrderSandwich {
    id: string;
    sandwichId: string;
    sandwich: Sandwich;
    position: number;
    quantity: number;
    additionalIngredients: OrderIngredient[];
}

export class OrderIngredient{
    id: string;
    orderSandwichId: string;
    ingredientId: string;
    quantity: number;
    ingredient: Ingredient;
}

