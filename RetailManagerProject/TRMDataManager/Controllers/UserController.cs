﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

using TRMDataManager.Library.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Controllers
{
    [Authorize]
   
    public class UserController : ApiController
    {
       
      
        [HttpGet]
        public UserModel GetById()
        {
            var userId = RequestContext.Principal.Identity.GetUserId();

            UserData userData = new UserData();

            return userData.GetUserById(userId).First();
           
        }

      
       

    }
}
