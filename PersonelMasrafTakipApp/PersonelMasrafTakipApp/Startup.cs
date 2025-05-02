using System.Reflection;
using MasrafTakip.Base;
using Microsoft.EntityFrameworkCore;
using PersonelMasrafTakipApp.Impl.Cqrs;

using AutoMapper;
using FluentValidation.AspNetCore;
using PersonelMasrafTakipApp.Impl.Validation;
using PersonelMasrafTakipApp.Mapper;

namespace PersonelMasrafTakipApp;

public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration) => Configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {

 services.AddControllers().AddFluentValidation(x =>
        {
            x.RegisterValidatorsFromAssemblyContaining<UserValidator>();
        });

        services.AddSingleton(new MapperConfiguration(x => x.AddProfile(new MapperConfig())).CreateMapper());

        services.AddControllers();

        services.AddSwaggerGen();

        string connectionStringMsSql = Configuration.GetConnectionString("MsSqlConnection");

        services.AddDbContext<MsSqlDbContext>(options => { options.UseSqlServer(connectionStringMsSql); });

        services.AddMediatR(x=> x.RegisterServicesFromAssemblies(typeof(CreateUserCommand).GetTypeInfo().Assembly));


    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

    }
}