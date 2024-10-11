import React, {useEffect, useState} from "react";
import {IEmployee} from "../models/IEmployee";
import {createEmployee, updateEmployee} from "../services/EmployeeService";
import {SexEnum} from "../constants/SexEnum";
import {AgeConstants} from "../constants/AgeConstants";

interface EmployeeFormProps {
    existingEmployee?: IEmployee;
    onSubmit: () => void;
}

const EmployeeForm: React.FC<EmployeeFormProps> = ({existingEmployee, onSubmit}) => {
    const [firstName, setFirstName] = useState<string>("");
    const [lastName, setLastName] = useState<string>("");
    const [age, setAge] = useState<number>(AgeConstants.MinAge);
    const [sex, setSex] = useState<SexEnum>(SexEnum.PreferNotToSay);
    const [error, setError] = useState<string>("");

    useEffect(() => {
        if (existingEmployee) {
            setFirstName(existingEmployee.firstName);
            setLastName(existingEmployee.lastName);
            setAge(existingEmployee.age);
            setSex(existingEmployee.sex);
        } else {
            setFirstName("");
            setLastName("");
            setAge(AgeConstants.MinAge);
            setSex(SexEnum.PreferNotToSay);
        }
    }, [existingEmployee]);

    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();
        setError("");

        const employee: IEmployee = {
            firstName,
            lastName,
            age,
            sex,
        };

        try {
            if (existingEmployee) {
                await updateEmployee({...employee, id: existingEmployee.id});
            } else {
                await createEmployee(employee);
            }
            onSubmit();
        } catch (error: unknown) {
            if (error instanceof Error) {
                setError(error.message);
            } else {
                setError("An unexpected error occurred.");
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
                min={AgeConstants.MinAge}
                max={AgeConstants.MaxAge}
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
