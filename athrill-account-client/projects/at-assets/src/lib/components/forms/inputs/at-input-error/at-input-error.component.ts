
import { Component } from '@angular/core';

@Component({
  selector: 'at-assets-input-error',
  template: `
    <mat-error class="at-input-error">
      <ng-content></ng-content>
    </mat-error>`,
  styleUrls: ['./../at-input.component.scss']
})

export class ATInputErrorComponent { }