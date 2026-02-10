import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Fares } from './fares';

describe('Fares', () => {
  let component: Fares;
  let fixture: ComponentFixture<Fares>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Fares]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Fares);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
