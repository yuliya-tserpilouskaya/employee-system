// src/models/IEmployee.ts
import { Sex } from "../constants/constants"; // Adjust the import path accordingly

export interface IEmployee {
    id?: string; // Optional property
    firstName: string;
    lastName: string;
    age: number;
    sex: Sex;
}
