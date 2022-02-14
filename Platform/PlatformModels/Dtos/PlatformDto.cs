using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformModels.Dtos;

public class PlatformDto {

    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Publisher { get; set; }

    public string? Cost { get; set; }
}

public class PlatformCreateDto {

    public string? Name { get; set; }

    public string? Publisher { get; set; }

    public string? Cost { get; set; }
}

