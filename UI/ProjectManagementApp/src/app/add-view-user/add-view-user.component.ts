import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Task } from '../Models/task';
import { User } from '../Models/user';
import { ProjectmanagementService } from '../service/projectmanagement.service';

@Component({
  selector: 'app-add-view-user',
  templateUrl: './add-view-user.component.html',
  styleUrls: ['./add-view-user.component.css']
})
export class AddViewUserComponent implements OnInit {
  pageTitle = 'Add User';


  AddUserFormGroup: FormGroup;
  firstName: FormControl;
  lastName: FormControl;
  employeeId: FormControl;

  searchForm: FormGroup;
  searchInputControl: FormControl;

  formSubmitted: boolean;
  isEditMode: boolean;
  editUserId: number;

  errorMessage = '';
  successMessage = '';

  @Input()
  users: User[];

  sortBy: String = "FirstName";

  searchUserInputValue: string = "";

  createOrEditUserModel: User = {
    UserId: null,
    TaskId: null,
    ProjectId: null,
    FirstName: "",
    LastName: "",
    EmployeeId: null
  };

  constructor(private projectManagementService: ProjectmanagementService) 
  { 
    this.initFormsControl();
  }

  ngOnInit() {
    this.pageTitle = 'Add User';

    this.firstName = new FormControl('', [Validators.required,
    Validators.minLength(2),
    Validators.maxLength(80)]);
    this.lastName = new FormControl('', [Validators.required,
    Validators.minLength(2),
    Validators.maxLength(80)]);
    this.employeeId = new FormControl('', Validators.required);

    this.AddUserFormGroup = new FormGroup({
      firstName: this.firstName,
      lastName: this.lastName,
      employeeId: this.employeeId
    });

    this.loadUserDetails();

  }

  onFormSubmit() {

    this.formSubmitted = true;

    if (this.AddUserFormGroup.valid) {
     
      this.createOrEditUserModel.FirstName = this.AddUserFormGroup.controls.firstName.value;
      this.createOrEditUserModel.LastName = this.AddUserFormGroup.controls.lastName.value;
      this.createOrEditUserModel.EmployeeId = this.AddUserFormGroup.controls.employeeId.value;

      if (!this.isEditMode) {

        this.CreateUserService(this.createOrEditUserModel);
      }
      else
      {
        this.EditUserService(this.createOrEditUserModel);
      }
    }
  }

  CreateUserService(userModel: User) {
    this.projectManagementService.createUser(userModel).subscribe(
      () => {
        this.successMessage = 'Record saved Successfully';
        window.alert(this.successMessage);
        this.resetForm();

        this.loadUserDetails();
      },
      error => this.errorMessage = <any>error
    );
  }

  EditUserService(userModel: User) {
    this.projectManagementService.editUser(userModel).subscribe(
      () => {
        this.successMessage = 'Record saved Successfully';
        window.alert(this.successMessage);
        this.resetForm();

        this.loadUserDetails();
      },
      error => this.errorMessage = <any>error
    );
  }

  onEditUser(user: User): void{
    this.isEditMode = true;
    this.editUserId = user.UserId;
    this.AddUserFormGroup.controls.firstName.setValue(user.FirstName);
    this.AddUserFormGroup.controls.lastName.setValue(user.LastName);
    this.AddUserFormGroup.controls.employeeId.setValue(user.EmployeeId);
  }

  onDeleteUser(user: User): void {
    this.projectManagementService.deleteUser(user.UserId).subscribe(x => {
      this.successMessage = 'Record deleted Successfully';
        window.alert(this.successMessage);
      //this.router.navigate(["/AddUser"]);
      this.loadUserDetails();
    });
  }

  onSort(field: string): void {
    this.sortBy = field;
  }

  loadUserDetails(): void {
    this.projectManagementService.getAllUsers().subscribe(x => {
      this.users = x;
    },
      error => this.errorMessage = <any>error
    );
  }

  isEmpty(val) {
    return (val === undefined || val == null || val.length <= 0) ? true : false;
  }
  resetForm() {
    // this.newTaskForm.reset();
    this.AddUserFormGroup.reset();
  }
  private initFormsControl() {
    this.searchInputControl = new FormControl(this.searchUserInputValue);
    

    this.searchForm = new FormGroup({
      searchInputControl: this.searchInputControl
    });

    this.searchForm.valueChanges.subscribe(x=>
      {
        this.searchUserInputValue=this.searchInputControl.value;
      });
  }


}
