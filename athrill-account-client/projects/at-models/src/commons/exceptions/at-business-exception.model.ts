import { ATErrorLevel } from '../../constants/at-enums.enum';

export class ATBusinessExceptionModel
{
    public Validations : ATBusinessExceptionMessageModel[];
    public  Message : string;
}

export class ATBusinessExceptionMessageModel 
{
    public ErrorLevel : ATErrorLevel;
    public  Message : string;
    public  TargetType : string;
}