﻿using System;
using System.Collections.Generic;
using System.Text;
using Tenets.Common.ServicesCommon.Codes.Interface;

namespace Codes.Services.Dto
{
    public class CityDto : ICityDto
    {
        public string NameAr { get ; set ; }
        public string NameEn { get ; set ; }
        public Guid CountryId { get ; set ; }
        public Guid Id { get ; set ; }
    }
}