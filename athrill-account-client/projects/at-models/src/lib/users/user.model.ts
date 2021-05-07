import { BaseModel } from '../at-base.model';
import { UserLoginModel } from './user-login.model';
import { ATDataValueModel } from '../at-datas/at-data-value.model';
import { UserRoleLinkModel } from './user-role-link.model';


export class UserModel extends BaseModel
{
    public UserId : number;
    public FirstName : string;
    public MiddleName : string;
    public LastName : string;
    public ImageName : string;
    public Email : string;
    public DOB : Date;
    public GenderId : number;
    public PhoneNumber : string;
    public Address : string;
    public IsActive : boolean;
    public Image: string;

    public Id : number;
    public FullName : string;

    public UserLogin : UserLoginModel;
    public UserRoleLink : UserRoleLinkModel;
    public Gender : ATDataValueModel;
}
