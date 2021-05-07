import { BaseModel } from '../../at-base.model';
import { FormDataBaseAddModel,FormDataBaseFormModel,FormDataBaseUpdateModel } from '../../at-form-data.model';
import { TeamCategoryMemberLinkModel } from './team-category-member-link.model';

export class TeamMemberModel extends BaseModel{
    public TeamMemberId: number;
    public Name: string;
    public Role: string;
    public Position: string;
    public Quotation: string;
    public ShortDescription: string;
    public Description: string;
    public Email: string;
    public ImageName: string;
    public PhoneNumber: string;
    public FacebookLink: string;
    public InstagramLink: string;
    public SkypeLink: string;
    public LinkedInLink: string;
    public Twiterlink: string;
    public Viber: string;
    public IsPublished: boolean;
    public Id: number;
    public Image: string;

    public TeamCategoryMemberLinks : TeamCategoryMemberLinkModel[];
}

export class TeamMemberAddModel extends FormDataBaseAddModel{
    public Name: string;
    public Role: string;
    public Position: string;
    public Quotation: string;
    public ShortDescription: string;
    public Description: string;
    public Email: string;
    public PhoneNumber: string;
    public FacebookLink: string;
    public InstagramLink: string;
    public SkypeLink: string;
    public LinkedInLink: string;
    public Twiterlink: string;
    public Viber: string;
    public IsPublished: boolean;
    public ImageFile: File;
    public TeamCategoryIds: number[];
}

export class TeamMemberUpdateModel extends FormDataBaseUpdateModel{
    public TeamMemberId: number;
    public Name: string;
    public Role: string;
    public Position: string;
    public Quotation: string;
    public ShortDescription: string;
    public Description: string;
    public Email: string;
    public PhoneNumber: string;
    public FacebookLink: string;
    public InstagramLink: string;
    public SkypeLink: string;
    public LinkedInLink: string;
    public Twiterlink: string;
    public Viber: string;
    public IsPublished: boolean;
    public ImageFile: File;
    public TeamCategoryIds: number[];
    public IsImageChanged: boolean;
}

export class TeamMemberFormModel extends FormDataBaseFormModel {
    public TeamMemberId: number;
    public Name: string;
    public Role: string;
    public Position: string;
    public Quotation: string;
    public ShortDescription: string;
    public Description: string;
    public Email: string;
    public PhoneNumber: string;
    public FacebookLink: string;
    public InstagramLink: string;
    public SkypeLink: string;
    public LinkedInLink: string;
    public Twiterlink: string;
    public Viber: string;
    public IsPublished: boolean;
    public ImageFile: File;
    public TeamCategoryIds: number[];

    public IsImageChanged: boolean;
}

export class TeamMemberOrientationUpdateModel{
    public TeamCategoryId : number;
    public TeamMemberId: number;
}
