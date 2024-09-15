﻿using OT.Assessment.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Core.Services.Services
{
    public interface IPlayerService
    {
        Task<IEnumerable<Player>> GetTopSpendersAsync(int count);
    }
}
