using Microsoft.AspNetCore.Mvc;
using ProjectBackendLearning.Bll.Services;
using ProjectBackendLearning.Core.DTOs;
using ProjectBackendLearning.Core.Models.Requests;

namespace ProjectBackendLearning.Controllers;

[ApiController]
[Route("/api/devices")]
public class DevicesController : Controller
{
    private readonly IDevicesService _devicesService;

    public DevicesController(IDevicesService devicesService)
    {
        _devicesService = devicesService;
    }

    [HttpGet("{id}")]
    public ActionResult<DeviceDto> GetDeviceById(Guid id)
    {
        if (id == Guid.Empty)
        {
            return NotFound($"Девайс с Id {id} не найден");
        }

        return Ok(_devicesService.GetDeviceById(id));
    }

    [HttpPost]
    public IActionResult SetDeviceStatus([FromBody] Guid deviceId, bool value)
    {
        _devicesService.SetDeviceStatus(deviceId, value);
        
        return Ok();
    }

    [HttpPost("{userId}")]
    public ActionResult AddDeviceToUser(Guid userId, [FromBody] AddDeviceToUserRequest request)
    {
        _devicesService.AddDeviceToUser(userId, request);

        return Ok();
    }

    [HttpGet()]
    public ActionResult<List<DeviceDto>> GetDevice([FromQuery] Guid? id, [FromQuery] Guid? userId)
    {
        if (userId is not null)
        {
            return Ok(_devicesService.GetDeviceById((Guid)userId));
        }

        if (id is not null)
        {
            return Ok(_devicesService.GetDeviceById((Guid)id));
        }

        return Ok(new List<DeviceDto>());
    }

    [HttpGet("by-user/{userId}")]
    public DeviceDto GetDeviceByUserId(Guid id)
    {
        return _devicesService.GetDeviceByUserId(Guid.NewGuid());
    }
}