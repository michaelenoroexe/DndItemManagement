import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SigninMenuComponent } from './signin-menu.component';

describe('SigninMenuComponent', () => {
  let component: SigninMenuComponent;
  let fixture: ComponentFixture<SigninMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SigninMenuComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SigninMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
