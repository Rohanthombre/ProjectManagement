import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddViewProjectComponent } from './add-view-project/add-view-project.component';
import { AddViewUserComponent } from './add-view-user/add-view-user.component';
import { AddTaskComponent } from './add-task/add-task.component';
import { ViewTaskComponent } from './view-task/view-task.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TaskQueryPipe } from './task-query.pipe';
import { CommonSearchPipe } from './pipe/common-search.pipe';
import { SortPipe } from './pipe/sort.pipe';
import { SelectDropDownModule } from 'ngx-select-dropdown';
import { SearchTaskPipe } from './pipe/search-task.pipe';

@NgModule({
  declarations: [
    AppComponent,
    AddViewProjectComponent,
    AddViewUserComponent,
    AddTaskComponent,
    ViewTaskComponent,
    TaskQueryPipe,
    CommonSearchPipe,
    SortPipe,
    SearchTaskPipe
  ],
  imports: [
    BrowserAnimationsModule,
    BsDatepickerModule.forRoot(),
    BrowserModule,
    RouterModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    SelectDropDownModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  exports: [SearchTaskPipe]
})
export class AppModule { }
