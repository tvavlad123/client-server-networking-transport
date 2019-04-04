using log4net;
using System;
using System.Collections.Generic;
using Transport.Model;

namespace Transport.Repository
{
    public class BookingDBRepository : IRepository<int, Booking>
    {
        private static IValidator<Booking> _validator;

        private static readonly ILog Logger = LogManager.GetLogger("BookingDBRepository");

        private readonly IDictionary<string, string> _props;

        public BookingDBRepository(IDictionary<string, string> props)
        {
            Logger.Info("Creating BookingDBRepository");
            _validator = new BookingValidator();
            _props = props;
        }

        public void Delete(int id)
        {
            Logger.InfoFormat("Try to delete employee with id {0}", id);
            var connection = DBUtils.GetConnection(_props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM bookings WHERE ID = @id";
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@id", (object)id)));
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Booking> FindAll()
        {
            Logger.Info("Try to find all");
            var connection = DBUtils.GetConnection(_props);
            var bookings = new List<Booking>();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM bookings";
                using (var read = command.ExecuteReader())
                {
                    while (read.Read())
                    {

                        var booking = new Booking
                        {
                            Id = read.GetInt32(0),
                            ClientId = read.GetInt32(1),
                            RideId = read.GetInt32(2),
                            SeatNo = read.GetInt32(3)
                        };
                        bookings.Add(booking);
                    }
                }
            }
            return bookings;
        }

        public Booking FindOne(int id)
        {
            Logger.InfoFormat("Try to find id {0}", id);
            var connection = DBUtils.GetConnection(_props);
            Booking booking = null;
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM bookings WHERE ID = @id";
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@id", (object)id)));
                using (var read = command.ExecuteReader())
                {
                    if (read.Read())
                        booking = new Booking
                        {
                            Id = read.GetInt32(0),
                            ClientId = read.GetInt32(1),
                            RideId = read.GetInt32(2),
                            SeatNo = read.GetInt32(3)
                        };
                }
            }
            if (booking == null)
                throw new RepositoryException("Booking not found");
            return booking;
        }

        public int Save(Booking entity)
        {
            Logger.InfoFormat("Try to add booking {0}", entity);
            _validator.Validate(entity);
            var connection = DBUtils.GetConnection(_props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "INSERT INTO bookings(id_client, id_ride, seat_no) VALUES (@id_client, @id_ride, @seat_no);select last_insert_rowid();";
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@id_client", (object)entity.ClientId)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@id_ride", (object)entity.RideId)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@seat_no", (object)entity.SeatNo)));
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public int Size()
        {
            Logger.InfoFormat("Try to find size");
            var connection = DBUtils.GetConnection(_props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM bookings";
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Update(int id, Booking entity)
        {
            Logger.InfoFormat("Try to update booking with id {0} and value {1}", id, entity);
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
                command.CommandText = "UPDATE bookings SET ID = @newId, id_client = @id_client, id_ride = @id_ride, seat_no = @seat_no WHERE ID = @id";
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@newId", (object)entity.Id)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@id_client", (object)entity.ClientId)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@id_ride", (object)entity.RideId)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@seat_no", (object)entity.SeatNo)));
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@id", (object)id)));
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Booking> FindByRide(int rideId)
        {
            Logger.InfoFormat("Try to find by rideId {0}", rideId);
            var connection = DBUtils.GetConnection(_props);
            List<Booking> bookings = new List<Booking>();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM bookings WHERE id_ride = @rideId";
                command.Parameters.Add(DBUtils.GetParameter(command, Tuple.Create("@rideId", (object)rideId)));
                using (var read = command.ExecuteReader())
                {
                    while (read.Read())
                    {
                        var booking = new Booking
                        {
                            Id = read.GetInt32(0),
                            ClientId = read.GetInt32(1),
                            RideId = read.GetInt32(2),
                            SeatNo = read.GetInt32(3)
                        };
                        bookings.Add(booking);
                    }
                }
            }
            return bookings;
        }
    }
}
