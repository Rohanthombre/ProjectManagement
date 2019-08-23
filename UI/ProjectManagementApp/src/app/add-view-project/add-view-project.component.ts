import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Project } from '../Models/project';
import { ProjectmanagementService } from '../service/projectmanagement.service';
import { FormBuilder } from '@angular/forms';
import { SelectDropDownModule } from 'ngx-select-dropdown';
import { User } from '../Models/user';

@Component({
  selector: 'app-add-view-project',
  templateUrl: './add-view-project.component.html',
  styleUrls: ['./add-view-project.component.css']
})
export class AddViewProjectComponent implements OnInit {
  pageTitle = 'Add Project';

  AddProjectFormGroup: FormGroup;
  projectName: FormControl;
  setDates: FormControl;
  startDate: FormControl;
  endDate: FormControl;
  priority: FormControl;
  managerName: FormControl;
  

  formSubmitted: boolean;
  isEditMode: boolean;
  editProjectId: number;



  searchForm: FormGroup;
  searchInputControl: FormControl;

  singleSelect: any = [];
  config = {
    displayKey: "FirstName" , //if objects array passed which key to be displayed defaults to description
    search: true,
    placeholder:"Select",
    noResultsFound: 'No results found!' ,
    searchPlaceholder:'Search'
  
  };
  managerOptions: any ;


  errorMessage = '';
  successMessage = '';

  @Input()
  projects: Project[];

  sortBy: String = "FirstName";

  searchProjectInputValue: string = "";

  createOrEditProjectModel: Project = {
    ProjectId: null,
    ProjectName: "null",
    StartDate: null,
    NoOfTasks:null,
    NoOfClosedTasks:null,
    EndDate: null,
    Priority: null,
    UserId: null
  };

  constructor(private projectManagementService: ProjectmanagementService, private fb: FormBuilder) { }

  ngOnInit() {

    

    this.projectName = new FormControl('', [Validators.required,
                                            Validators.minLength(2),
                                            Validators.maxLength(80)]);
    this.setDates = new FormControl(false);
    this.startDate = new FormControl('', Validators.required);
    this.endDate = new FormControl('', Validators.required);
    this.priority = new FormControl('', Validators.required);
    this.managerName = new FormControl('',Validators.required);

    this.AddProjectFormGroup = new FormGroup({
      projectName: this.projectName,
      setDates: this.setDates,
      startDate: this.startDate,
      endDate: this.endDate,
      priority: this.priority,
      managerName: this.managerName
    });

    this.initFormsControl();
    this.loadProjectDetails();
    this.loadManagerDetails();
    // this.AddProjectFormGroup = this.fb.group({
    //   projectName: this.projectName,
    //   setDates: this.setDates,
    //   startDate: this.startDate,
    //   endDate: this.endDate,
    //   priority: this.priority,
    //   managerName: this.managerName
    // });
  }

  onFormSubmit() {

    this.formSubmitted = true;

    if (this.AddProjectFormGroup.valid) {

      this.createOrEditProjectModel.ProjectName = this.AddProjectFormGroup.controls.projectName.value;
      this.createOrEditProjectModel.StartDate = this.AddProjectFormGroup.controls.startDate.value;
      this.createOrEditProjectModel.EndDate = this.AddProjectFormGroup.controls.endDate.value;
      this.createOrEditProjectModel.Priority = this.AddProjectFormGroup.controls.priority.value;
      this.createOrEditProjectModel.UserId = this.AddProjectFormGroup.controls.managerName.value.UserId;

      if (!this.isEditMode) {

        this.CreateProjectService(this.createOrEditProjectModel);
      }
      else {
        this.EditProjectService(this.createOrEditProjectModel);
      }
    }
  }

  CreateProjectService(projectModel: Project) {
    this.projectManagementService.createProject(projectModel).subscribe(
      () => {
        this.successMessage = 'Record saved Successfully';
        window.alert(this.successMessage);
        this.resetForm();

        this.loadProjectDetails();
      },
      error => this.errorMessage = <any>error
    );
  }

  EditProjectService(projectModel: Project) {
    this.projectManagementService.editProject(projectModel).subscribe(
      () => {
        this.successMessage = 'Record saved Successfully';
        window.alert(this.successMessage);
        this.resetForm();

        this.loadProjectDetails();
      },
      error => this.errorMessage = <any>error
    );
  }

  onEditProject(project: Project): void {
    this.isEditMode = true;
    this.editProjectId = project.ProjectId;
    this.AddProjectFormGroup.controls.projectName.setValue(project.ProjectName);
    this.AddProjectFormGroup.controls.startDate.setValue(project.StartDate);
    this.AddProjectFormGroup.controls.endDate.setValue(project.EndDate);
    this.AddProjectFormGroup.controls.priority.setValue(project.Priority);
    this.AddProjectFormGroup.controls.managerName.setValue(project.UserId);


  }

  onDeleteProject(project: Project): void {
    this.projectManagementService.deleteUser(project.ProjectId).subscribe(x => {
      this.successMessage = 'Record deleted Successfully';
      window.alert(this.successMessage);
      //this.router.navigate(["/AddUser"]);
      this.loadProjectDetails();
    });
  }

  loadProjectDetails(): void {
    this.projectManagementService.getAllProjects().subscribe(x => {
      this.projects = x;
    },
      error => this.errorMessage = <any>error
    );
  }
  loadManagerDetails(): void {
    this.projectManagementService.getAllUsers().subscribe(x => {
      this.managerOptions = x;
    },
      error => this.errorMessage = <any>error
    );
  }
  
  resetForm() {
    // this.newTaskForm.reset();
    this.AddProjectFormGroup.reset();
  }
  private initFormsControl() {
    this.searchInputControl = new FormControl(this.searchProjectInputValue);


    this.searchForm = new FormGroup({
      searchInputControl: this.searchInputControl
    });

    this.searchForm.valueChanges.subscribe(x => {
      this.searchProjectInputValue = this.searchInputControl.value;
    });
  }
}
