using Microsoft.Extensions.DependencyInjection;
using lesson_2.Interfaces;
using lesson_2.Services;
using lesson_2.Controllers;
using lesson_2.Models;

namespace lesson_2.Utilties
{
  public static class Utilities
    {
        public static void AddListToDo(this IServiceCollection services)
        {
            services.AddSingleton<IListToDo  , ListToDo >();
        }
    }
}
  