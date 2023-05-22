import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DmMenuComponent } from './dm-menu.component';

describe('DmMenuComponent', () => {
  let component: DmMenuComponent;
  let fixture: ComponentFixture<DmMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DmMenuComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DmMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
