import React, { useState } from 'react';
import { IEmployee } from '../models/IEmployee';
import {SexNames} from '../constants/SexEnum'; // Import the Sex enum

interface EmployeeListProps {
    employees: IEmployee[];
    onDelete: (ids: string[]) => Promise<void>;
    onEdit: (employee: IEmployee) => void; // Accept an edit function
}

const EmployeeList: React.FC<EmployeeListProps> = ({ employees, onDelete, onEdit }) => {
    const [selectedIds, setSelectedIds] = useState<string[]>([]);

    const handleSelect = (id: string) => {
        if (selectedIds.includes(id)) {
            setSelectedIds(selectedIds.filter(selectedId => selectedId !== id));
        } else {
            setSelectedIds([...selectedIds, id]);
        }
    };

    const handleDelete = async () => {
        if (window.confirm("Are you sure you want to delete the selected employees?")) {
            await onDelete(selectedIds);
            setSelectedIds([]); // Clear selected IDs after deletion
        }
    };

    return (
        <div>
            <h2>Employee List</h2>
            {employees.length === 0 ? (
                <p>No employees available.</p>
            ) : (
                <div>
                    <table>
                        <thead>
                        <tr>
                            <th>Select</th>
                            <th>Name</th>
                            <th>Age</th>
                            <th>Gender</th>
                            <th>Actions</th>
                        </tr>
                        </thead>
                        <tbody>
                        {employees.map((employee, index) => (
                            <tr key={employee.id} style={{ backgroundColor: index % 2 === 0 ? '#f9f9f9' : '#fff' }}>
                                <td>
                                    <input
                                        type="checkbox"
                                        checked={selectedIds.includes(employee.id || '')}
                                        onChange={() => handleSelect(employee.id || '')}
                                    />
                                </td>
                                <td>{`${employee.firstName} ${employee.lastName}`}</td>
                                <td>{`${employee.age} years`}</td>
                                <td>{SexNames.get(employee.sex)}</td>
                                <td>
                                    <button onClick={() => onEdit(employee)}>Edit</button>
                                </td>
                            </tr>
                        ))}
                        </tbody>
                    </table>
                    {selectedIds.length > 0 && (
                        <button onClick={handleDelete}>Remove Selected</button>
                    )}
                </div>
            )}
        </div>
    );
};

export default EmployeeList;
