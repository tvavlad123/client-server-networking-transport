using log4net;
using System;
using System.Collections.Generic;
using Transport.Model;

namespace Transport.Repository
{
    public class EmployeeDBRepository : IRepository<int, Employee>
    {
        private static IValidator<Employee> _validator;

        private static readonly ILog Logger = LogManager.GetLogger("EmployeeDBRepository");

        private readonly IDictionary<string, string> _props;

        public EmployeeDBRepository(IDictionary<string, string> props)
        {
            Logger.Info("Creating EmployeeDBValidator");
            _validator = new EmployeeValidator();
            _props = props;
        }

        public void Delete(int id)
        {
            Logger.InfoFormat("Try to delete employee with id {0}", id);
            var connection = DBUtils.GetConnection(_props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM employees WHERE ID = @id";
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@id", (object)id)));
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Employee> FindAll()
        {
            Logger.Info("Try to find all");
            var connection = DBUtils.GetConnection(_props);
            var employees = new List<Employee>();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM employees";
                using (var read = command.ExecuteReader())
                {
                    while (read.Read())
                    {

                        var employee = new Employee
                        {
                            Id = read.GetInt32(0),
                            FirstName = read.GetString(1),
                            LastName = read.GetString(2),
                            UserName = read.GetString(3),
                            Password = read.GetString(4),
                            Office = read.GetString(5)
                        };
                        employees.Add(employee);
                    }
                }
            }
            return employees;
        }

        public Employee FindOne(int id)
        {
            Logger.InfoFormat("Try to find id {0}", id);
            var connection = DBUtils.GetConnection(_props);
            Employee employee = null;
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM employees WHERE ID = @id";
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@id", (object)id)));
                using (var read = command.ExecuteReader())
                {
                    if (read.Read())
                        employee = new Employee
                        {
                            Id = read.GetInt32(0),
                            FirstName = read.GetString(1),
                            LastName = read.GetString(2),
                            UserName = read.GetString(3),
                            Password = read.GetString(4),
                            Office = read.GetString(5)
                        };
                }
            }
            if (employee == null)
                throw new RepositoryException("Employee not found");
            return employee;
        }

        public int Save(Employee entity)
        {
            Logger.InfoFormat("Try to add employee {0}", entity);
            _validator.Validate(entity);
            var connection = DBUtils.GetConnection(_props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "INSERT INTO employees(first_name, last_name, username, password, office) VALUES (@first_name, @last_name, @username, @password, @office);select last_insert_rowid();";
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@first_name", (object)entity.FirstName)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@last_name", (object)entity.LastName)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@username", (object)entity.UserName)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@password", (object)entity.Password)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@office", (object)entity.Office)));
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public int Size()
        {
            Logger.InfoFormat("Try to find size");
            var connection = DBUtils.GetConnection(_props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM employees";
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Update(int id, Employee entity)
        {
            Logger.InfoFormat("Try to update employee with id {0} and value {1}", id, entity);
            _validator.Validate(entity);
            var ok = true;
            if (id != entity.Id)
            {
                try
                {
                    FindOne(entity.Id);
                    ok = false;
                }
                catch
                {
                    // ignored
                }
            }
            if (!ok)
                throw new RepositoryException("Id already exists");
            var connection = DBUtils.GetConnection(_props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE employees SET ID = @newId, first_name = @first_name, last_name = @last_name, username = @username, password = @password, office = @office WHERE ID = @id";
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@newId", (object)entity.Id)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@first_name", (object)entity.FirstName)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@last_name", (object)entity.LastName)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@username", (object)entity.UserName)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@password", (object)entity.Password)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@office", (object)entity.Office)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@id", (object)id)));
                command.ExecuteNonQuery();
            }
        }

        public bool FindByCredentials(string username, string password)
        {
            Logger.InfoFormat("Try to find employee by username");
            var connection = DBUtils.GetConnection(_props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM employees WHERE username = @username and password=@password";
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@username", (object)username)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@password", (object)password)));
                using (var read = command.ExecuteReader())
                {
                    if (read.Read())
                        return true;
                }
            }
            return false;
        }
    }
}
