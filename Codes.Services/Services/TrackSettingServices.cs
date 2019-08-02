﻿using Codes.Entities.Entities;
using Codes.Services.Core;
using Codes.Services.Interfaces;
using LinqKit;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tenets.Common.Core;
using Tenets.Common.ServicesCommon.Codes.Interface;
using Tenets.Common.ServicesCommon.Codes.Parameters;
using Tenets.Common.ServicesCommon.Identity.Base;

namespace Codes.Services.Services
{
    public class TrackSettingServices : BaseService<TrackSetting, ITrackSettingDto>, ITrackSettingServices
    {
        public TrackSettingServices(IServiceBaseParameter<TrackSetting> businessBaseParameter, IHttpContextAccessor httpContextAccessor) : base(businessBaseParameter, httpContextAccessor)
        {
        }

        public async Task<IDataPagging> GetAllPaggedAsync(BaseParam<TrackSettingFilter> filter)
        {
            try
            {
                int limit = filter.PageSize;
                int offset = ((--filter.PageNumber) * filter.PageSize);
                var query = await _unitOfWork.Repository.FindPaggedAsync(predicate: PredicateBuilderFunction(filter.Filter), skip: offset, take: limit, filter.OrderByValue);
                var data = Mapper.Map<IEnumerable<TrackSetting>, IEnumerable<ITrackSettingDto>>(query.Item2);
                return new DataPagging(++filter.PageNumber, filter.PageSize, query.Item1, ResponseResult.PostResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()));
            }
            catch (Exception e)
            {
                result.Message = e.InnerException != null ? e.InnerException.Message : e.Message;
                result = new ResponseResult(null, status: HttpStatusCode.InternalServerError, exception: e, message: result.Message);
                return new DataPagging(0, 0, 0, result);
            }
        }
        static Expression<Func<TrackSetting, bool>> PredicateBuilderFunction(TrackSettingFilter filter)
        {
            var predicate = PredicateBuilder.New<TrackSetting>(true);
            if (filter.FromTrackId != null)
            {
                predicate = predicate.And(b => b.FromTrackId == filter.FromTrackId);
            }
            if (filter.ToTrackId != null)
            {
                predicate = predicate.And(b => b.ToTrackId == filter.ToTrackId); 
            }
            return predicate;
        }
    }
}
