﻿using earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Databases;
using earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy;
using global.Duffy.DataAccess.EntityFramework.Repositories;
using System;
using System.Linq;

namespace earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Repositories.FanFootTherapy
{
    public interface ISeasonsRepository : IBaseRepository<SeasonEntity, int>
    {
        SeasonEntity Get(DateTime dateTimeUtc);
    }

    public class SeasonsRepository : BaseRepository<IFanFootTherapyDatabase, SeasonEntity, int>, ISeasonsRepository
    {
        public SeasonsRepository(IFanFootTherapyDatabase database) : base(database)
        {
        }

        public SeasonEntity Get(DateTime dateTimeUtc)
        {
            var seasonEntity = Database
                .Context
                .Set<SeasonEntity>()
                .FirstOrDefault(entity => (dateTimeUtc >= entity.BeginDateTimeUtc) && (dateTimeUtc <= entity.EndDateTimeUtc));

            return seasonEntity;
        }
    }
}
