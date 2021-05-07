import { BaseModel } from '../../at-base.model';
import { TeamCategoryModel } from './team-category.model';
import { TeamMemberModel } from './team-member.model';

export class TeamCategoryMemberLinkModel extends BaseModel{
    public TeamCategoryMemberLinkId: number;
    public TeamCategoryId: number;
    public TeamMemberId: number;
    public TeamMemberOrientation: number;

    public Id: number;

    public TeamCategory: TeamCategoryModel;
    public TeamMember: TeamMemberModel;

}
