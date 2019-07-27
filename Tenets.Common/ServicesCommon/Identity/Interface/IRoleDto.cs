﻿using System;
using Tenets.Common.Core;

namespace Tenets.Common.ServicesCommon.Identity.Interface
{
    public interface IRoleDto: IPrimaryKeyField<Guid>
    {
        string Name { get; set; }
        bool IsDeleted { get; set; }
        int? UsersRoleCount { get; set; }
    }
}