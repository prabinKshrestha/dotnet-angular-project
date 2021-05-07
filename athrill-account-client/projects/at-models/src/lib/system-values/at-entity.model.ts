import { BaseModel } from '../at-base.model';


export class ATEntityModel extends BaseModel
{
    public  EntityId : number;
    public  NameKey : string;
    public  DisplayName : string;
    public  Description : string;
    public  IsShownInRecordLogForAdminPanel : boolean;
    public  Id : number;
}