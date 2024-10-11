import { IEmployee } from '../models/IEmployee';

const API_URL = "https://localhost:7285/api/Employee";

// Function to get all employees
const getEmployees = async (): Promise<IEmployee[]> => {
    const response = await fetch(API_URL);
    if (!response.ok) throw new Error('Failed to fetch employees');
    return response.json();
};

// Function to create a new employee
const createEmployee = async (employee: Omit<IEmployee, 'id'>): Promise<void> => {
    console.log(employee)
    const response = await fetch(API_URL, {
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

// Function to update an existing employee
const updateEmployee = async (employee: IEmployee): Promise<void> => {
    const response = await fetch(`${API_URL}`, {
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

// Function to delete employees
const deleteEmployees = async (ids: string[]): Promise<void> => {
    const response = await fetch(API_URL, {
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

// Export functions as named exports
export { getEmployees, createEmployee, updateEmployee, deleteEmployees };
