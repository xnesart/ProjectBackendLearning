using AutoMapper;
using ProjectBackendLearning.Core.DTOs;
using ProjectBackendLearning.Core.Enums;
using ProjectBackendLearning.Core.Exceptions;
using ProjectBackendLearning.Core.Models.Requests;
using ProjectBackendLearning.DataLayer.Repositories;

namespace ProjectBackendLearning.Bll.Services;

public class DevicesService : IDevicesService
{
    private readonly IDevicesRepository _devicesRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IUsersService _usersService;
    private readonly IMapper _mapper;

    public DevicesService(IMapper mapper, IDevicesRepository devicesRepository, IUsersService usersService,IUsersRepository usersRepository)
    {
        _devicesRepository = devicesRepository;
        _usersService = usersService;
        _usersRepository = usersRepository;
        _mapper = mapper;
    }

    public DeviceDto GetDeviceById(Guid id) => _devicesRepository.GetDeviceById(id);
    public DeviceDto GetDeviceByUserId(Guid userId) => _devicesRepository.GetDeviceByUserId(userId);

    public void AddDeviceToUser(Guid id, AddDeviceToUserRequest request)
    {
        var user = _usersService.GetUserById(id);
        if (user is null) throw new NotFoundException($"пользователь с Id {id} не найден");

        int enumCount = Enum.GetNames(typeof(DeviceType)).Length;
        if ((int)request.DeviceType > enumCount) throw new ValidationException($"передан неверный тип устройства");

        DeviceDto device = _mapper.Map<DeviceDto>(request);
        
        device.Owner = user;

        _devicesRepository.AddDeviceToUser(device);
    }

    public void SetDeviceStatus(Guid deviceId, bool value)
    {
        var device = GetDeviceById(deviceId);
        if (device is null) throw new NotFoundException($"девайс с id {deviceId} не найден");

        device.Status = value;

        _devicesRepository.SetDeviceStatus(device);
    }

    public List<DeviceDto> GetDevicesWithStatus()
    {
        return _devicesRepository.GetDevicesWhereStatusIsNotNull();
    }
}