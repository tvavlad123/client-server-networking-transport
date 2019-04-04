using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Model;

namespace Transport.Repository
{
    public class ClientDBRepository : IRepository<int, Client>
    {
        private static IValidator<Client> _validator;

        private static readonly ILog Logger = LogManager.GetLogger("ClientDBRepository");

        private readonly IDictionary<string, string> _props;

        public ClientDBRepository(IDictionary<string, string> props)
        {
            Logger.Info("Creating ClientDBValidator");
            _validator = new ClientValidator();
            _props = props;
        }

        public void Delete(int id)
        {
            Logger.InfoFormat("Try to delete client with id {0}", id);
            var connection = DBUtils.GetConnection(_props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM clients WHERE ID = @id";
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@id", (object)id)));
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Client> FindAll()
        {
            Logger.Info("Try to find all");
            var connection = DBUtils.GetConnection(_props);
            var clients = new List<Client>();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM clients";
                using (var read = command.ExecuteReader())
                {
                    while (read.Read())
                    {

                        var client = new Client
                        {
                            Id = read.GetInt32(0),
                            FirstName = read.GetString(1),
                            LastName = read.GetString(2)
                        };
                        clients.Add(client);
                    }
                }
            }
            return clients;
        }

        public Client FindOne(int id)
        {
            Logger.InfoFormat("Try to find id {0}", id);
            var connection = DBUtils.GetConnection(_props);
            Client client = null;
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM clients WHERE ID = @id";
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@id", (object)id)));
                using (var read = command.ExecuteReader())
                {
                    if (read.Read())
                        client = new Client
                        {
                            Id = read.GetInt32(0),
                            FirstName = read.GetString(1),
                            LastName = read.GetString(2)
                        };
                }
            }
            if (client == null)
                throw new RepositoryException("Client not found");
            return client;
        }

        public int Save(Client entity)
        {
            Logger.InfoFormat("Try to add client {0}", entity);
            _validator.Validate(entity);
            var connection = DBUtils.GetConnection(_props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "INSERT INTO clients(first_name, last_name) VALUES (@first_name, @last_name);select last_insert_rowid();";
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@first_name", (object)entity.FirstName)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@last_name", (object)entity.LastName)));
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public int Size()
        {
            Logger.InfoFormat("Try to find size");
            var connection = DBUtils.GetConnection(_props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM clients";
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Update(int id, Client entity)
        {
            throw new NotImplementedException();
        }

        public Client FindByFirstAndLastName(string firstName, string lastName)
        {
            Logger.InfoFormat("Try to find by name");
            var connection = DBUtils.GetConnection(_props);
            Client client = null;
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * from clients where first_name=@firstName and last_name=@lastName";
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@firstName", (object)firstName)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@lastName", (object)lastName)));

                using (var read = command.ExecuteReader())
                {
                    if (read.Read())
                        client = new Client
                        {
                            Id = read.GetInt32(0),
                            FirstName = read.GetString(1),
                            LastName = read.GetString(2)
                        };
                }
            }
            return client;
        }
    }
}
