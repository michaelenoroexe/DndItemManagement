import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FullRoomComponent } from './full-room.component';

describe('FullRoomComponent', () => {
  let component: FullRoomComponent;
  let fixture: ComponentFixture<FullRoomComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FullRoomComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FullRoomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
