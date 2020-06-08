import { Component, OnInit, Input } from '@angular/core';
import { Sandwich }  from '../../model/sandwich';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  @Input() public sandwiches: Sandwich[];

  constructor() { }

  ngOnInit(): void {
  }

}
