import client from '../api/employees.api';

const getEmployees = async () => {
    return await client.get();
};

const employeesService = {
    getEmployees
};

export default employeesService;