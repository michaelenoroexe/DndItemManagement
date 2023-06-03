import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayItemComponent } from './play-item.component';

describe('PlayItemComponent', () => {
  let component: PlayItemComponent;
  let fixture: ComponentFixture<PlayItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlayItemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PlayItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
