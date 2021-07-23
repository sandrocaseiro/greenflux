using Dapper;
using Greenflux.Models.Groups;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Greenflux.Data
{
    public class GroupRepository
    {
        private readonly IDbConnection _conn;

        public GroupRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public async Task<SGroup> CreateAsync(SGroup group)
        {
            var id = await _conn.QuerySingleAsync<int>("insert into 'group' (name, capacity) values (@Name, @Capacity); select last_insert_rowid();", group);
            group.Id = id;

            return group;
        }

        public Task UpdateAsync(SGroup group) =>
            _conn.ExecuteAsync("update 'group' set name = @Name, capacity = @Capacity where id = @Id", group);

        public Task DeleteByIdAsync(int id) =>
            _conn.ExecuteAsync("delete from 'group' where id = @Id", new { Id = id });

        public Task<SGroup> FindByIdAsync(int id) =>
            _conn.QuerySingleOrDefaultAsync<SGroup>("select id, name, cast(capacity as REAL) as capacity from 'group' where id = @Id", new { Id = id });

        public Task<IEnumerable<SGroup>> FindAllAsync() =>
            _conn.QueryAsync<SGroup>("select id, name, cast(capacity as REAL) as capacity from 'group'");

        public Task<decimal> GetUsedCurrentByIdAsync(int groupId) =>
            _conn.QuerySingleAsync<decimal>(@"
                select 
                cast(COALESCE(sum(c.max_current), 0) as REAL)
                from
                'group' g
                inner join charge_station s on s.group_id = g.id
                inner join connector c on c.charge_station_id = s.id
                WHERE
                g.id = @Id
            ", new { Id = groupId });
    }
}