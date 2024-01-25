
using AZTDinner.Api;
using AZTDinner.Application;
using AZTDinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
  builder.Services
            .AppPresentation()
            .AppApplication()
            .AppInfrastructure(builder.Configuration);
  //builder.Services.AddControllers(option=> option.Filters.Add<ErrorHandlingFilterAttribute>());


}

// Add services to the container.

var app = builder.Build();
{
  // app.UseMiddleware<ErrorHandlingMiddleware>();
  app.UseExceptionHandler("/error");
  app.UseHttpsRedirection();
  app.UseAuthentication();
  app.UseAuthorization();
  app.MapControllers();

  app.Run();

}


