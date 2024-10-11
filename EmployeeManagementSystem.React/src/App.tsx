import React, { useEffect, useState } from "react";
import EmployeeForm from "./components/EmployeeForm";
import EmployeeList from "./components/EmployeeList";
import { IEmployee } from "./models/IEmployee";
import * as employeeService from "./services/EmployeeService";

const App: React.FC = () => {
    const [employees, setEmployees] = useState<IEmployee[]>([]);
    const [refresh, setRefresh] = useState<number>(0);
    const [editingEmployee, setEditingEmployee] = useState<IEmployee | undefined>(undefined); // Set to undefined

    useEffect(() => {
        const fetchEmployees = async () => {
            const fetchedEmployees = await employeeService.getEmployees();
            setEmployees(fetchedEmployees);
        };

        fetchEmployees();
    }, [refresh]);

    const refreshEmployeeList = () => {
        setRefresh(prev => prev + 1);
    };

    const handleDelete = async (ids: string[]) => {
        await employeeService.deleteEmployees(ids);
        refreshEmployeeList();
    };

    const handleEdit = (employee: IEmployee) => {
        setEditingEmployee(employee); // Set the employee to be edited
    };

    const handleSubmit = () => {
        setEditingEmployee(undefined); // Clear the editing state after submission
        refreshEmployeeList();
    };

    return (
        <div>
            <h1>Employee Management System</h1>
            <EmployeeForm existingEmployee={editingEmployee} onSubmit={handleSubmit} />
            <EmployeeList employees={employees} onDelete={handleDelete} onEdit={handleEdit} />
        </div>
    );
};

export default App;
