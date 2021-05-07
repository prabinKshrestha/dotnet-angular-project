import { UserModel } from './user.model';
import { UserTrackType } from '../../constants/at-enums.enum';

export class UserTrackRecordModel
{
    public UserTrackRecordId : number;
    public UserId : number;
    public UserTrackTypeId : number;
    public IpAddress : string;
    public ClientName : string;
    public UserTrackTypeDisplayName : string;
    public CreatedById :number;
    public CreatedOn : Date;

    public User: UserModel;
    public UserTrackType : UserTrackType;
}