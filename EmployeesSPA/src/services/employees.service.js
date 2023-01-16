import client from '../api/employees.api';

const getEmployees = async () => {
    return await client.get();
};

const insertEmployee = async (employee) => {
    return await client.post('/' ,employee);
};

const updateEmployee = async (employee) => {
    return await client.put(`/${employee.id.toString()}`, employee);
};

const deleteEmployee = async(employee) => {
    return await client.delete(`/${employee.id.toString()}`);
}

const employeesService = {
    getEmployees,
    insertEmployee,
    updateEmployee,
    deleteEmployee
};

export default employeesService;