import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenControllPanelComponent } from './gen-control-panel.component';

describe('GenControllPanelComponent', () => {
  let component: GenControllPanelComponent;
  let fixture: ComponentFixture<GenControllPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GenControllPanelComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GenControllPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
