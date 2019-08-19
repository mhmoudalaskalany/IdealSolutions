﻿using AutoMapper;
using Codes.Entities.Entities;
using Codes.Services.Dto;
using Codes.Services.Interfaces;
using Codes.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tenets.Common.Core;
using Tenets.Common.ServicesCommon.Base;

namespace Codes.Services.Services
{
    public class LookupsServices : ILookupsServices
    {
        private readonly IMapper _mapper;
        private readonly IResponseResult _responseResult;
        private readonly IUnitOfWork<InvoiceType> _invoiceType;
        private readonly IUnitOfWork<Car> _car;
        private readonly IUnitOfWork<CarType> _carType;
        private readonly IUnitOfWork<CustomerCategory> _customerCategory;
        private readonly IUnitOfWork<TrackSetting> _trackSetting;
        private readonly IUnitOfWork<TaxType> _taxType;
        private IResult result;
        public LookupsServices(IMapper mapper, IResponseResult responseResult,IUnitOfWork<InvoiceType> invoiceType, IUnitOfWork<Car> car, IUnitOfWork<CarType> carType, IUnitOfWork<CustomerCategory> customerCategory, IUnitOfWork<TrackSetting> trackSetting, IUnitOfWork<TaxType> taxType)
        {
            _mapper = mapper;
            _responseResult = responseResult;
            _invoiceType = invoiceType;
            _car = car;
            _carType = carType;
            _customerCategory = customerCategory;
            _trackSetting = trackSetting;
            _taxType = taxType;
        }
        public async Task<IResult> GetAllLookupsForPolicy()
        {
            try
            {
                var lookups = new List<DropdownDto>();
                var invoiceType = await _invoiceType.Repository.GetAllAsync();
                lookups.AddRange(_mapper.Map<IEnumerable<DropdownDto>>(invoiceType));
                var car = await _car.Repository.GetAllAsync();
                lookups.AddRange(_mapper.Map<IEnumerable<DropdownDto>>(car));
                var carType = await _carType.Repository.GetAllAsync();
                lookups.AddRange(_mapper.Map<IEnumerable<DropdownDto>>(carType));
                var customerCategory = await _customerCategory.Repository.GetAllAsync();
                lookups.AddRange(_mapper.Map<IEnumerable<DropdownDto>>(customerCategory));
                var trackSetting = await _trackSetting.Repository.GetAllAsync(disableTracking: false);
                lookups.AddRange(_mapper.Map<IEnumerable<DropdownDto>>(trackSetting));
                var taxType = await _taxType.Repository.GetAllAsync();
                lookups.AddRange(_mapper.Map<IEnumerable<DropdownDto>>(taxType));
                return _responseResult.PostResult(lookups, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
            }
            catch (Exception e)
            {
                result.Message = e.InnerException != null ? e.InnerException.Message : e.Message;
                result = new ResponseResult(null, status: HttpStatusCode.InternalServerError, exception: e, message: result.Message);
                return result;
            }
        }
    }
}