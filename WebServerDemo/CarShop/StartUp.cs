﻿namespace CarShop
{
    using CarShop.Data;
    using CarShop.Services;
    using Microsoft.EntityFrameworkCore;
    using MyWebServer;
    using MyWebServer.Controllers;
    using MyWebServer.Results.Views;
    using System.Threading.Tasks;


    public class StartUp
    {
        public static async Task Main()
            => await HttpServer
            .WithRoutes(routes => routes
                .MapStaticFiles()
                .MapControllers())
            .WithServices(services => services
                .Add<IViewEngine, CompilationViewEngine>()
                .Add<IValidator,Validator>()
                .Add<IPasswordHasher,PasswordHasher>()
                .Add<IUserService, UserService>()
                .Add<CarShopDbContext>())
            .WithConfiguration<CarShopDbContext>(c => c.Database.Migrate())
            .Start();
    }
}
