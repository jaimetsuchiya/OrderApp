import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SandwichListComponent } from './sandwich-list.component';

describe('SandwichListComponent', () => {
  let component: SandwichListComponent;
  let fixture: ComponentFixture<SandwichListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SandwichListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SandwichListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
