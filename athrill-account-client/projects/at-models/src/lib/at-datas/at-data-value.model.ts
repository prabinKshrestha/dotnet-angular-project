
import { BaseModel } from '../at-base.model';
import { ATDataTypeModel } from './at-data-type.model';

export class ATDataValueModel extends BaseModel
{
    public ATDataValueId : number;
    public ATDataTypeId : number;
    public DisplayName : string;
    public Value : string;
    public Description : string;
    public IsActive : boolean;

    public ATDataType :  ATDataTypeModel;

    public Id : number;
}