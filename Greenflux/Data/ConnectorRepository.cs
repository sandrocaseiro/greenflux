using Dapper;
using Greenflux.Models.Connectors;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Greenflux.Data
{
    public class ConnectorRepository
    {
        private readonly IDbConnection _conn;

        public ConnectorRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public Task CreateAsync(SConnector connector) =>
            _conn.ExecuteAsync("insert into connector (id, charge_station_id, max_current) values (@Id, @ChargeStationId, @MaxCurrent)", connector);

        public Task UpdateAsync(SConnector connector) =>
            _conn.ExecuteAsync("update connector set max_current = @MaxCurrent where id = @Id and charge_station_id = @ChargeStationId",
                connector);

        public Task DeleteByIdAsync(int id, int chargeStationId) =>
            _conn.ExecuteAsync("delete from connector where id = @Id and charge_station_id = @ChargeStationId", new { Id = id, ChargeStationId = chargeStationId });

        public Task DeleteByGroupIdAsync(int groupId) =>
            _conn.ExecuteAsync("delete from connector where charge_station_id in (select id from charge_station where group_id = @GroupId)",
                new { GroupId = groupId });

        public Task DeleteByChargeStationIdAsync(int chargeStationId) =>
            _conn.ExecuteAsync("delete from connector where charge_station_id = @ChargeStationId", new { ChargeStationId = chargeStationId });

        public Task<SConnector> FindByIdAndChargeStationIdAsync(int id, int chargeStationId) =>
            _conn.QuerySingleOrDefaultAsync<SConnector>("select id, cast(max_current as REAL) as MaxCurrent, charge_station_id as ChargeStationId from connector where id = @Id and charge_station_id = @ChargeStationId",
                new { Id = id, ChargeStationId = chargeStationId });

        public Task<IEnumerable<SConnector>> FindAllByGroupAsync(int groupId) =>
            _conn.QueryAsync<SConnector>("select id, cast(max_current as REAL) as MaxCurrent, charge_station_id as ChargeStationId from connector where charge_station_id in (select id from charge_station where group_id = @GroupId)", new { GroupId = groupId });

        public Task<IEnumerable<SConnector>> FindAllByChargeStationAsync(int chargeStationId) =>
            _conn.QueryAsync<SConnector>("select id, cast(max_current as REAL) as MaxCurrent, charge_station_id as ChargeStationId from connector where charge_station_id = @ChargeStationId", new { ChargeStationId = chargeStationId });
    }
}