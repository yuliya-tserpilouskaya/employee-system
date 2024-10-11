import React, { useState } from 'react';
import { IEmployee } from '../models/IEmployee';
import { SexNames } from '../constants/SexEnum';

interface EmployeeListProps {
    employees: IEmployee[];
    onDelete: (ids: string[]) => Promise<void>;
    onEdit: (employee: IEmployee) => void;
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
            setSelectedIds([]);
        }
    };

    return (
        <div>
            <div className="employee-list-header">
                <h2 className="employee-list-title">Employee List</h2>
                {selectedIds.length > 0 && (
                    <button
                        onClick={handleDelete}
                        className="red-button"
                    >
                        Remove Selected
                    </button>
                )}
            </div>
            {employees.length === 0 ? (
                <p>No employees available.</p>
            ) : (
                <div className="employee-list-container">
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
                            <tr key={employee.id} className={index % 2 === 0 ? 'table-row-even' : 'table-row-odd'}>
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
                </div>
            )}
        </div>
    );
};

export default EmployeeList;
