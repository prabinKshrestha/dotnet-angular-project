import {UserModel} from './../users/user.model';

export class AuthenticationResponseModel
{
    public Token : string;
    public TokenExpireTTL : Date;
    public IsResetPassword? : boolean;
    public UserRoleId : number;
    public User : UserModel;
    public Permissions : string[];
}