import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrationMenuComponent } from './registration-menu.component';

describe('RegistrationMenuComponent', () => {
  let component: RegistrationMenuComponent;
  let fixture: ComponentFixture<RegistrationMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegistrationMenuComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegistrationMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
