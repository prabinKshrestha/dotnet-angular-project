import { BaseUpdateModel } from "../at-base.model";

export class UserEditModel extends BaseUpdateModel {
    public UserId : number;
    public IsActive :  boolean;
    public UserRoleId :  number;
}
