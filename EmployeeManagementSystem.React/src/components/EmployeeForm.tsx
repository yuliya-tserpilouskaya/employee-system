import React, {useEffect, useState} from "react";
import {IEmployee} from "../models/IEmployee";
import {createEmployee, updateEmployee} from "../services/EmployeeService";
import {SexEnum} from "../constants/SexEnum";

interface EmployeeFormProps {
    existingEmployee?: IEmployee;
    onSubmit: () => void;
}

const EmployeeForm: React.FC<EmployeeFormProps> = ({existingEmployee, onSubmit}) => {
    const [firstName, setFirstName] = useState<string>("");
    const [lastName, setLastName] = useState<string>("");
    const [age, setAge] = useState<number>(18); // Default age to 18
    const [sex, setSex] = useState<SexEnum>(SexEnum.PreferNotToSay); // Default to "Prefer not to say"
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
            setSex(SexEnum.PreferNotToSay); // Reset to "Prefer not to say"
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
                await updateEmployee({...employee, id: existingEmployee.id});
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
                onChange={(e) => {
                    const value = e.target.value;
                    if (value) {
                        setFirstName(value)
                    }
                }}
                required
            />
            <input
                type="text"
                placeholder="Last Name"
                value={lastName}
                onChange={(e) => {
                    const value = e.target.value;
                    if (value) {
                        setLastName(value)
                    }
                }}
                required
            />
            <input
                type="number"
                placeholder="Age"
                value={age}
                onChange={(e) => {
                    const value = e.target.value;
                    if (value) {
                        setAge(Number(value))
                    }
                }}
                min={18}
                max={100}
                required
            />
            <select value={sex} onChange={(e) => {
                const value = e.target.value;
                if (value) {
                    setSex(Number(value))
                }
            }}>
                <option value={SexEnum.Male}>Male</option>
                <option value={SexEnum.Female}>Female</option>
                <option value={SexEnum.Other}>Other</option>
                <option value={SexEnum.PreferNotToSay}>Prefer not to say</option>
            </select>
            <button type="submit">{existingEmployee ? "Update Employee" : "Add Employee"}</button>
        </form>
    );
};

export default EmployeeForm;
