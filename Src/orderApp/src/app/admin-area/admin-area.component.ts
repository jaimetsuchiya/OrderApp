import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { GuestServiceService } from '../guest-service.service';
import { Sandwich }  from '../../model/sandwich';
import { Router } from '@angular/router';
import { Ingredient }  from '../../model/ingredient';
import { LocalStorageService } from '../local-storage-service.service'
import { FormGroup, FormBuilder, Validators }  from '@angular/forms';

@Component({
  selector: 'app-admin-area',
  templateUrl: './admin-area.component.html',
  styleUrls: ['./admin-area.component.css'],
  changeDetection: ChangeDetectionStrategy.Default
})

export class AdminAreaComponent implements OnInit {

  sandwiches: Sandwich[];
  ingredients: Ingredient[];

  constructor(public dataService: GuestServiceService, public localService: LocalStorageService, private router: Router) { 
  }
  

  ngOnInit(): void {
    this.dataService.listSandwiches().subscribe((result) => {
      this.sandwiches = result as Sandwich[];
    },
    (error) => {
      console.error(error);
    });

    this.dataService.listIngredients().subscribe((result) => {
      this.ingredients = result as Ingredient[];
    },
    (error) => {
      console.error(error);
    });
  };

  saveIngredient(ingredient) {

  };
  
}
