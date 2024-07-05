using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using Microsoft.Win32;
using System.Collections.Generic;
using TOLEAGRI.Model.Domain;
using TOLEAGRI.Model.Persistence;
using TOLEAGRI.Model.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace TOLEAGRI.Model.Services
{
    public class ManagerService
    {
        private readonly TOLEDbContext dbContext;

        public ManagerService(TOLEDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


    }
}
