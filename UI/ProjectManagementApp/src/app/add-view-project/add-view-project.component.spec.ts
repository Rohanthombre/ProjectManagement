import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddViewProjectComponent } from './add-view-project.component';

describe('AddViewProjectComponent', () => {
  let component: AddViewProjectComponent;
  let fixture: ComponentFixture<AddViewProjectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddViewProjectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddViewProjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
