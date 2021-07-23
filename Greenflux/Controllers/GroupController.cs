using AutoMapper;
using Greenflux.Models.Groups;
using Greenflux.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Greenflux.Controllers
{
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly GroupService _groupService;
        private readonly IMapper _mapper;

        public GroupController(GroupService groupService, IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }

        [HttpGet("/v1/groups")]
        public async Task<IEnumerable<VGroupResp>> FindGroupList()
        {
            var groups = await _groupService.FindAllAsync();

            return _mapper.Map<IEnumerable<VGroupResp>>(groups);
        }

        [HttpGet("/v1/groups/{groupId:int}")]
        public async Task<VGroupResp> GetGroupById(int groupId)
        {
            var group = await _groupService.GetByIdAsync(groupId);

            return _mapper.Map<VGroupResp>(group);
        }

        [HttpPost("/v1/groups")]
        public async Task<IActionResult> CreateGroup(VCreateGroupReq request)
        {
            var group = _mapper.Map<SGroup>(request);
            var generatedGroup = await _groupService.CreateAsync(group);

            return StatusCode(StatusCodes.Status201Created, _mapper.Map<VGroupResp>(generatedGroup));
        }

        [HttpPut("/v1/groups/{groupId:int}")]
        public async Task<IActionResult> UpdateGroup(int groupId, [FromBody] VCreateGroupReq request)
        {
            var group = _mapper.Map<SGroup>(request);
            group.Id = groupId;
            await _groupService.UpdateAsync(group);

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("/v1/groups/{groupId:int}/name")]
        public async Task<IActionResult> UpdateGroupName(int groupId, [FromBody] VUpdateGroupNameReq request)
        {
            await _groupService.UpdateNameAsync(groupId, request.Name);

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("/v1/groups/{groupId:int}/capacity")]
        public async Task<IActionResult> UpdateGroupCapacity(int groupId, [FromBody] VUpdateGroupCapacityReq request)
        {
            await _groupService.UpdateCapacityAsync(groupId, request.Capacity.Value);

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("/v1/groups/{groupId:int}")]
        public async Task<IActionResult> DeleteGroup(int groupId)
        {
            await _groupService.DeleteByIdAsync(groupId);

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
