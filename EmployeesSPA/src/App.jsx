import { useEffect, useState } from 'react'
import EmployeesTable from './components/employees.table';
import employeesService from './services/employees.service';

function App() {
  const [employees, setEmployees] = useState();

  useEffect(() => {
    retrieveEmployees();
  }, []);

  const retrieveEmployees = async () => {
    await employeesService.getEmployees().then(result => {
      setEmployees(result.data);
    });
  };

  if (employees) {
    return (
      <EmployeesTable employees={employees} />
    );
  }
};

export default App
