import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VehicleRoute } from './vehicle-route';

describe('VehicleRoute', () => {
  let component: VehicleRoute;
  let fixture: ComponentFixture<VehicleRoute>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VehicleRoute]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VehicleRoute);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
