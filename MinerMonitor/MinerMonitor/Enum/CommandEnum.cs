using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinerDaemon.Enum
{
    public enum CommandEnum
    {
        WORKER_STATE_CMD,
        GPU_STATE_CMD,
        DISK_STATE_CMD,
        SYNC_STATE_CMD,
        SOCAT_STATE_UNKNOWN_CMD,
        CHECK_EXPIRE,
        SECTOR_FAULT,
    }

    public enum ExecuteType
    { 
        /// <summary>
        /// 정의 없음
        /// </summary>
        NONE,
        /// <summary>
        /// 하루 한번 실행
        /// </summary>
        ONCE,
        /// <summary>
        /// 매 시간 실행
        /// </summary>
        EVERYTIME,
    }
}
