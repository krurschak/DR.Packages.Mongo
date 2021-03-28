using System;
using System.Collections.Generic;
using System.Text;

namespace DR.Packages.Mongo.Models
{
    public interface IIdentifiable
    {
        Guid Id { get; }
    }
}
