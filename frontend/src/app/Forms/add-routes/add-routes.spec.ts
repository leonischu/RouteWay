import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddRoutes } from './add-routes';

describe('AddRoutes', () => {
  let component: AddRoutes;
  let fixture: ComponentFixture<AddRoutes>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddRoutes]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddRoutes);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
