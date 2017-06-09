using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeaSk.Application.Infrastructure;
using TeaSk.Domain.Entities;

namespace TeaSk.Web.Controllers
{
    public class JsonController : ApiController
    {
        private readonly IService<Activities> _activitiesService;
        private readonly IService<User> _userService;
        public JsonController(IService<Activities> activitiesService, IService<User> userService)
        {
            _activitiesService = activitiesService;
            _userService = userService;
        }

        public string Users()
        {
            return JsonConvert.SerializeObject(_userService.GetAll());
        }

        public string Events()
        {
            return JsonConvert.SerializeObject(_activitiesService.GetAll());
        }
    }
}
