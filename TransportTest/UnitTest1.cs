using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Transport.Model;
using Transport.Repository;

namespace TransportTest
{
    [TestClass]
    public class UnitTest1
    {
        ClientDBRepository ClientDBRepository;
        ClientRepository ClientRepository;
        EmployeeDBRepository EmployeeDBRepository;
        EmployeeRepository EmployeeRepository;
        RideDBRepository RideDBRepository;
        RideRepository RideRepository;
        BookingDBRepository BookingDBRepository;
        BookingRepository BookingRepository;

        [TestInitialize]
        public void Initialize()
        {
            ClientDBRepository = new ClientDBRepository(DBUtils.GetProperties());
            ClientRepository = new ClientRepository(new ClientValidator());
            EmployeeDBRepository = new EmployeeDBRepository(DBUtils.GetProperties());
            EmployeeRepository = new EmployeeRepository(new EmployeeValidator());
            RideDBRepository = new RideDBRepository(DBUtils.GetProperties());
            RideRepository = new RideRepository(new RideValidator());
            BookingDBRepository = new BookingDBRepository(DBUtils.GetProperties());
            BookingRepository = new BookingRepository(new BookingValidator());


        }

        [TestMethod]
        public void TestEmployeeRepository()
        {
            var employee = new Employee
            {
                Id = 1,
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                UserName = "TestUser",
                Password = "TestPassword",
                Office = "TestOffice"
            };

            Assert.AreEqual(0, EmployeeRepository.Size());

            try
            {
                EmployeeRepository.Save(employee);
            }
            catch
            {
                Assert.Fail();
            }

            Assert.AreEqual(1, EmployeeRepository.Size());

            var updateEmployee = new Employee
            {
                Id = 1,
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                UserName = "UpdatedUser",
                Password = "UpdatedPassword",
                Office = "UpdatedOffice"
            };

            try
            {
                EmployeeRepository.Update(employee.Id, updateEmployee);
            }
            catch
            {
                Assert.Fail();
            }

            try
            {
                EmployeeRepository.FindOne(employee.Id);
                Assert.Fail();
            }
            catch
            {
                try
                {
                    Assert.AreEqual("UpdatedLastName", EmployeeRepository.FindOne(employee.Id).LastName);
                }
                catch
                {
                    Assert.Fail();
                }
            }

            EmployeeRepository.Delete(updateEmployee.Id);
            Assert.AreEqual(0, EmployeeRepository.Size());
        }

        [TestMethod]
        public void TestClientRepository()
        {
            var client = new Client
            {
                Id = 1,
                FirstName = "TestFirstName",
                LastName = "TestLastName"
            };

            Assert.AreEqual(0, ClientRepository.Size());

            try
            {
                ClientRepository.Save(client);
            }
            catch
            {
                Assert.Fail();
            }

            Assert.AreEqual(1, ClientRepository.Size());

            var updateClient = new Client
            {
                Id = 1,
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName"
            };

            try
            {
                ClientRepository.Update(client.Id, updateClient);
            }
            catch
            {
                Assert.Fail();
            }

            try
            {
                ClientRepository.FindOne(client.Id);
                Assert.Fail();
            }
            catch
            {
                try
                {
                    Assert.AreEqual("UpdatedLastName", ClientRepository.FindOne(client.Id).LastName);
                }
                catch
                {
                    Assert.Fail();
                }
            }

            ClientRepository.Delete(updateClient.Id);
            Assert.AreEqual(0, ClientRepository.Size());
        }

        [TestMethod]
        public void TestRideRepository()
        {
            var ride = new Ride
            {
                Id = 1,
                Destination = "TestDestination",
                Date = DateTime.Parse("2019-01-01"),
                Hour = DateTime.Parse("08:45")
            };

            Assert.AreEqual(0, RideRepository.Size());

            try
            {
                RideRepository.Save(ride);
            }
            catch
            {
                Assert.Fail();
            }

            Assert.AreEqual(1, RideRepository.Size());

            var updateRide = new Ride
            {
                Id = 1,
                Destination = "UpdatedDestination",
                Date = DateTime.Parse("1900-01-01"),
                Hour = DateTime.Parse("01:01")
            };

            try
            {
                RideRepository.Update(ride.Id, updateRide);
            }
            catch
            {
                Assert.Fail();
            }

            try
            {
                ClientRepository.FindOne(ride.Id);
                Assert.Fail();
            }
            catch
            {
                try
                {
                    Assert.AreEqual("UpdatedDestination", RideRepository.FindOne(ride.Id).Destination);
                }
                catch
                {
                    Assert.Fail();
                }
            }

            RideRepository.Delete(updateRide.Id);
            Assert.AreEqual(0, RideRepository.Size());
        }

        [TestMethod]
        public void TestBookingRepository()
        {
            var booking = new Booking
            {
                Id = 1,
                ClientId = 1,
                RideId = 1,
                SeatNo = 10
            };

            Assert.AreEqual(0, BookingRepository.Size());

            try
            {
                BookingRepository.Save(booking);
            }
            catch
            {
                Assert.Fail();
            }

            Assert.AreEqual(1, BookingRepository.Size());

            var updateBooking = new Booking
            {
                Id = 1,
                ClientId = 5,
                RideId = 5,
                SeatNo = 5
            };

            try
            {
                BookingRepository.Update(booking.Id, updateBooking);
            }
            catch
            {
                Assert.Fail();
            }

            try
            {
                BookingRepository.FindOne(booking.Id);
                Assert.Fail();
            }
            catch
            {
                try
                {
                    Assert.AreEqual(5, BookingRepository.FindOne(booking.Id).RideId);
                }
                catch
                {
                    Assert.Fail();
                }
            }

            BookingRepository.Delete(updateBooking.Id);
            Assert.AreEqual(0, BookingRepository.Size());
        }

       
    }
}
