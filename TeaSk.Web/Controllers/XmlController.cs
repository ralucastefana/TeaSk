using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using TeaSk.Application.Infrastructure;
using TeaSk.Domain.Entities;

namespace TeaSk.Web.Controllers
{
    public class XmlController : Controller
    {
        private readonly IService<Activities> _activitiesService;
        private readonly IService<User> _userService;
        public XmlController(IService<Activities> activitiesService, IService<User> userService)
        {
            _activitiesService = activitiesService;
            _userService = userService;
        }

        //public string Users()
        //{
        //    string result;
        //    return new XmlSerializer(typeof(List<User>)).Serialize(result,_userService.GetAll())
        //    return XmlConvert.SerializeObject(_userService.GetAll(), Formatting.Indented,
        //                        new XmlSerializerSettings
        //                        {
        //                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //                        });
        //}

        //public string Events()
        //{
        //    return JsonConvert.SerializeObject(_activitiesService.GetAll(), Formatting.Indented,
        //                        new JsonSerializerSettings
        //                        {
        //                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //                        });
        //}
    }
}