import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoomWithDmForFullTableComponent } from './room-with-dm-for-full-table.component';

describe('RoomWithDmForFullTableComponent', () => {
  let component: RoomWithDmForFullTableComponent;
  let fixture: ComponentFixture<RoomWithDmForFullTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RoomWithDmForFullTableComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RoomWithDmForFullTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
