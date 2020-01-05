import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { EmployeeEditComponent } from '../employees/employee-list/employee-edit/employee-edit.component';

@Injectable()
export class PreventUnsavedChanges implements CanDeactivate<EmployeeEditComponent> {
    canDeactivate(component: EmployeeEditComponent) {
        if (component.editForm.dirty) {
            return confirm('Are you sure you want to continue? Any unsaved changes will be lost');
        }
        return true;
    }
}
