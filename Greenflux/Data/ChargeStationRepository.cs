using Dapper;
using Greenflux.Models.ChargeStations;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Greenflux.Data
{
    public class ChargeStationRepository
    {
        private readonly IDbConnection _conn;

        public ChargeStationRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public async Task<SChargeStation> CreateAsync(SChargeStation station)
        {
            var id = await _conn.QuerySingleAsync<int>("insert into charge_station (name, group_id) values (@Name, @GroupId); select last_insert_rowid();", station);
            station.Id = id;

            return station;
        }

        public Task UpdateAsync(SChargeStation station) =>
            _conn.ExecuteAsync("update charge_station set name = @Name where id = @Id", station);

        public Task DeleteByIdAsync(int id) =>
            _conn.ExecuteAsync("delete from charge_station where id = @Id", new { Id = id });

        public Task DeleteByGroupIdAsync(int groupId) =>
            _conn.ExecuteAsync("delete from charge_station where group_id = @GroupId", new { GroupId = groupId });

        public Task<SChargeStation> FindByIdAsync(int id) =>
            _conn.QuerySingleOrDefaultAsync<SChargeStation>("select id, name, group_id as groupId from charge_station where id = @Id", new { Id = id });

        public Task<IEnumerable<SChargeStation>> FindAllByGroupIdAsync(int groupId) =>
            _conn.QueryAsync<SChargeStation>("select id, name, group_id as groupId from charge_station where group_id = @GroupId", new { GroupId = groupId });
    }
}