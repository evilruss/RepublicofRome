using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoRService.Repositories.Helpers
{
    public enum RepositoryActionStatus
    {
        Ok,
        Created,
        Updated,
        NotFound,
        Deleted,
        NothingModified,
        Error
    }
}