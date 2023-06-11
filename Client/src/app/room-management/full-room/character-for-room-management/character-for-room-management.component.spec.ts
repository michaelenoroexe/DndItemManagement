import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CharacterForRoomManagementComponent } from './character-for-room-management.component';

describe('CharacterForRoomManagementComponent', () => {
  let component: CharacterForRoomManagementComponent;
  let fixture: ComponentFixture<CharacterForRoomManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CharacterForRoomManagementComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CharacterForRoomManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
