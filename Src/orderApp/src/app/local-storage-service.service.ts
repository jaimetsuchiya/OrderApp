import { Inject, Injectable } from '@angular/core';
import { LOCAL_STORAGE, StorageService } from 'ngx-webstorage-service';
import { NewOrder, Order }  from '../model/order';
import { Sandwich } from 'src/model/sandwich';

// key that is used to access the data in local storageconst  STORAGE_KEY = 'local_todolist';

 @Injectable()
export class LocalStorageService {
    // key that is used to access the data in local storageconst 
     constructor(@Inject(LOCAL_STORAGE) private storage: StorageService) { }

     public storeOrder(order: NewOrder): void {

         var arr = this.storage.get("LOCAL_ORDER") as Order[];
         if( arr == null )
            arr = [];

         var idx = arr.findIndex(x=>x.tableNumber == order.tableNumber);
         if( idx < 0 ) {
            
            var newOrder = new Order()
                newOrder.tableNumber = order.tableNumber
                newOrder.sandwiches= [];
                newOrder.sandwiches.push(order.sandwich);
            arr.push(newOrder);
         }
         else {
            
            var tmp = arr[idx];
            var pos = tmp.sandwiches.findIndex(x=>x.position == order.sandwich.position );
            if (pos < 0)
               arr[idx].sandwiches.push(order.sandwich);
            else {
               arr[idx].sandwiches[pos] = order.sandwich;
            }

         }
            

        this.storage.set("LOCAL_ORDER", order);
     }

     public retrieveOrder(tableNumber: number) {
        this.storage.get("LOCAL_ORDER") as Order;
     }

     public storeSandwich(sandwich: Sandwich): void {
      this.storage.set("LOCAL_SANDWICH", sandwich);
     }

      public retrieveSandwich() {
         this.storage.get("LOCAL_SANDWICH") as Sandwich;
      }
}