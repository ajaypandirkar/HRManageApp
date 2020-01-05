import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { EmployeeListComponent } from './employees/employee-list/employee-list/employee-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';
import { EmployeeDetailComponent } from './employees/employee-list/employee-detail/employee-detail.component';
import { EmployeeDetailResolver } from './_resolvers/employee-detail.resolver';
import { EmployeeListResolver } from './_resolvers/employee-list.resolver';

export const appRoutes: Routes = [
    { path: 'home', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'employees', component: EmployeeListComponent,
                resolve: {users: EmployeeListResolver}},
            { path: 'employees/:id', component: EmployeeDetailComponent,
                resolve: {user: EmployeeDetailResolver}},
            { path: 'messages', component: MessagesComponent},
            { path: 'lists', component: ListsComponent},
        ]
    },
    { path: '**', redirectTo: 'home', pathMatch: 'full'},
];
