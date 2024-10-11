import { SexEnum } from "../constants/SexEnum";

export interface IEmployee {
    id?: string;
    firstName: string;
    lastName: string;
    age: number;
    sex: SexEnum;
}
