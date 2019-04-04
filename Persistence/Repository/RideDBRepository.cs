using log4net;
using System;
using System.Collections.Generic;
using Transport.Model;

namespace Transport.Repository
{
    public class RideDBRepository : IRepository<int, Ride>
    {

        private static IValidator<Ride> _validator;

        private static readonly ILog Logger = LogManager.GetLogger("RideDBRepository");

        private readonly IDictionary<string, string> _props;

        public RideDBRepository(IDictionary<string, string> props)
        {
            Logger.Info("Creating RideDBRepository");
            _validator = new RideValidator();
            _props = props;
        }

        public void Delete(int id)
        {
            Logger.InfoFormat("Try to delete ride with id {0}", id);
            var connection = DBUtils.GetConnection(_props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM rides where ID=@id";
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@id", (object)id)));
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Ride> FindAll()
        {
            Logger.Info("Try to find all");
            var connection = DBUtils.GetConnection(_props);
            var rides = new List<Ride>();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM rides";
                using (var read = command.ExecuteReader())
                {
                    while (read.Read())
                    {

                        var ride = new Ride
                        {
                            Id = read.GetInt32(0),
                            Destination = read.GetString(1),
                            Date = read.GetDateTime(2),
                            Hour = read.GetDateTime(3)
                        };
                        rides.Add(ride);
                    }
                }
            }
            return rides;
        }

        public Ride FindOne(int id)
        {
            Logger.InfoFormat("Try to find id {0}", id);
            var connection = DBUtils.GetConnection(_props);
            Ride ride = null;
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM rides WHERE ID = @id";
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@id", (object)id)));
                using (var read = command.ExecuteReader())
                {
                    if (read.Read())
                        ride = new Ride
                        {
                            Id = read.GetInt32(0),
                            Destination = read.GetString(1),
                            Date = read.GetDateTime(2),
                            Hour = read.GetDateTime(3)
                        };
                }
            }
            if (ride == null)
                throw new RepositoryException("Ride not found");
            return ride;
        }

        public int Save(Ride entity)
        {
            Logger.InfoFormat("Try to add ride {0}", entity);
            _validator.Validate(entity);
            var connection = DBUtils.GetConnection(_props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "INSERT INTO rides(destination, date, hour) VALUES (@destination, @date, @hour);select last_insert_rowid();";
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@destination", (object)entity.Destination)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@date", (object)entity.Date)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@Hour", (object)entity.Hour)));
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public int Size()
        {
            Logger.InfoFormat("Try to find size");
            var connection = DBUtils.GetConnection(_props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM rides";
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Update(int id, Ride entity)
        {
            Logger.InfoFormat("Try to update ride with id {0} and value {1}", id, entity);
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
                command.CommandText = "UPDATE rides SET ID = @newId, destination = @destination, date = @date, hour = @hour WHERE ID = @id";
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@newId", (object)entity.Id)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@destination", (object)entity.Destination)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@date", (object)entity.Date)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@Hour", (object)entity.Hour)));
                command.ExecuteNonQuery();
            }
        }

        public IDictionary<Ride, int> FindRide(string destination, string date, string hour)
        {
            Logger.InfoFormat("Try to find ride");
            var connection = DBUtils.GetConnection(_props);
            Ride ride = null;
            int countRows = 0;
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM rides WHERE destination = @destination and date=@date and hour=@hour";
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@destination", (object)destination)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@date", (object)date)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@hour", (object)hour)));
                using (var read = command.ExecuteReader())
                {
                    if (read.Read())
                        ride = new Ride
                        {
                            Id = read.GetInt32(0),
                            Destination = read.GetString(1),
                            Date = read.GetDateTime(2),
                            Hour = read.GetDateTime(3)
                        };
                }
                if (ride == null)
                {
                    ride = new Ride
                    {
                        Id = 1,
                        Destination = "None",
                        Date = DateTime.MinValue,
                        Hour = new DateTime()
                    };
                    return new Dictionary<Ride, int> { { ride, countRows } };
                }
                using (var command2 = connection.CreateCommand())
                {
                    command2.CommandText = "select count(bookings.id) from bookings inner join rides on rides.id=bookings.id_ride " +
                    "where rides.id=@id";
                    command2.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@id", (object)ride.Id)));
                    using (var read = command2.ExecuteReader())
                    {
                        if (read.Read())
                        {
                            countRows = read.GetInt32(0);
                        }
                    }
                }
            }
            var dictionary = new Dictionary<Ride, int>
            {
                { ride, countRows }
            };
            return dictionary;
        }
    }
}
