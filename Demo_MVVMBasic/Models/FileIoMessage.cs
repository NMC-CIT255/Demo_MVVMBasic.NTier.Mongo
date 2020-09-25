using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_MVVMBasic
{
    /// <summary>
    /// file I/O status codes
    /// </summary>
    public enum FileIoMessage
    {
        None,
        Complete,
        FileAccessError,
        RecordNotFound,
        NoRecordsFound
    }
}
