import { ContentTreeEntityModel } from './content-tree-entity.model';

export class ContentTreeModel
{
    public Title : string;
    public Node : ContentTreeEntityModel;
    public Children : ContentTreeModel[];
} 

