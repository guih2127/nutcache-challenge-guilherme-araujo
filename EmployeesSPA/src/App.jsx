import { useEffect, useState } from 'react'
import client from './api/employees.api';
import EmployeesTable from './components/employees.table';

function App() {
  const [employees, setEmployees] = useState();

  useEffect(() => {
    client.get().then(response => {
      setEmployees(response.data);
    }); // TODO - MOVER METODO PARA ARQUIVO DE SERVICE
  }, [])

  if (employees) {
    return (
      <EmployeesTable employees={employees} />
    );
  }
};

export default App
