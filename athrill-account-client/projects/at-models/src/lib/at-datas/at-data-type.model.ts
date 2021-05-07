
import { BaseModel } from '../at-base.model';
import { ATDataValueModel } from './at-data-value.model';

export class ATDataTypeModel extends BaseModel
{
    public ATDataTypeId : number;
    public NameKey : string;
    public DisplayName : string;
    public Description : string;
    public IsActive : boolean;

    public ATDataValues :  ATDataValueModel[];

    public Id : number;
}
