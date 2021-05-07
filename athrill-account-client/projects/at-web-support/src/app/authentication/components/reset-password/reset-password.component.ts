import { Component, OnInit, ViewChild } from '@angular/core';
import { NgModel } from '@angular/forms';
import { ResetPasswordModel, ATBusinessExceptionModel } from 'at-models';
import { AuthenticationService } from 'at-services';
import { ATGlobalStore } from '../../../shared/services/at-global.store';

@Component({
  selector: 'at-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./../../authentication.component.scss']
})
export class ResetPasswordComponent implements OnInit {

  @ViewChild("username", { static: true }) username: NgModel;
  @ViewChild("email", { static: true }) email: NgModel;

  public resetPasswordModel: ResetPasswordModel = new ResetPasswordModel;
  public isSubmitting: boolean = false;

  public hasError: boolean = false;
  public validationMessages: string[];
  public showSuccessMessage : boolean = false;

  constructor(
    public atGlobalStore: ATGlobalStore,
    private _authenticationService: AuthenticationService
  ) { }


  ngOnInit(): void {
    this.username.valueChanges.subscribe(value => this._resetForm());
    this.email.valueChanges.subscribe(value => this._resetForm());
  }

  public resetPassword(): void {
    this.isSubmitting = true;
    this._authenticationService.resetPassword(this.resetPasswordModel).subscribe({
      next: () => {
        this.showSuccessMessage = true;
        this._resetForm();
      },
      error: (error: ATBusinessExceptionModel) => {
        this.isSubmitting = false;
        this.hasError = true;
        this.showSuccessMessage = false;
        this.validationMessages = [];
        error.Validations?.forEach(err => {
          this.validationMessages.push(err.Message);
        })
      }
    });
  }

  private _resetForm() {
    this.isSubmitting = false;
    this.hasError = false;
    this.validationMessages = [];
  }
}
