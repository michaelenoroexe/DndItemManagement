import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GlobalRoomTableComponent } from './global-room-table.component';

describe('GlobalRoomTableComponent', () => {
  let component: GlobalRoomTableComponent;
  let fixture: ComponentFixture<GlobalRoomTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GlobalRoomTableComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GlobalRoomTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
