﻿namespace CarShop.Services
{
    using CarShop.Data;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly CarShopDbContext data;

        public UserService(CarShopDbContext data)
        {
            this.data = data;
        }

        public bool IsMechanic(string userId)
        => this.data.Users.Any(u => u.Id == userId && u.IsMechanic);


        public bool OwnsCar(string userId, string carId)
            => this.data
                    .Cars
                    .Any(u => u.Id == carId && u.OwnerId == userId);
    }
}
