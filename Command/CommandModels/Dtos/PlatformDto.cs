using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandModels.Dtos;

public class PlatformReadDto {

    public int Id { get; set; }
    public string? Name { get; set; }
}

public class PlatformPublishDto {

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Event { get; set; }

}