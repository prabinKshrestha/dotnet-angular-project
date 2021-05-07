import { BaseModel } from '../at-base.model';
import { ContentModel } from './content.model';



export class ContentTypeModel extends BaseModel 
{
    public ContentTypeId: number;
    public NameKey: string;
    public DisplayName: string;
    public Description: string;
    public IsActive: boolean;

    public Id: number;

    public Contents : ContentModel[];
}