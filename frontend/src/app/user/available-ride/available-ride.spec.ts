import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AvailableRide } from './available-ride';

describe('AvailableRide', () => {
  let component: AvailableRide;
  let fixture: ComponentFixture<AvailableRide>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AvailableRide]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AvailableRide);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
