import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DmPlayComponent } from './dm-play.component';

describe('DmPlayComponent', () => {
  let component: DmPlayComponent;
  let fixture: ComponentFixture<DmPlayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DmPlayComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DmPlayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
