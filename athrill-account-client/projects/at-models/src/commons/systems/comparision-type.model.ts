
export enum ComparisionTypes {
    IsEqualTo = 1,
    IsNotEqualTo,
    IsLessThan,
    IsLessThanOrEqualTo,
    IsGreaterThan,
    IsGreaterThanOrEqualTo,
    Contains,
    NotContains,
    IsNull,
    IsNotNull,
    StartsWith,
    NotStartsWith,
    EndsWith,
    NotEndsWith
}

export class ComparisionTypeModel
{
    ComparisionTypeId : ComparisionTypes;
    DisplayName : string;
}

export const TotalComparisionTypeCollection : ComparisionTypeModel[] = [
    { ComparisionTypeId : ComparisionTypes.IsEqualTo, DisplayName : "Is Equal to" },
    { ComparisionTypeId : ComparisionTypes.IsNotEqualTo, DisplayName : "Is not Equal to" },
    { ComparisionTypeId : ComparisionTypes.IsLessThan, DisplayName : "Is Less Than" },
    { ComparisionTypeId : ComparisionTypes.IsLessThanOrEqualTo, DisplayName : "Is Less Than or Equal To" },
    { ComparisionTypeId : ComparisionTypes.IsGreaterThan, DisplayName : "Is Greater Than" },
    { ComparisionTypeId : ComparisionTypes.IsGreaterThanOrEqualTo, DisplayName : "Is Greater Than or Equal To" },
    { ComparisionTypeId : ComparisionTypes.Contains, DisplayName : "Contains" },
    { ComparisionTypeId : ComparisionTypes.NotContains, DisplayName : "Does not Contain" },
    { ComparisionTypeId : ComparisionTypes.IsNull, DisplayName : "Is Null" },
    { ComparisionTypeId : ComparisionTypes.IsNotNull, DisplayName : "Is not Null" },
    { ComparisionTypeId : ComparisionTypes.StartsWith, DisplayName : "Starts With" },
    { ComparisionTypeId : ComparisionTypes.NotStartsWith, DisplayName : "Does not Start With" },
    { ComparisionTypeId : ComparisionTypes.EndsWith, DisplayName : "Ends With" },
    { ComparisionTypeId : ComparisionTypes.NotEndsWith, DisplayName : "Does not End With" },
]

export const BOOLEAN_COMPARISION_TYPE_COLLECTION: ComparisionTypes[] = [
    ComparisionTypes.IsEqualTo,
    ComparisionTypes.IsNotEqualTo,
    ComparisionTypes.IsNull,
    ComparisionTypes.IsNotNull
];

export const INTEGER_COMPARISION_TYPE_COLLECTION: ComparisionTypes[] = [
    ComparisionTypes.IsEqualTo,
    ComparisionTypes.IsNotEqualTo,
    ComparisionTypes.IsLessThan,
    ComparisionTypes.IsLessThanOrEqualTo,
    ComparisionTypes.IsGreaterThan,
    ComparisionTypes.IsGreaterThanOrEqualTo,
    ComparisionTypes.IsNull,
    ComparisionTypes.IsNotNull
];

export const STRING_COMPARISION_TYPE_COLLECTION: ComparisionTypes[] = [
    ComparisionTypes.IsEqualTo,
    ComparisionTypes.IsNotEqualTo,
    ComparisionTypes.IsLessThan,
    ComparisionTypes.IsLessThanOrEqualTo,
    ComparisionTypes.IsGreaterThan,
    ComparisionTypes.IsGreaterThanOrEqualTo,
    ComparisionTypes.Contains,
    ComparisionTypes.NotContains,
    ComparisionTypes.IsNull,
    ComparisionTypes.IsNotNull,
    ComparisionTypes.StartsWith,
    ComparisionTypes.NotStartsWith,
    ComparisionTypes.EndsWith,
    ComparisionTypes.NotEndsWith,
];

export const DATE_COMPARISION_TYPE_COLLECTION: ComparisionTypes[] = [
    ComparisionTypes.IsEqualTo,
    ComparisionTypes.IsNotEqualTo,
    ComparisionTypes.IsLessThan,
    ComparisionTypes.IsLessThanOrEqualTo,
    ComparisionTypes.IsGreaterThan,
    ComparisionTypes.IsGreaterThanOrEqualTo,
    ComparisionTypes.IsNull,
    ComparisionTypes.IsNotNull
];

export const DROPDOWN_COMPARISION_TYPE_COLLECTION: ComparisionTypes[] = [
    ComparisionTypes.IsEqualTo,
    ComparisionTypes.IsNotEqualTo,
    ComparisionTypes.IsNull,
    ComparisionTypes.IsNotNull
];