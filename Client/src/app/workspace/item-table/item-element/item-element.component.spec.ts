import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemElementComponent } from './item-element.component';

describe('ItemElementComponent', () => {
  let component: ItemElementComponent;
  let fixture: ComponentFixture<ItemElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ItemElementComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ItemElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
