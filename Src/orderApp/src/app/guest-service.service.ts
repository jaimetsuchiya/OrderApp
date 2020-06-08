import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { environment } from '../environments/environment';
import { retry, catchError, tap } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { Sandwich} from '../model/sandwich'
import {Observable} from 'rxjs';    
import { Order } from 'src/model/order';

@Injectable({
  providedIn: 'root'
})

export class GuestServiceService {

  constructor(private httpClient: HttpClient) { }

  public serviceURL(controllerName) {
    return environment.serviceURL + controllerName;
  }

  public createHeaders() {
    let headers = new HttpHeaders();

    headers.set('Content-Type', 'application/json; charset=utf-8');
    headers.set('Access-Control-Allow-Origin', '*');
    headers.set('Access-Control-Allow-Credentials', 'true');
    headers.set('Access-Control-Allow-Methods', 'GET, POST, PUT');
    headers.set('Access-Control-Allow-Headers', '*');
    headers.set('credentials','include');
    // headers.set('Authorization', 'Bearer ' + environment.guestToken);
    
    return headers;
  }
  
  public listSandwiches():Observable<Sandwich[]> {
    let headers = this.createHeaders();
    let url = this.serviceURL('sandwich');

    return this.httpClient.get<Sandwich[]>(url, {headers});
  };

  public placeOrder(order: Order) {
    let headers = this.createHeaders();
    let url = this.serviceURL('order');

    return this.httpClient.post<Order>(url, order, {headers});
  };
}
