﻿using PlatformModels.Dtos;

namespace PlatformService.AsyncDataServices;

public interface IMessageBusClient {

    void PublishNewPlatform(PlatformPublishDto plat);






}
