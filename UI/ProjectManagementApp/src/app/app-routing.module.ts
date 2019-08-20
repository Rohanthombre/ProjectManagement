import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddViewProjectComponent } from './add-view-project/add-view-project.component';
import { AddViewUserComponent } from './add-view-user/add-view-user.component';
import { AddTaskComponent } from './add-task/add-task.component';
import { ViewTaskComponent } from './view-task/view-task.component';


const routes: Routes = [
  {path: 'add-view-project' , component: AddViewProjectComponent},
  {path: 'add-view-user', component: AddViewUserComponent},
  {path: 'add-task', component: AddTaskComponent},
  {path: 'view-task', component: ViewTaskComponent},
];

export const routing = RouterModule.forRoot(routes);

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
