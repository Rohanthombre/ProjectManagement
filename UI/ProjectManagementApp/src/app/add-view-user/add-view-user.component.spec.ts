import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddViewUserComponent } from './add-view-user.component';

describe('AddViewUserComponent', () => {
  let component: AddViewUserComponent;
  let fixture: ComponentFixture<AddViewUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddViewUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddViewUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
