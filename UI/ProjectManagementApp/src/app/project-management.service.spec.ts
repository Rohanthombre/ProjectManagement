import { TestBed } from '@angular/core/testing';

import { ProjectManagementService } from './project-management.service';

describe('ProjectManagementService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ProjectManagementService = TestBed.get(ProjectManagementService);
    expect(service).toBeTruthy();
  });
});
