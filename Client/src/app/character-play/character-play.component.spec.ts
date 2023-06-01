import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CharacterPlayComponent } from './character-play.component';

describe('CharacterPlayComponent', () => {
  let component: CharacterPlayComponent;
  let fixture: ComponentFixture<CharacterPlayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CharacterPlayComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CharacterPlayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
