import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AngularEditorModule } from '@kolkov/angular-editor';

import { ATAssetsSharedModule } from '../../at-assets-shared.module';
import { ItemsModule } from '../items/items.module';

import { ATFormComponent } from './form/at-form.component';
import { ATInputTextComponent } from './inputs/at-input-text/at-input-text.component';
import { ATInputNumberComponent } from './inputs/at-input-number/at-input-number.component';
import { ATInputCheckboxComponent } from './inputs/at-input-checkbox/at-input-checkbox.component';
import { ATInputDatePickerComponent } from './inputs/at-input-datepicker/at-input-datepicker.component';
import { ATInputSelectComponent } from './inputs/at-input-select/at-input-select.component';
import { ATInputTextAreaComponent } from './inputs/at-input-textarea/at-input-textarea.component';
import { ATInputErrorComponent } from './inputs/at-input-error/at-input-error.component';
import { ATInputFileComponent } from './inputs/at-input-file/at-input-file.component';
import { ATFormNotificationComponent } from './notification/at-form-notification.component';
import { ATInputWYSIWYGComponent } from './inputs/at-input-wysiwyg/at-input-wysiwyg.component';
import { ATInputMultiSelectComponent } from './inputs/at-input-multi-select/at-input-multi-select.component';

const ComponentCollection = [
    ATFormComponent,
    ATInputTextComponent,
    ATInputNumberComponent,
    ATInputCheckboxComponent,
    ATInputDatePickerComponent,
    ATInputSelectComponent,
    ATInputTextAreaComponent,
    ATInputFileComponent,
    ATInputErrorComponent,
    ATFormNotificationComponent,
    ATInputWYSIWYGComponent,
    ATInputMultiSelectComponent
];

@NgModule({
    declarations: [
        ...ComponentCollection
    ],
    imports: [
        CommonModule,
        FormsModule,
        ATAssetsSharedModule,
        ItemsModule,
        AngularEditorModule
    ],
    exports: [
        ...ComponentCollection
    ]
})
export class ATFormsModule { }
