import { UserRoleLinkModel } from './user-role-link.model';


export class UserRoleModel{
    
    public UserRoleId : number;
    public NameKey : string;
    public DisplayName: string; 
    public Description : string;
    public IsShown : boolean;
        
    public UserRoleLinks : UserRoleLinkModel[];
}