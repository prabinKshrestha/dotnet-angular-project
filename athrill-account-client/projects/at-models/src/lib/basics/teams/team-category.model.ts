import { BaseModel,BaseAddModel,BaseUpdateModel } from '../../at-base.model';
import { TeamCategoryMemberLinkModel } from './team-category-member-link.model';

export class TeamCategoryModel extends BaseModel{
    public TeamCategoryId: number;
    public Orientation: number;
    public Name: string;;
    public ShortDescription: string;
    public Description: string;
    public IsPublished: boolean;
    public Id: number;

    public TeamCategoryMemberLinks : TeamCategoryMemberLinkModel[];
}

export class TeamCategoryAddModel extends BaseAddModel{
    public Name: string;;
    public ShortDescription: string;
    public Description: string;
    public IsPublished: boolean;

    public TeamCategoryMemberLinks : TeamCategoryMemberLinkModel[];
}

export class TeamCategoryUpdateModel extends BaseUpdateModel{
    public TeamCategoryId: number;
    public Name: string;;
    public ShortDescription: string;
    public Description: string;
    public IsPublished: boolean;
}
