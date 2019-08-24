import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl, ValidatorFn } from '@angular/forms';
import { Project } from '../Models/project';
import { ProjectmanagementService } from '../service/projectmanagement.service';
import { FormBuilder } from '@angular/forms';
import { SelectDropDownModule } from 'ngx-select-dropdown';
import { User } from '../Models/user';
import * as Const from "src/app/const/const";
import { getLocaleDateTimeFormat } from '@angular/common';
import {DatePipe} from '@angular/common';

@Component({
  selector: 'app-add-view-project',
  templateUrl: './add-view-project.component.html',
  styleUrls: ['./add-view-project.component.css']
})
export class AddViewProjectComponent implements OnInit {
  pageTitle = 'Add Project';

  readonly priorityMin = Const.priorityMin;
  readonly priorityMax = Const.priorityMax;

  AddProjectFormGroup: FormGroup;
  projectName: FormControl;
  setDates: FormControl;
  startDate: FormControl;
  endDate: FormControl;
  priority: FormControl;
  managerName: FormControl;

  dateFormGroup: FormGroup;

  formSubmitted: boolean;
  isEditMode: boolean;
  editProjectId: number;



  searchForm: FormGroup;
  searchInputControl: FormControl;

  singleSelect: any = [];
  config = {
    displayKey: "FirstName", //if objects array passed which key to be displayed defaults to description
    search: true,
    placeholder: "Select",
    noResultsFound: 'No results found!',
    searchPlaceholder: 'Search'

  };
  managerOptions: any;


  errorMessage = '';
  successMessage = '';

  @Input()
  projects: Project[];

  sortBy: String = "ProjectName";

  searchProjectInputValue: string = "";

  createOrEditProjectModel: Project = {
    ProjectId: null,
    ProjectName: "null",
    StartDate: null,
    NoOfTasks: null,
    NoOfClosedTasks: null,
    EndDate: null,
    Priority: null,
    UserId: null
  };

  constructor(private projectManagementService: ProjectmanagementService, private fb: FormBuilder) { 
    this.initFormsControl(); 
  }

  ngOnInit() {

    this.loadProjectDetails();
    this.loadManagerDetails();

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
    else
    {
      this.validateAllFormFields(this.AddProjectFormGroup);
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

    this.projectName = new FormControl('', [Validators.required,
    Validators.minLength(2),
    Validators.maxLength(80)]);
    this.setDates = new FormControl(false);
    this.startDate = new FormControl('', Validators.required);
    this.endDate = new FormControl('', Validators.required);
    this.priority = new FormControl('', [Validators.required,
                                        Validators.min(Const.priorityMin),
                                        Validators.max(Const.priorityMax)]);
    this.managerName = new FormControl('', Validators.required);

    this.AddProjectFormGroup = new FormGroup({
      projectName: this.projectName,
      setDates: this.setDates,
      startDate: this.startDate,
      endDate: this.endDate,
      priority: this.priority,
      managerName: this.managerName
    });

    this.dateFormGroup = new FormGroup({
      startDateControl: this.startDate,
      endDateControl: this.startDate
    });


    this.startDate.disable();
    this.endDate.disable();

    this.dateFormGroup = new FormGroup({
      setDates: this.setDates,
      startDate: this.startDate,
      endDate: this.endDate
    });

    //this.dateFormGroup.setValidators(dateValidator);

    this.setDates.valueChanges.subscribe(x => {
      if (this.setDates.value as boolean === true) {
        this.startDate.enable();
        this.startDate.setValidators([Validators.required]);

        this.endDate.enable();
        this.endDate.setValidators([Validators.required]);

        let dataValidation = DateMustbeGreaterThanValidation(
          "startDate",
          "endDate"
        );

        this.dateFormGroup.setValidators(dataValidation);
      }
      else {
        this.startDate
        this.startDate.disable();
        this.startDate.clearValidators();

        this.endDate.disable();
        this.endDate.clearValidators();

        this.dateFormGroup.clearValidators();
      }
    });

    this.searchInputControl = new FormControl(this.searchProjectInputValue);

    this.searchForm = new FormGroup({
      searchInputControl: this.searchInputControl
    });

    this.searchForm.valueChanges.subscribe(x => {
      this.searchProjectInputValue = this.searchInputControl.value;
    });
  }
 validateAllFormFields(formGroup: FormGroup) {         //{1}
  Object.keys(formGroup.controls).forEach(field => {  //{2}
    const control = formGroup.get(field);             //{3}
    if (control instanceof FormControl) {             //{4}
      control.markAsTouched({ onlySelf: true });
    } else if (control instanceof FormGroup) {        //{5}
      this.validateAllFormFields(control);            //{6}
    }
  });
}

}

export function DateMustbeGreaterThanValidation(startDateFormControlname: string, endDateFormControlname: string): ValidatorFn {
  return (control: AbstractControl): { [s: string]: boolean } | null => {

    const invalidOj = { "DateRange": true };
    const startDateFormControl = control.get(startDateFormControlname);
    const endDateFormControl = control.get(endDateFormControlname);
    if (startDateFormControl.valid && endDateFormControl.valid) {
      const stardDate = Date.parse(startDateFormControl.value);
      const endDate = Date.parse(endDateFormControl.value);
      if (stardDate > endDate) {
        control.setErrors({ 'DateRange': true });
        return invalidOj;
      }
      return null;
    }
    return null;

  }
}