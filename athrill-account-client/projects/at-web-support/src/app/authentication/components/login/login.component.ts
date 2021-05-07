import { Component, OnInit, ViewChild } from '@angular/core';
import { NgModel } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { LoginModel, ATBusinessExceptionModel, AuthenticationResponseModel } from 'at-models';
import { AuthenticationService, SessionTokenService, UserContextService, UserPermissionService } from 'at-services';

@Component({
    selector: 'at-login',
    templateUrl: './login.component.html',
    styleUrls: ['./../../authentication.component.scss']
})
export class LoginComponent implements OnInit {

    @ViewChild("username", { static: true }) username: NgModel;
    @ViewChild("password", { static: true }) password: NgModel;

    public loginModel: LoginModel = new LoginModel();
    public isSigningIn: boolean = false;

    public hasError: boolean = false;
    public validationMessages: string[];

    constructor(
        private _router: Router,
        private _route: ActivatedRoute,
        private _authenticationService: AuthenticationService,
        private _sessionTokenService: SessionTokenService,
        private _userContextService: UserContextService,
        private _userPermissionService: UserPermissionService,
    ) { }

    ngOnInit(): void {
        this.username.valueChanges.subscribe(value => this._resetForm());
        this.password.valueChanges.subscribe(value => this._resetForm());
    }

    public signIn(): void {
        this.isSigningIn = true;
        this._authenticationService.signIn(this.loginModel).subscribe({
            next: (authResponse: AuthenticationResponseModel) => {
                this._resetForm();
                this._sessionTokenService.setSession(authResponse.Token, authResponse.TokenExpireTTL); // store session token with expires on
                this._userContextService.store(authResponse.User); // store user information is user context.
                this._userPermissionService.store(authResponse.Permissions); // store user Permissions.
                
                if (authResponse.User?.UserLogin.IsResetPassword) {
                    // Do not change isChangePassword: We use this for opening dialog directly in profile general component
                    this._router.navigate(['/profile/general'], { state: { isChangePassword: true } });
                } else if (this._route.snapshot.queryParams["returnUrl"]) {
                    this._router.navigate([this._route.snapshot.queryParams["returnUrl"]]);
                } else {
                    this._router.navigate(['/']);
                }
            },
            error: (error: ATBusinessExceptionModel) => {
                this.isSigningIn = false;
                this.hasError = true;
                this.validationMessages = [];
                error.Validations?.forEach(err => {
                    this.validationMessages.push(err.Message);
                })
            }
        });
    }

    public signOut(): void {
        this._sessionTokenService.invalidateSession();
        this._router.navigate(['/auth']);
    }

    private _resetForm() {
        this.isSigningIn = false;
        this.hasError = false;
        this.validationMessages = [];
    }

}
