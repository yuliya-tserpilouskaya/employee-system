import { IEmployee } from '../models/IEmployee';
import {EmployeeUrl} from "../constants/UrlsConstants";

const getEmployees = async (): Promise<IEmployee[]> => {
    const response = await fetch(EmployeeUrl);
    if (!response.ok) throw new Error('Failed to fetch employees');
    return response.json();
};

const createEmployee = async (employee: Omit<IEmployee, 'id'>): Promise<void> => {
    const response = await fetch(EmployeeUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(employee),
    });

    if (!response.ok) {
        const errorData = await response.json();
        throw new Error(`Failed to create employee: ${JSON.stringify(errorData)}`);
    }
};

const updateEmployee = async (employee: IEmployee): Promise<void> => {
    const response = await fetch(`${EmployeeUrl}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(employee),
    });

    if (!response.ok) {
        const errorData = await response.json();
        throw new Error(`Failed to update employee: ${JSON.stringify(errorData)}`);
    }
};

const deleteEmployees = async (ids: string[]): Promise<void> => {
    const response = await fetch(EmployeeUrl, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(ids),
    });

    if (!response.ok) {
        const errorData = await response.json();
        throw new Error(`Failed to delete employees: ${JSON.stringify(errorData)}`);
    }
};

export { getEmployees, createEmployee, updateEmployee, deleteEmployees };
