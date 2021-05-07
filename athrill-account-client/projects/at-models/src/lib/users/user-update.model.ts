import { FormDataBaseUpdateModel } from '../at-form-data.model';

export class UserUpdateModel extends FormDataBaseUpdateModel {
    public UserId: number;
    public FirstName: string;
    public MiddleName: string;
    public LastName: string;
    public Email: string;
    public DOB: Date;
    public GenderId: number;
    public PhoneNumber: string;
    public Address: string;
    public ImageFile: File;
    public IsImageChanged: boolean;
}