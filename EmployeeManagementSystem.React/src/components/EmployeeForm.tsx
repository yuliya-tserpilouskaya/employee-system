import React, { useEffect, useState } from "react";
import { IEmployee } from "../models/IEmployee";
import { createEmployee, updateEmployee } from "../services/EmployeeService";
import { Sex } from "../constants/constants";

interface EmployeeFormProps {
    existingEmployee?: IEmployee;
    onSubmit: () => void;
}

const EmployeeForm: React.FC<EmployeeFormProps> = ({ existingEmployee, onSubmit }) => {
    const [firstName, setFirstName] = useState<string>("");
    const [lastName, setLastName] = useState<string>("");
    const [age, setAge] = useState<number>(18); // Default age to 18
    const [sex, setSex] = useState<Sex>(Sex.PreferNotToSay); // Default to "Prefer not to say"
    const [error, setError] = useState<string>("");

    useEffect(() => {
        if (existingEmployee) {
            setFirstName(existingEmployee.firstName);
            setLastName(existingEmployee.lastName);
            setAge(existingEmployee.age);
            setSex(existingEmployee.sex); // Retain the existing employee's gender
        } else {
            setFirstName("");
            setLastName("");
            setAge(18); // Reset to default age when there is no existing employee
            setSex(Sex.PreferNotToSay); // Reset to "Prefer not to say"
        }
    }, [existingEmployee]);

    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();
        setError(""); // Clear previous errors

        const employee: IEmployee = {
            firstName,
            lastName,
            age,
            sex, // Use the selected sex value
        };

        try {
            if (existingEmployee) {
                await updateEmployee({ ...employee, id: existingEmployee.id });
            } else {
                await createEmployee(employee); // ID is not needed for creation
            }
            onSubmit(); // Refresh the employee list
        } catch (error: unknown) { // Specify the type as unknown
            if (error instanceof Error) {
                setError(error.message); // Set error message for user feedback
            } else {
                setError("An unexpected error occurred."); // Fallback for unexpected errors
            }
            console.error(error);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>{existingEmployee ? "Edit Employee" : "Add Employee"}</h2>
            {error && <div className="error">{error}</div>}
            <input
                type="text"
                placeholder="First Name"
                value={firstName}
                onChange={(e) => setFirstName(e.target.value)}
                required
            />
            <input
                type="text"
                placeholder="Last Name"
                value={lastName}
                onChange={(e) => setLastName(e.target.value)}
                required
            />
            <input
                type="number"
                placeholder="Age"
                value={age}
                onChange={(e) => setAge(Number(e.target.value))}
                min={18}
                max={100}
                required
            />
            <select value={sex} onChange={(e) => setSex(e.target.value as Sex)}>
                <option value={Sex.Male}>Male</option>
                <option value={Sex.Female}>Female</option>
                <option value={Sex.Other}>Other</option>
                <option value={Sex.PreferNotToSay}>Prefer not to say</option>
            </select>
            <button type="submit">{existingEmployee ? "Update Employee" : "Add Employee"}</button>
        </form>
    );
};

export default EmployeeForm;
