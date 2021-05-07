import { ChangeDetectorRef, Component, ContentChildren, EventEmitter, Input, Output, QueryList, ViewChild } from '@angular/core';
import { NgForm, NgModel } from '@angular/forms';
import { Observable } from 'rxjs';
import { ATBusinessExceptionModel } from 'at-models';
import { ATInputFileComponent } from '../inputs/at-input-file/at-input-file.component';
import { ATFormNotificationService } from '../notification/at-form-notification.service';
import { ATInputDatePickerComponent } from '../inputs/at-input-datepicker/at-input-datepicker.component';
import { ATInputWYSIWYGComponent } from '../inputs/at-input-wysiwyg/at-input-wysiwyg.component';
import { ATScrollerService } from '../../items/at-scroller/at-scroller.service';

@Component({
  selector: 'at-assets-form',
  templateUrl: './at-form.component.html',
  styleUrls: ['./at-form.component.scss']
})
export class ATFormComponent {

  @Input() subscription: Observable<any>;
  @Input() isValid: boolean = true;

  @Output() onProcess: EventEmitter<boolean> = new EventEmitter();
  @Output() onSubmission: EventEmitter<boolean> = new EventEmitter();
  @Output() onSuccess: EventEmitter<any> = new EventEmitter();
  @Output() onFailure: EventEmitter<any> = new EventEmitter();
  @Output() onCancel: EventEmitter<boolean> = new EventEmitter();

  @ViewChild("atForm", { static: true }) form: NgForm;
  @ContentChildren(NgModel, { descendants: true }) models: QueryList<NgModel>;
  @ContentChildren(ATInputFileComponent, { descendants: true }) fileModels: QueryList<ATInputFileComponent>;
  @ContentChildren(ATInputDatePickerComponent, { descendants: true }) dateModels: QueryList<ATInputDatePickerComponent>;
  @ContentChildren(ATInputWYSIWYGComponent, { descendants: true }) wysiwygModels: QueryList<ATInputDatePickerComponent>;

  constructor(
    private _cdf: ChangeDetectorRef,
    public atFormNotificationService: ATFormNotificationService,
    public atScrollerService: ATScrollerService
  ) { }

  ngAfterViewChecked() {
    this._cdf.detectChanges();
  }

  ngAfterContentInit() {
    this.models.toArray().forEach(model => {
      model.valueChanges.subscribe(value => {
        this.atFormNotificationService.clearErrors();
      });
    });
    this.fileModels.toArray().forEach(model => {
      model.valueChanges.subscribe(value => {
        this.atFormNotificationService.clearErrors();
      });
    });
    this.dateModels.toArray().forEach(model => {
      model.valueChanges.subscribe(value => {
        this.atFormNotificationService.clearErrors();
      });
    });
    this.wysiwygModels.toArray().forEach(model => {
      model.valueChanges.subscribe(value => {
        this.atFormNotificationService.clearErrors();
      });
    });
  }

  onSaveHandler() {
    this.onSubmission.emit(true);
    this.atFormNotificationService.clearErrors();
    if (this.isValid && this._isFormValid()) {
      if (this.subscription) {
        this.onProcess.emit(true);
        this.subscription.subscribe({
          next: (res: any) => {
            this.resetForm();
            this.onSuccess.emit(res);
            this.onProcess.emit(false);
          },
          error: (error: ATBusinessExceptionModel) => {
            this.atFormNotificationService.showErrors(error);
            this.atScrollerService.scrollToClassName('form-footer-notification');
            this.onFailure.emit(error);
            this.onProcess.emit(false);
          }
        });
      }
    }
    else {
      this.atScrollerService.scrollToClassName('ng-invalid');
    }
  }

  onCancelHandler() {
    this.onCancel.emit(true);
  }

  resetForm() {
    this.form.resetForm();
  }

  private _isFormValid(): boolean {
    return this.models.toArray().filter(x => x.invalid).length == 0
      && this.fileModels.toArray().filter(x => x.invalid).length == 0
      && this.dateModels.toArray().filter(x => x.invalid).length == 0
      && this.wysiwygModels.toArray().filter(x => x.invalid).length == 0;
  }

}