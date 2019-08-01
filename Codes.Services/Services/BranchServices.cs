﻿using Codes.Entities.Entities;
using Codes.Services.Core;
using Codes.Services.Interfaces;
using LinqKit;
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
    public class BranchServices : BaseService<Branch, IBranchDto>, IBranchServices
    {
        public BranchServices(IServiceBaseParameter<Branch> businessBaseParameter) : base(businessBaseParameter)
        {

        }
        public async Task<IDataPagging> GetAllPaggedAsync(BaseParam<BranchFilter> filter)
        {
            try
            {
                int limit = filter.PageSize;
                int offset = ((--filter.PageNumber)*filter.PageSize);
                var query = await _unitOfWork.Repository.FindPaggedAsync(predicate: PredicateBuilderFunction(filter.Filter),skip:offset, take: limit,filter.OrderByValue);
                var data = Mapper.Map<IEnumerable<Branch>, IEnumerable<IBranchDto>>(query.Item2);
                return new DataPagging(++filter.PageNumber, filter.PageSize, query.Item1, ResponseResult.PostResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()));
            }
            catch (Exception e)
            {
                result.Message = e.InnerException != null ? e.InnerException.Message : e.Message;
                result = new ResponseResult(null, status: HttpStatusCode.InternalServerError, exception: e, message: result.Message);
                return new DataPagging(0,0,0,result);
            }
        }
        static Expression<Func<Branch, bool>> PredicateBuilderFunction(BranchFilter filter)
        {
            var predicate = PredicateBuilder.New<Branch>(true);
            if (filter.CompanyId!=null)
            {
                predicate = predicate.And(b => b.CompanyId == filter.CompanyId);
            }
            if (!string.IsNullOrWhiteSpace(filter.NameAr))
            {
                predicate = predicate.And(b => b.NameAr.ToLower().Contains(filter.NameAr.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter.NameEn))
            {
                predicate = predicate.And(b => b.NameEn.ToLower().Contains(filter.NameEn.ToLower()));
            }
            return predicate;
        }
    }
}
