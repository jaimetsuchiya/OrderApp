import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";


import { AppComponent } from './app.component';
import { AdminAreaComponent } from './admin-area/admin-area.component';
import { GuestAreaComponent } from './guest-area/guest-area.component';
import { SandwichListComponent } from './sandwich-list/sandwich-list.component';
import { IngredientListComponent } from './ingredient-list/ingredient-list.component';
import { SandwichFormComponent } from './sandwich-form/sandwich-form.component';
import { IngredientFormComponent } from './ingredient-form/ingredient-form.component';
import { OrderFormComponent } from './order-form/order-form.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';
import { MenuComponent } from './menu/menu.component';
import { HttpClientModule } from '@angular/common/http';
import { StorageServiceModule } from 'ngx-webstorage-service';
import { AppBootstrapModule } from './app-bootstrap.module';
import { LocalStorageService } from './local-storage-service.service'

const routes: Routes = [
  { path: 'menu', component: GuestAreaComponent },
  { path: 'admin', component: AdminAreaComponent },
  { path: 'order/:id', component: OrderFormComponent },
  { path: 'shopping-cart', component: ShoppingCartComponent },
];


@NgModule({
  declarations: [
    AppComponent,
    SandwichListComponent,
    IngredientListComponent,
    SandwichFormComponent,
    IngredientFormComponent,
    OrderFormComponent,
    ShoppingCartComponent,
    MenuComponent,
    GuestAreaComponent,
    AdminAreaComponent
  ],
  imports: [
    FormsModule, ReactiveFormsModule,
    AppBootstrapModule,
    CommonModule,
    MatToolbarModule,
    HttpClientModule,
    MatMenuModule,
    MatIconModule,
    BrowserModule,
    RouterModule.forRoot(routes,  { enableTracing: false }),
    BrowserAnimationsModule,
    StorageServiceModule
  ],
  providers: [LocalStorageService],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
