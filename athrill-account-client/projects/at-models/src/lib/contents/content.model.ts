import { BaseModel } from '../at-base.model';
import { FormDataBaseAddModel,FormDataBaseFormModel,FormDataBaseUpdateModel } from '../at-form-data.model';
import { ATDataValueModel } from '../at-datas/at-data-value.model';
import { ContentTypeModel } from './content-type.model';

export class ContentModel extends BaseModel {
    public ContentId: number;
    public ContentTypeId: number;
    public ParentId: number;
    public PlacementId: number;
    public Position: number;
    public Slug: string;
    public Name: string;
    public SubName: string;
    public ImageName: string;
    public ImageAltName: string;
    public ShortDescription: string;
    public Description: string;
    public ExternalUrl: string;
    public FontAwesomeIcon: string;
    public IsPublished: boolean;
    public Id: number;
    public Image: string;

    public ContentType : ContentTypeModel;
    public Parent : ContentModel;
    public Placement : ATDataValueModel;
    public Childrens : ContentModel[];
}

export class ContentAddModel extends FormDataBaseAddModel {
    public ContentTypeId: number;
    public ParentId: number ;
    public PlacementId: number;
    public Slug: string;
    public Name: string;
    public SubName: string;
    public ImageAltName: string;
    public ShortDescription: string;
    public Description: string;
    public ExternalUrl: string;
    public FontAwesomeIcon: string;
    public IsPublished: boolean;    
    public ImageFile: File;
}
export class ContentUpdateModel extends FormDataBaseUpdateModel {
    public ContentId: number;
    public ContentTypeId: number;
    public ParentId: number;
    public PlacementId: number;
    public Slug: string;
    public Name: string;
    public SubName: string;
    public ImageAltName: string;
    public ShortDescription: string;
    public Description: string;
    public ExternalUrl: string;
    public FontAwesomeIcon: string;
    public IsPublished: boolean;    
    public ImageFile: File;

    public IsImageChanged: boolean;
}
export class ContentFormModel extends FormDataBaseFormModel {
    public ContentId: number;
    public ContentTypeId: number;
    public ParentId: number;
    public PlacementId: number;
    public Slug: string;
    public Name: string;
    public SubName: string;
    public ImageAltName: string;
    public ShortDescription: string;
    public Description: string;
    public ExternalUrl: string;
    public FontAwesomeIcon: string;
    public IsPublished: boolean;    
    public ImageFile: File;

    public IsImageChanged: boolean;
}