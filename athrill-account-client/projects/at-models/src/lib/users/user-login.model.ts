import { BaseModel } from '../at-base.model';
import { UserModel } from './user.model';

export class UserLoginModel extends BaseModel
{
    public UserLoginId : number;
    public UserId : number;
    public Username : string;
    public IsResetPassword? : boolean;

    public User : UserModel;

    public Id : number;
}
