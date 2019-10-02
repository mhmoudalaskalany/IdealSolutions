﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Codes.API.Controllers.Base;
using Codes.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Tenets.Common.Core;
using Tenets.Common.ServicesCommon.Codes.Parameters;
using Tenets.Common.ServicesCommon.Transaction.Parameters;

namespace Codes.API.Controllers
{
    /// <inherit  />
    public class LookupsController : BaseController
    {
        private readonly ILookupsServices _lookupsServices;
        /// <inheritdoc />
        public LookupsController(ILookupsServices lookupsServices)
        {
            _lookupsServices = lookupsServices;
        }
        /// <summary>
        /// Get all lookups in end point data 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IResult> GetLookups()
        {
            return await _lookupsServices.GetAllLookupsForPolicy();
        }
        /// <summary>
        /// Get track setting for policy
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> GetTrackSettings(TrackPriceBasedOnParameters trackPriceBasedOnParameters)
        {
            return await _lookupsServices.GettrackSettingForPolicy(trackPriceBasedOnParameters.CustomerId, trackPriceBasedOnParameters.PolicyDate);
        }
        /// <summary>
        /// Get car types for policy
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> GetCarTypes(TrackPriceBasedOnParameters trackPriceBasedOnParameters)
        {
            return await _lookupsServices.GetCarTypesForPolicy(trackPriceBasedOnParameters.CustomerId, trackPriceBasedOnParameters.PolicyDate,trackPriceBasedOnParameters.TrackSettingId??Guid.Empty);
        }
        /// <summary>
        /// Get Type for CustomerName or Rent Name  
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> GetTypeNameForOpeningBalance(IEnumerable<OpeningBalanceParameters> parameters)
        {
            return await _lookupsServices.GetTypeNameForOpeningBalance(parameters);
        }
        
    }
}