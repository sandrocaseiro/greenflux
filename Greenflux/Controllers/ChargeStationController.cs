using AutoMapper;
using Greenflux.Models.ChargeStations;
using Greenflux.Models.Connectors;
using Greenflux.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Greenflux.Models.ChargeStations.VCreateStationResp;

namespace Greenflux.Controllers
{
    [ApiController]
    public class ChargeStationController : ControllerBase
    {
        private readonly ChargeStationService _chargeStationService;
        private readonly IMapper _mapper;

        public ChargeStationController(ChargeStationService chargeStationService, IMapper mapper)
        {
            _chargeStationService = chargeStationService;
            _mapper = mapper;
        }

        [HttpGet("/v1/groups/{groupId:int}/stations")]
        public async Task<IEnumerable<VChargeStationResp>> FindStationsByGroupList(int groupId)
        {
            var stations = await _chargeStationService.FindAllByGroupIdAsync(groupId);

            return _mapper.Map<IEnumerable<VChargeStationResp>>(stations);
        }

        [HttpGet("/v1/stations/{stationId:int}")]
        public async Task<VChargeStationByIdResp> GeStationsById(int stationId)
        {
            var stations = await _chargeStationService.GetByIdAsync(stationId);

            return _mapper.Map<VChargeStationByIdResp>(stations);
        }

        [HttpPost("/v1/stations")]
        public async Task<IActionResult> CreateStation(VCreateStationReq request)
        {
            var station = _mapper.Map<SChargeStation>(request);
            var connectors = _mapper.Map<IEnumerable<SConnector>>(request.Connectors);

            var generatedStation = await _chargeStationService.CreateAsync(station, connectors);
            var result = _mapper.Map<VCreateStationResp>(generatedStation.chargeStation);
            result.Connectors = _mapper.Map<IEnumerable<VCreateStationConnectorResp>>(generatedStation.connectors);

            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut("/v1/stations/{stationId:int}")]
        public async Task<IActionResult> UpdateChargeStation(int stationId, [FromBody] VUpdateChargeStationReq request)
        {
            var station = _mapper.Map<SChargeStation>(request);
            station.Id = stationId;
            await _chargeStationService.UpdateAsync(station);

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("/v1/stations/{stationId:int}")]
        public async Task<IActionResult> DeleteChargeStation(int stationId)
        {
            await _chargeStationService.DeleteByIdAsync(stationId);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
