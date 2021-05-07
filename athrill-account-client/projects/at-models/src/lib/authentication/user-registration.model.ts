import { FormDataBaseAddModel } from '../at-form-data.model';


export class UserRegistrationModel extends FormDataBaseAddModel {
    public FirstName : string;
    public MiddleName : string;
    public LastName : string;
    public Email : string;
    public PhoneNumber : string;
    public Address : string;
    public DOB : Date;
    public GenderId :  number;
    public IsActive :  boolean;
    public UserRoleId :  number;
    public Username :  number;
    public ImageFile : File;
}
