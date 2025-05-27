import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TravelOfferDetailsComponent } from './travel-offer-details.component';

describe('TravelOfferDetailsComponent', () => {
  let component: TravelOfferDetailsComponent;
  let fixture: ComponentFixture<TravelOfferDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TravelOfferDetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TravelOfferDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
