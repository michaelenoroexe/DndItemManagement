import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StandartItemComponent } from './standart-item.component';

describe('StandartItemComponent', () => {
  let component: StandartItemComponent;
  let fixture: ComponentFixture<StandartItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StandartItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StandartItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
