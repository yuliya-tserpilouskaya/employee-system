export enum SexEnum {
    Male = 1,
    Female = 2,
    Other = 3,
    PreferNotToSay = 4
}

export const SexNames = new Map<SexEnum, string>([
    [SexEnum.Male, 'Male'],
    [SexEnum.Female, 'Female'],
    [SexEnum.Other, 'Other'],
    [SexEnum.PreferNotToSay, 'Prefer not to say'],
]);
