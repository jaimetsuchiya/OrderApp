import { Inject, Injectable } from '@angular/core';
import { LOCAL_STORAGE, StorageService } from 'ngx-webstorage-service';
import { Order }  from '../model/order';

@Injectable()

export class LocalStorageService {
    // key that is used to access the data in local storageconst 
    STORAGE_KEY = 'local_order';

     constructor(@Inject(LOCAL_STORAGE) private storage: StorageService) { }

     public storeOnLocalStorage(order: Order): void {
        this.storage.set(this.STORAGE_KEY, order);
     }

     public retrieveFromLocalStorage() {
        this.storage.get(this.STORAGE_KEY) as Order;
     }
}