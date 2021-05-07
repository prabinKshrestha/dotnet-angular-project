import { UserRoleModel } from './user-role.model';
import { UserModel } from './user.model';

export class UserRoleLinkModel{
    
    public UserRoleLinkId : number;
    public UserId : number;
    public UserRoleId: number; 

    public User: UserModel; 
    public UserRole: UserRoleModel; 
}