import { BaseModel, BaseUpdateModel, BaseAddModel } from '../at-base.model';

export class EmailSettingModel extends BaseModel 
{
    public  EmailSettingId : number;
    public  Name : string;
    public  Email : string;
    public  Password : string;
    public  SendFromName : string;
    public  ReplyToName : string;
    public  ReplyToEmail : string;
    public  Host : string;
    public  Port : string;
    public  IsSSL : boolean;
    public  IsDefault : boolean;
    public  Description : string;
    public  IsPublished : boolean;
    public  Id : number;

}

export class EmailSettingAddModel extends BaseAddModel 
{
    public  Name : string;
    public  Email : string;
    public  Password : string;
    public  SendFromName : string;
    public  ReplyToName : string;
    public  ReplyToEmail : string;
    public  Host : string;
    public  Port : string;
    public  IsSSL : boolean;
    public  IsDefault : boolean;
    public  Description : string;
    public  IsPublished : boolean;
}

export class EmailSettingUpdateModel extends BaseUpdateModel 
{
    public  EmailSettingId : number;
    public  Name : string;
    public  Email : string;
    public  Password : string;
    public  SendFromName : string;
    public  ReplyToName : string;
    public  ReplyToEmail : string;
    public  Host : string;
    public  Port : string;
    public  IsSSL : boolean;
    public  IsDefault : boolean;
    public  Description : string;
    public  IsPublished : boolean;
}