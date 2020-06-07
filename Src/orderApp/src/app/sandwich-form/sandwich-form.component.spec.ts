import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SandwichFormComponent } from './sandwich-form.component';

describe('SandwichFormComponent', () => {
  let component: SandwichFormComponent;
  let fixture: ComponentFixture<SandwichFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SandwichFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SandwichFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
