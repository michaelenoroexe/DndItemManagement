import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnauthorizedMenuComponent } from './unauthorized-menu.component';

describe('UnauthorizedMenuComponent', () => {
  let component: UnauthorizedMenuComponent;
  let fixture: ComponentFixture<UnauthorizedMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UnauthorizedMenuComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UnauthorizedMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
