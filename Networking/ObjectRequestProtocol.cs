﻿using Networking;
using System;

namespace networking
{
    public interface IRequest
    {

    }

    [Serializable]
    public class LoginRequest : IRequest
    {

        public LoginRequest(EmployeeDTO employeeDto)
        {
            EmployeeDto = employeeDto;
        }

        public virtual EmployeeDTO EmployeeDto { get; }
    }

    [Serializable]
    public class GetAllRidesRequest : IRequest
    {
        public GetAllRidesRequest(RideDTO rideDto)
        {
            RideDto = rideDto;
        }

        public virtual RideDTO RideDto { get; }
    }

    [Serializable]
    public class GetCustomRidesRequest : IRequest
    {
        public GetCustomRidesRequest(CustomRideDTO customRideDto)
        {
            CustomRideDto = customRideDto;
        }

        public virtual CustomRideDTO CustomRideDto { get; }
    }

    [Serializable]
    public class GetCustomClientsRequest : IRequest
    {
        public GetCustomClientsRequest(ClientDTO clientDto)
        {
            ClientDto = clientDto;
        }

        public virtual ClientDTO ClientDto { get; }
    }


    [Serializable]
    public class BookingRequest : IRequest
    {
        public BookingRequest(BookingDTO bookingDto, RideDTO rideDto, EmployeeDTO employeeDto)
        {
            BookingDto = bookingDto;
            RideDto = rideDto;
            EmployeeDto = employeeDto;
        }

        public virtual BookingDTO BookingDto { get; }
        public virtual EmployeeDTO EmployeeDto { get; }
        public virtual RideDTO RideDto { get; }
    }

    [Serializable]
    public class ReservationRefreshRequest : IRequest
    {

    }

    [Serializable]
    public class LogOutRequest : IRequest
    {
        public LogOutRequest(EmployeeDTO employeeDto)
        {
            EmployeeDto = employeeDto;
        }

        public virtual EmployeeDTO EmployeeDto { get; }
    }
}