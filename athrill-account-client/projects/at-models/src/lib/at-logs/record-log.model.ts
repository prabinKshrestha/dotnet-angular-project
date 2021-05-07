import { BaseModel } from '../at-base.model';
import { ATEntityModel } from '../system-values/at-entity.model';

export class RecordLogModel extends BaseModel {
    public RecordLogId: number;
    public RecordType: number;
    public EntityId: string;
    public Record: string;
    public CreatedBy: number;
    public IPAddress: string;
    public ClientName: string;
    public Id: number;

    public Entity : ATEntityModel;
}