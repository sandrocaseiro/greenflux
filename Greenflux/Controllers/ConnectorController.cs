using AutoMapper;
using Greenflux.Models.Connectors;
using Greenflux.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Greenflux.Controllers
{
    [ApiController]
    public class ConnectorController : ControllerBase
    {
        private readonly ConnectorService _connectorService;
        private readonly IMapper _mapper;

        public ConnectorController(ConnectorService connectorService, IMapper mapper)
        {
            _connectorService = connectorService;
            _mapper = mapper;
        }

        [HttpGet("/v1/groups/{groupId:int}/connectors")]
        public async Task<IEnumerable<VConnectorResp>> FindConnectorsByGroupList(int groupId)
        {
            var connectors = await _connectorService.FindAllByGroupAsync(groupId);

            return _mapper.Map<IEnumerable<VConnectorResp>>(connectors);
        }

        [HttpGet("/v1/stations/{stationId:int}/connectors")]
        public async Task<IEnumerable<VConnectorByStationResp>> FindConnectorsByChargeStationList(int stationId)
        {
            var connectors = await _connectorService.FindAllByChargeStationAsync(stationId);

            return _mapper.Map<IEnumerable<VConnectorByStationResp>>(connectors);
        }

        [HttpPost("/v1/stations/{stationId:int}/connectors")]
        public async Task<IActionResult> CreateConnector(int stationId, [FromBody] VCreateConnectorReq request)
        {
            var connector = _mapper.Map<SConnector>(request);
            connector.ChargeStationId = stationId;
            var generatedConnector = await _connectorService.CreateAsync(connector);

            return StatusCode(StatusCodes.Status201Created, _mapper.Map<VConnectorByStationResp>(generatedConnector));
        }

        [HttpPut("/v1/stations/{stationId:int}/connectors/{connectorId:int}")]
        public async Task<IActionResult> UpdateConnector(int stationId, int connectorId, [FromBody] VUpdateConnectorReq request)
        {
            var connector = _mapper.Map<SConnector>(request);
            connector.Id = connectorId;
            connector.ChargeStationId = stationId;
            await _connectorService.UpdateAsync(connector);

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("/v1/stations/{stationId:int}/connectors/{connectorId:int}")]
        public async Task<IActionResult> DeleteConnector(int stationId, int connectorId)
        {
            await _connectorService.DeleteByIdAsync(connectorId, stationId);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
